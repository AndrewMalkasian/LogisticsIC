using IndividualCapstone.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IndividualCapstone.Controllers
{
    public class QuoteController : Controller
    {
        private ApplicationDbContext db;

        public int fuelSurcharge { get; private set; }

        public QuoteController()
        {
            db = new ApplicationDbContext();

        }

        // GET: Quotes
        public async Task<ActionResult> Index()
        {
            var shipment = db.Shipments.FirstOrDefault();
            //await DistanceFromPickupBackToHubString(shipment);
            return View();
            //shipments.ToList()
        }

        // GET: Quotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: Quotes/Create
        public ActionResult Create(Shipment shipment)
        {
            Quote quote = new Quote();
            var newQuote = db.Shipments.Where(q => q.Id == shipment.QuoteId);
        

            return View(newQuote);
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,PackageCost,PickupCost,DeliveryCost,ServiceLevel,ServiceLevelCost,ShipmentCost")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Quotes.Add(quote);
                db.SaveChanges();
                return RedirectToAction("FinalizeQuote");
            }

            return View(quote);
        }

        //public ActionResult AddPickUpAddress()
        //Public ActionResult AddDeliveryAddress()
        //Public ActionResult AddServiceLevel()
        //Public ActionResult AddServiceType()


        //public ActionResult CalculateQuote()
        //{
        //    //TODO: goes to quote screen and gives an amount and they can choose
        //    return 0;
        //}

        // GET: Quotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PackageCost,PickupCost,DeliveryCost,ServiceLevel,ServiceLevelCost,ShipmentCost")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quote);
        }

        // GET: Quotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        //public async string DistanceBetween(Shipment shipment)
        //{
        //    //take in the userinput address broken down and apply it to the origin;
        //    var distanceInMiles = DistanceFromOriginAndDestinationsString(pickupInput, deliveryInput);


        //pickupAddress = 
        //string geocodedLocation = await GeocodeFromLocationToLatLongString(selectedLocation);
        //return RedirectToAction("Create", new { = geocodedLocation, locationName = selectedLocation });

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
       
        


       
        public decimal DistanceFromHubToNearestAirPort(Shipment shipment)
        {
            return 0;
        }
   
                
            
    }
}


//public static decmial GetTwoAddresses(string address1, string address2)
//{

//    //make jason requests to google api
//    string json1 = WebRequestJson(
//        string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false", address1)
//        );
//    string json2 = WebRequestJson(
//        string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false", address2)
//        );

//    //deserialize json to GeocodeResponse object
//    var loc1 = FromJSON<GoogleMapsGeoCodingApiJson>(json1);
//    var loc2 = FromJSON<GoogleMapsGeoCodingApiJson>(json2);

//    //check is response is ok
//    if (loc1.status != "OK" || loc2.Status != "OK") { return -1; }

//    //check if only one location found for each address
//    //if (loc1.Results.Count > 1 || loc2.Results.Count > 1) { return -2; }

//    //copy coordinate values
//    var pos1 = new Location();
//    {
//        var Latitude = loc1.geometry.Location.Lat,
//         Longitude = loc1.results.First().Geometry.Location.Lng
//    };

//    {
//        Latitude = loc2.Results.First().Geometry.Location.Lat,
//        Longitude = loc2.Results.First().Geometry.Location.Lng
//    };

//    //return distance in unit
//    return CalculateDistance(pos1, pos2, unit);
//}

//public decmial CostForFiveServiceAreas(ServiceArea serviceArea)
//{

//    // var distanceFromOriginPoint;
//    if (serviceArea.Distance >= serviceArea.A && serviceArea.Distance < serviceArea.B)
//    {
//        serviceArea.Rate = 15;
//        return newQuote.ServiceAreaCost += serviceArea.Rate;
//    }
//    else if (serviceArea.Distance >= serviceArea.B && serviceArea.Distance < serviceArea.C)
//    {
//        serviceArea.Rate = 30;
//        return newQuote.ServiceAreaCost += serviceArea.Rate;
//    }
//    else if (serviceArea.Distance >= serviceArea.C && serviceArea.Distance < serviceArea.D)
//    {
//        serviceArea.Rate = 40;
//        return newQuote.ServiceAreaCost += serviceArea.Rate;
//    }
//    else if (serviceArea.Distance >= serviceArea.D && serviceArea.Distance < serviceArea.Y)
//    {
//        serviceArea.Rate = 55;
//        return newQuote.ServiceAreaCost += serviceArea.Rate;
//    }
//    else if (serviceArea.Distance >= serviceArea.Y)
//    {
//        serviceArea.Rate = 60;
//        var beyondPricing = serviceArea.Distance * newQuote.FuelSurcharge + serviceArea.Rate;
//        //should probably these ints into variable
//        return beyondPricing;
//    }
//    else
//    {
//        return 0;
//    }
//    // starting point if NOT sameday = makariosAddress


//        }

//}





////////public async Task<string> DistanceForPickUpAndDeliveryLocal(Shipment shipment)
////////{
////////    HttpClient http = new HttpClient();
////////    string pickupZip = db.PickupAddresses.Where(p => p.Id == shipment.PickupAddressId).Select(p => p.PickupZip).FirstOrDefault();
////////    //    decmial latAddressOne = jsonDataAddressOne.results[0].geometry.location.lat;
////////    //    decmial longAddressOne = jsonDataAddressOne.results[0].geometry.location.lng;
////////    string destinationZip = db.DeliveryAddresses.Where(d => d.Id == shipment.DeliveryAddressId).Select(d => d.DeliveryZip).FirstOrDefault();
////////    var url = String.Format("https://maps.googleapis.com/maps/api/geocode/json?origins={0}&destinations={0}key={1}", pickupZip, destinationZip, ApiKeys.GoogleApiKey);
////////    HttpResponseMessage response = await http.GetAsync(url);
////////    string result = await response.Content.ReadAsStringAsync();
////////    var jsonData = JsonConvert.DeserializeObject<GoogleDistanceApi.Rootobject>(result);
////////    string distances = jsonData.Rows[0].Elements[0].Distance.Text.ToString();
////////    decmial distanceBetween = Convert.Todecmial(distances);
////////    //await PlacesTypeApiSearch(shipment, distanceBetween);
////////    return "add logic";
////////}
