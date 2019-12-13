using IndividualCapstone.Models;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace IndividualCapstone.Controllers
{
    public class ShipmentsController : Controller
    {
        private ApplicationDbContext db;

        public ShipmentsController()
        {
            db = new ApplicationDbContext();


        }

        // GET: Shipments
        public ActionResult Index()
        {
            var listOfShipments = db.Shipments.Include("DeliveryAddress").Include("DeliveryArea").
                 Include("PickupAddress").Include("ServiceLevel").Include("Package").Include("ServiceType").
                 Include("Quote").ToList();


            return View(listOfShipments);
        }
        [HttpPost]
        public ActionResult Index(List<Shipment> listOfShipments)
        {

            for (int i = listOfShipments.Count - 1; i >= 0 ; i--)
            {
                var shipment = listOfShipments[i];

                if (shipment.AddToRoute == false)
                {
                    listOfShipments.Remove(shipment);
                }       
            }
            

            var fromAddress = new MailAddress("Dmalkasian1206@gmail.com", "Andrew");
            var toAddress = new MailAddress("Dmalkasian1206@gmail.com", "To Name");
            const string fromPassword = "Captalmo1!";
            const string subject = "Delivery";
            const string body = "Your shipment is going to be delivered";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return View("Mapping", listOfShipments);
        }

        
        public ActionResult Mapping (List<Shipment> listOfShipments)
        {


            return View(listOfShipments);
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            } 
            return View(shipment);
        }


        // GET: Shipments/Create/{serviceLevel}/
        [HttpGet]
        public ActionResult Create()

        {

            var serviceLevels = db.ServiceLevels.ToList();
            var serviceTypes = db.ServiceTypes.ToList();

            Shipment shipment = new Shipment()
            {
                LevelOfServiceList = serviceLevels,
                ServiceTypeList = serviceTypes
            };

            return View(shipment);


        }

        //public async ActionResult ServiceTypeIdToShipmentModel()
        //{

        //    return RedirectToRoute("Create");
        //}
        //var teams = _context.Teams.ToList();
        //Player player = new Player()

        //    Teams = teams
        //};
        //    return View(pla.yer);

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Shipment shipment)
        {

            //run dimensional vs normal weight
            db.Packages.Add(shipment.Package);
            db.PickupAddresses.Add(shipment.PickupAddress);
            db.DeliveryAddresses.Add(shipment.DeliveryAddress);
            await DistanceFromPickupBackToHubString(shipment);
            await DistanceFromHubToDelivery(shipment);
            if (shipment.DistanceForDelivery > 100)
            {
                //hubtoAirportAndDelivery
                await GeocodedDestinationLatLongAddressString(shipment);
                await GettingNearestAirportLatLong(shipment);
                await DistanceBetweenCitiesString(shipment); // hub to hub
                //await DistanceFromAirPortToDeliveryString(shipment); 
                if(shipment.distanceBetweenHubAndAirport != 0)
                {
                    shipment.DistanceForFinalMile = 21;
                }
            }
            else
            {

            }
            
            MakingShipmentDeliveryId(shipment);
            db.SaveChanges();
            db.Shipments.Add(shipment);
            db.SaveChanges();
            CostLogicToRuleThemAll(shipment);
            db.SaveChanges();
            return RedirectToAction("Index", "Shipments");


            //ViewBag.DeliveryAddressId = new SelectList(db.DeliveryAddresses, "Id", "DeliveryZip", shipment.DeliveryAddressId);
            //ViewBag.PickupAddressId = new SelectList(db.PickupAddresses, "Id", "PickupZip", shipment.PickupAddressId);
            //ViewBag.QuoteId = new SelectList(db.CustomerQuotes, "Id", "ServiceLevel", shipment.QuoteId);
            //return View(shipment);
        }
        //ultimately would make origins --> admins.streetAdress + Zip


        //HUB TO DELIVERY
        public async Task<Shipment> DistanceFromHubToDelivery(Shipment shipment)
        {
            HttpClient http = new HttpClient();
            string hubLatLong = "42.948650,-87.909540"; // ultimately this would be the Admins business address
            string destinations = shipment.DeliveryAddress.StreetAddress + " " + shipment.DeliveryAddress.DeliveryZip;
            string destinationsUrl = destinations.Replace(" ", "+");
            var url = String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0}&destinations={1}&key={2}", hubLatLong, destinationsUrl, ApiKeys.GoogleApiKey);
            HttpResponseMessage response = await http.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleDistanceApi.Rootobject>(result);
            string distances = jsonData.Rows[0].Elements[0].Distance.Text;
            distances = distances.Replace(" mi", "");
            shipment.DistanceForDelivery = Convert.ToDecimal(distances);
            MakingShipmentDeliveryId(shipment);

            return shipment;
        }
        //First getting the destination address into latlong
        public async Task<Shipment> GeocodedDestinationLatLongAddressString(Shipment shipment) //#1
        {
            HttpClient http = new HttpClient();
            string destinationAddress = shipment.DeliveryAddress.StreetAddress + " " + shipment.DeliveryAddress.DeliveryZip;
            string destinationAddressUrl = destinationAddress.Replace(" ", "+");
            string geoCodeUrl = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", destinationAddressUrl, ApiKeys.GoogleApiKey);
            HttpResponseMessage response = await http.GetAsync(geoCodeUrl);
            string result = await response.Content.ReadAsStringAsync();
            var jsonDestinationData = JsonConvert.DeserializeObject<GoogleMapsGeoCodingApiJson.RootObject>(result);
           shipment.DeliveryAddress.AddressLatLong = jsonDestinationData.results[0].geometry.location.lat.ToString() + "," + jsonDestinationData.results[0].geometry.location.lng.ToString();
            return shipment;


        }
        //Getting the nearest airport near the latlong delivery address
        public async Task<Shipment> GettingNearestAirportLatLong(Shipment shipment) //#2
        {
            HttpClient http = new HttpClient();        
            string type = "Airport";
            string url = String.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0}&rankby=distance&type={1}&key={2}", shipment.DeliveryAddress.AddressLatLong, type, ApiKeys.GoogleApiKey);
            HttpResponseMessage response = await http.GetAsync(url);
            string results = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GooglePlacesApiJson.Rootobject>(results);
            shipment.AirportLatLong = jsonData.results[1].geometry.location.lat.ToString() + "," + jsonData.results[1].geometry.location.lng.ToString();
            return shipment;
            
        }
        //comparing the distance between the logitsics hub and the nearest airport
        public async Task<Shipment> DistanceBetweenCitiesString(Shipment shipment)
        {

            //this is responsible for getting the Airport nearest the delivery
            HttpClient http = new HttpClient();
            shipment.HubLatLong = "42.948650,-87.909540";  // ultimately this would be the Admins business address
            string distanceUrl = String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0}&destinations={1}&key={2}", shipment.HubLatLong, shipment.AirportLatLong, ApiKeys.GoogleApiKey);
            HttpResponseMessage responseTwo = await http.GetAsync(distanceUrl);
            string distanceResult = await responseTwo.Content.ReadAsStringAsync();
            var jsonDistanceData = JsonConvert.DeserializeObject<GoogleDistanceApi.Rootobject>(distanceResult);
            string distances = jsonDistanceData.Rows[0].Elements[0].Distance.Text;
            distances = distances.Replace(" mi", "");
            shipment.distanceBetweenHubAndAirport = Convert.ToDecimal(distances);
            //This is responsible for getting the distance between latLong of airport and final delivery
            return shipment;

            //return latLongType;

            //shipment.distanceBetweenHubAndAirport = jsonData.rows[0].Elements[0].Distance.Text.ToString();
            // shipment.DistanceForPickup = Convert.Todecmial(distances);



        }
        public async Task<Shipment> DistanceFromAirPortToDeliveryString(Shipment shipment)
        {
            HttpClient http = new HttpClient();
            var url = String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0}&destinations={1}&key={2}", shipment.AirportLatLong, shipment.DeliveryAddress.AddressLatLong, ApiKeys.GoogleApiKey);
            HttpResponseMessage response = await http.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleDistanceApi.Rootobject>(result);
            string distances = jsonData.Rows[0].Elements[0].Distance.Text;
            distances = distances.Replace(" mi", "").Replace(" ft", "");
            shipment.DistanceForFinalMile = Convert.ToDecimal(distances);
            MakingShipmentDeliveryId(shipment);
            return shipment;
            //PICKUP BACK TO HUB
         


            
        }
        //This one is good to go
        public async Task<Shipment> DistanceFromPickupBackToHubString(Shipment shipment)
        {
            HttpClient http = new HttpClient();

            string origins = shipment.PickupAddress.StreetAddress + "+" + shipment.PickupAddress.PickupZip;
            string pickupUrl = origins.Replace(" ", "+");
            shipment.HubLatLong = "42.948650,-87.909540";
            //string pickupZip = db.PickupAddresses.Where(p => p.Id == shipment.PickupAddress.PickupZip).FirstOrDefault();
            //    decmial latAddressOne = jsonDataAddressOne.results[0].geometry.location.lat;
            //    decmial longAddressOne = jsonDataAddressOne.results[0].geometry.location.lng;
            var url = String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0}&destinations={1}&key={2}", pickupUrl, shipment.HubLatLong, ApiKeys.GoogleApiKey);
            HttpResponseMessage response = await http.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleDistanceApi.Rootobject>(result);
            string distances = jsonData.Rows[0].Elements[0].Distance.Text;
            distances = distances.Replace(" mi", "");
            shipment.DistanceForPickup = Convert.ToDecimal(distances);
            MakingShipmentPickupId(shipment);
            //MakingDeliveryId

            return shipment;
        }

        public Shipment CostLogicToRuleThemAll(Shipment shipment)
        {
            decimal FuelSurcharge = Convert.ToDecimal(1.12);
            decimal weightCharge = Convert.ToDecimal(.02);
            
            Quote quote = new Quote();
            decimal packageCost = shipment.Package.Weight * weightCharge;
            quote.PackageCost = packageCost;
            decimal PickupCost = shipment.DistanceForPickup * FuelSurcharge;
            quote.PickupCost = PickupCost;
            decimal DeliveryCost = shipment.DistanceForDelivery * FuelSurcharge; 
            quote.DeliveryCost = DeliveryCost;
            decimal CostBetweenCities = shipment.distanceBetweenHubAndAirport * FuelSurcharge * weightCharge;
            quote.BetweenCitiesCost = CostBetweenCities;
            //if(shipment.distanceBetweenHubAndAirport != 0 && shipment.DeliveryArea.NameOfServiceArea == "Next Day")
            //{
            //    decmial CostBetweenCities = shipment.distanceBetweenHubAndAirport * FuelSurcharge * 2.6;
            //    quote.BetweenCitiesCost = CostBetweenCities;
            //}
            //else
            //{
            //    decmial CostBetweenCities = shipment.distanceBetweenHubAndAirport * FuelSurcharge * .02;
            //    quote.BetweenCitiesCost = CostBetweenCities;
            //}
            decimal? shipmentCost = quote.PackageCost + quote.PickupCost + quote.DeliveryCost + quote.BetweenCitiesCost;
            shipment.ShipmentCost = shipmentCost;
            db.Quotes.Add(quote);
           

            return shipment;
        }

        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Shipment shipment = db.Shipments.Find(id);
        if (shipment == null)
        {
            return HttpNotFound();
        }
        //ViewBag.DeliveryAddressId = new SelectList(db.DeliveryAddresses, "Id", "DeliveryZip", shipment.DeliveryAddressId);
        //ViewBag.PickupAddressId = new SelectList(db.PickupAddresses, "Id", "PickupZip", shipment.PickupAddressId);
        //ViewBag.QuoteId = new SelectList(db.CustomerQuotes, "Id", "ServiceLevel", shipment.QuoteId);
        return View(shipment);
    }

    // POST: Shipments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Pieces,ResidentialShipment,CommericalShipment,PackagesSerialized,DeliveryAddressId,PickupAddressId,QuoteId")] Shipment shipment)
    {
        if (ModelState.IsValid)
        {
            db.Entry(shipment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.DeliveryAddressId = new SelectList(db.DeliveryAddresses, "Id", "DeliveryZip", shipment.DeliveryAddressId);
        ViewBag.PickupAddressId = new SelectList(db.PickupAddresses, "Id", "PickupZip", shipment.PickupAddressId);
        ViewBag.QuoteId = new SelectList(db.Quotes, "Id", "ServiceLevel", shipment.QuoteId);
        return View(shipment);
    }

    // GET: Shipments/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Shipment shipment = db.Shipments.Find(id);
        if (shipment == null)
        {
            return HttpNotFound();
        }
        return View(shipment);
    }

    // POST: Shipments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Shipment shipment = db.Shipments.Find(id);
        db.Shipments.Remove(shipment);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
        public decimal WeightVsDimensionalWeight(Shipment shipment)
        {
            shipment.Package.DimensionalWeight = shipment.Package.Length * shipment.Package.Width * shipment.Package.Height / shipment.Package.DimFactor;
            if (shipment.Package.DimensionalWeight >= shipment.Package.Weight)
            {
                shipment.Package.DimensionalWeight = shipment.Package.Weight;
                return shipment.Package.Weight;
            }
            else
            {
                return shipment.Package.Weight;
            }
        }

        //IF SERVICEPOINT IS OVER 100 MILES AWAY
        // DistanceFromPickUp applies

        public decimal distanceFromAirportToDelivery()
    { 
        return 0;
    }
    public Shipment MakingShipmentPickupId(Shipment shipment)
    {
            if (shipment.DistanceForPickup >= 0 && shipment.DistanceForPickup <= 10)
            {
                shipment.PickupAreaId = db.PickupAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaA").Id;
                //decmial shipmentCosts = db.PickupAreas.FirstOrDefault(p => p.Id == shipment.PickupAreaId).CostOfServiceAreaPoint;
                //shipmentCosts = 60 + (shipment.Package.Weight * .20);
                return shipment;
            }
            else if (shipment.DistanceForPickup > 10 && shipment.DistanceForPickup <= 20)
            {
                shipment.PickupAreaId = db.PickupAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaB").Id;
                //decmial shipmentCosts = db.PickupAreas.FirstOrDefault(p => p.Id == shipment.PickupAreaId).CostOfServiceAreaPoint;
                //shipmentCosts = 30 + (shipment.Package.Weight * .20); ;
                return shipment;
            }
            else if (shipment.DistanceForPickup > 20 && shipment.DistanceForPickup <= 30)
            {
                shipment.PickupAreaId = db.PickupAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaC").Id;
                //decmial shipmentCosts = db.PickupAreas.FirstOrDefault(p => p.Id == shipment.PickupAreaId).CostOfServiceAreaPoint;
                //shipmentCosts = 40 + (shipment.Package.Weight * .20);
                return shipment;
            }

            else if (shipment.DistanceForPickup > 30 && shipment.DistanceForPickup <= 40)
            {
                shipment.PickupAreaId = db.PickupAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaD").Id;
                //decmial shipmentCosts = db.PickupAreas.FirstOrDefault(p => p.Id == shipment.PickupAreaId).CostOfServiceAreaPoint;
                //shipmentCosts = 50 + (shipment.Package.Weight * .20);
                return shipment;
            }

            else
            {
                shipment.PickupAreaId = db.PickupAreas.FirstOrDefault(s => s.NameOfServiceArea == "BeyondAreaY").Id;
                //decmial shipmentCosts = db.PickupAreas.FirstOrDefault(p => p.Id == shipment.PickupAreaId).CostOfServiceAreaPoint;
                //shipmentCosts = 60 + (shipment.Package.Weight * .20);
                //var BeyondRate = (2.60 * shipment.DistanceForPickup) + (shipment.Package.Weight * .20);
                //shipment.PickupArea.CostOfServiceAreaPoint = BeyondRate;
                return shipment;
            }
    }
        public Shipment MakingShipmentDeliveryId(Shipment shipment)
        {
            //decmial fuelSurcharge = 1.12;

            if (shipment.DistanceForDelivery >= 0 && shipment.DistanceForDelivery <= 10)
            {
                shipment.DeliveryAreaId = db.DeliveryAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaA").Id;
                //shipment.DeliveryArea.CostOfServiceAreaPoint = 15 * fuelSurcharge + (shipment.Package.Weight * .16);
                return shipment;
            }

            else if (shipment.DistanceForDelivery > 10 && shipment.DistanceForDelivery <= 20)
            {
                shipment.DeliveryAreaId = db.DeliveryAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaB").Id;
                //shipment.DeliveryArea.CostOfServiceAreaPoint = 30 * fuelSurcharge + (shipment.Package.Weight * .18);
                return shipment;
            }
            else if (shipment.DistanceForDelivery > 20 && shipment.DistanceForDelivery <= 30)
            {
                shipment.DeliveryAreaId = db.DeliveryAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaC").Id;
                //shipment.DeliveryArea.CostOfServiceAreaPoint = 45 * fuelSurcharge + (shipment.Package.Weight * .19);
                return shipment;
            }

            else if (shipment.DistanceForDelivery > 30 && shipment.DistanceForDelivery <= 40)
            {
                shipment.DeliveryAreaId = db.DeliveryAreas.FirstOrDefault(s => s.NameOfServiceArea == "LocalAreaD").Id;
                //shipment.DeliveryArea.CostOfServiceAreaPoint = 60 * fuelSurcharge + (shipment.Package.Weight * .20);
                return shipment;
            }

            else
            {
                shipment.DeliveryAreaId = db.PickupAreas.FirstOrDefault(s => s.NameOfServiceArea == "BeyondAreaY").Id;
                //var BeyondRate = (2.15 * shipment.DistanceForDelivery * fuelSurcharge) + (shipment.Package.Weight * .20);
                //shipment.DeliveryArea.CostOfServiceAreaPoint = BeyondRate;
                return shipment;
            }
            
        }
        //public List<Shipment> Mapping(Shipment shipment)
        //{
           
        //}
        //public decimal ServiceLevelCostLogic(Shipment shipment)
        //{
        //    decimal fuelSurcharge = 1.12;
        //    var serviceLevel = db.ServiceLevels.FirstOrDefault(s => s.Id == shipment.ServiceLevelId).TypeOfServiceLevel;
        //    var serviceLevelCost = db.ServiceLevels.FirstOrDefault(s => s.Id == shipment.ServiceLevelId).CostOfServiceLevel;

        //    if (serviceLevel == "Next Day" || serviceLevel == "Second Day" || serviceLevel == "Third Day" || serviceLevel == "Economy")
        //    {
        //        if (shipment.DeliveryArea.NameOfServiceArea == "BeyondAreaY")
        //        {
        //            decimal BeyondRate = (2.15 * shipment.DistanceForDelivery * fuelSurcharge) + (shipment.Package.Weight * .20);
        //            serviceLevelCost += BeyondRate + shipment.DeliveryArea.CostOfServiceAreaPoint;
        //            return serviceLevelCost;
        //        }
        //        else
        //        {
        //            serviceLevelCost += shipment.DeliveryArea.CostOfServiceAreaPoint;
        //            return serviceLevelCost;
        //        }


        //    }
        //    return 0;

        //}

    }

}


//TODO:add latlong/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//public decmial DegreesToRadians(decmial degrees)
//{

//    return degrees * Math.PI / 180;

//}

//public decmial DistanceInKmBetweenEarthCoordinates(decmial lat1, decmial lon1, decmial lat2, decmial lon2)
//{
//    var earthRadiusKm = 6371;

//    var dLat = DegreesToRadians(lat2 - lat1);
//    var dLon = DegreesToRadians(lon2 - lon1);

//    lat1 = DegreesToRadians(lat1);
//    lat2 = DegreesToRadians(lat2);

//    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
//            Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
//    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

//    var distanceInKm = earthRadiusKm * c;
//    var distanceInKmConvertedToMi = distanceInKm * .05399568035;
//    return ShipmentServiceAreaIdandCost(distanceInKmConvertedToMi);
//}
//THIS WILL CREATE THE SERVICEAREA_ID and THE RESPECTIVE COST FOR EACH SHIPMENT



    //public async Task<decmial> TwoGeoCodedAddressesToLatLongStrings(Shipment shipment)
    //{
    //    string addressOne = db.PickupAddresses.Where(p => p.Id == shipment.PickupAddressId).Select(p => p.PickupZip).FirstOrDefault();
    //    HttpClient http = new HttpClient();
    //    String urlOne = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", addressOne, ApiKeys.GoogleApiKey);
    //    HttpResponseMessage responseOne = await http.GetAsync(urlOne);
    //    var resultOne = await responseOne.Content.ReadAsStringAsync();
    //    GoogleMapsGeoCodingApiJson.Rootobject jsonDataAddressOne = JsonConvert.DeserializeObject<GoogleMapsGeoCodingApiJson.Rootobject>(resultOne);

//    decmial latAddressOne = jsonDataAddressOne.results[0].geometry.location.lat;
//    decmial longAddressOne = jsonDataAddressOne.results[0].geometry.location.lng;

//    // var newHttp = new HttpClient();
//    string addressTwo = db.DeliveryAddresses.Where(d => d.Id == shipment.DeliveryAddressId).Select(d => d.DeliveryZip).FirstOrDefault();
//    String urlTwo = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", addressTwo, ApiKeys.GoogleApiKey);
//    var responseTwo = await http.GetAsync(urlTwo);
//    var resultTwo = await responseTwo.Content.ReadAsStringAsync();
//    GoogleMapsGeoCodingApiJson.Rootobject jsonDataAddressTwo = JsonConvert.DeserializeObject<GoogleMapsGeoCodingApiJson.Rootobject>(resultTwo);

//    decmial latAddressTwo = jsonDataAddressTwo.results[0].geometry.location.lat;
//    decmial longAddressTwo = jsonDataAddressTwo.results[0].geometry.location.lng;

//    return DistanceInKmBetweenEarthCoordinates(latAddressOne, longAddressOne, latAddressTwo, longAddressTwo);
//}

//Logic FOR DISTANCE//////BETWEEN TWO/////GEO POINTS////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////








