using IndividualCapstone.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<ActionResult> Index()
        {
            var shipment = db.Shipments.FirstOrDefault();
            await DistanceFromOriginAndDestinationsString(shipment);
            return View();
            //shipments.ToList()
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
        public ActionResult Create(Shipment shipment)
        {


            db.Packages.Add(shipment.Package);
            db.PickupAddresses.Add(shipment.PickupAddress);
            db.DeliveryAddresses.Add(shipment.DeliveryAddress);
            db.SaveChanges();
            db.Shipments.Add(shipment);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");


            //ViewBag.DeliveryAddressId = new SelectList(db.DeliveryAddresses, "Id", "DeliveryZip", shipment.DeliveryAddressId);
            //ViewBag.PickupAddressId = new SelectList(db.PickupAddresses, "Id", "PickupZip", shipment.PickupAddressId);
            //ViewBag.QuoteId = new SelectList(db.CustomerQuotes, "Id", "ServiceLevel", shipment.QuoteId);
            //return View(shipment);
        }
       //Logic FOR DISTANCE//////BETWEEN TWO/////GEO POINTS////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<string> DistanceFromOriginAndDestinationsString(Shipment shipment)
        {
            var http = new HttpClient();
            string origins = db.PickupAddresses.Where(p => p.Id == shipment.PickupAddressId).Select(p => p.PickupZip).FirstOrDefault();
            string destinations = db.DeliveryAddresses.Where(d => d.Id == shipment.DeliveryAddressId).Select(d => d.DeliveryZip).FirstOrDefault();
            var url = String.Format("https://maps.googleapis.com/maps/api/geocode/json?origins={0}&destinations={0}key={1}", origins, destinations, ApiKeys.GoogleApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleDistanceApi.Rootobject>(result);
            string distances = jsonData.Rows[0].Elements[0].Distance.Text.ToString();
            double distanceBetween = Convert.ToDouble(distances);
            ShipmentServiceAreaIdAndCost(shipment, distanceBetween);
            return "add logic";
        }
     

        public ActionResult ShipmentServiceAreaIdAndCost(Shipment shipment, double distanceBetween)
        {
            ServiceArea serviceArea = new ServiceArea();
            serviceArea.Distance = distanceBetween;
          
            if(distanceBetween >= 0 || distanceBetween <= 10)
            {
                //var shipmentServiceAreaId = db.Shipments.Where(s => s.ServiceAreaId == null).Where(s => s.Id == s.);
              
                return View();
            }
            else if (distanceBetween > 10 && distanceBetween <= 20 )
            {
              
            }
 
            else if (distanceBetween > 20 && distanceBetween <= 30)
            {

            }
            else if (distanceBetween > 30 && distanceBetween <= 40)
            {

            }



            return View();
        }
        //public double DegreesToRadians(double degrees)
        //{

        //    return degrees * Math.PI / 180;

        //}

        //public double DistanceInKmBetweenEarthCoordinates(double lat1, double lon1, double lat2, double lon2)
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



        //public async Task<double> TwoGeoCodedAddressesToLatLongStrings(Shipment shipment)
        //{
        //    string addressOne = db.PickupAddresses.Where(p => p.Id == shipment.PickupAddressId).Select(p => p.PickupZip).FirstOrDefault();
        //    HttpClient http = new HttpClient();
        //    String urlOne = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", addressOne, ApiKeys.GoogleApiKey);
        //    var responseOne = await http.GetAsync(urlOne);
        //    var resultOne = await responseOne.Content.ReadAsStringAsync();
        //    GoogleMapsGeoCodingApiJson.Rootobject jsonDataAddressOne = JsonConvert.DeserializeObject<GoogleMapsGeoCodingApiJson.Rootobject>(resultOne);

        //    double latAddressOne = jsonDataAddressOne.results[0].geometry.location.lat;
        //    double longAddressOne = jsonDataAddressOne.results[0].geometry.location.lng;

        //    // var newHttp = new HttpClient();
        //    string addressTwo = db.DeliveryAddresses.Where(d => d.Id == shipment.DeliveryAddressId).Select(d => d.DeliveryZip).FirstOrDefault();
        //    String urlTwo = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", addressTwo, ApiKeys.GoogleApiKey);
        //    var responseTwo = await http.GetAsync(urlTwo);
        //    var resultTwo = await responseTwo.Content.ReadAsStringAsync();
        //    GoogleMapsGeoCodingApiJson.Rootobject jsonDataAddressTwo = JsonConvert.DeserializeObject<GoogleMapsGeoCodingApiJson.Rootobject>(resultTwo);

        //    double latAddressTwo = jsonDataAddressTwo.results[0].geometry.location.lat;
        //    double longAddressTwo = jsonDataAddressTwo.results[0].geometry.location.lng;

        //    return DistanceInKmBetweenEarthCoordinates(latAddressOne, longAddressOne, latAddressTwo, longAddressTwo);
        //}

        //Logic FOR DISTANCE//////BETWEEN TWO/////GEO POINTS////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
























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
            ViewBag.DeliveryAddressId = new SelectList(db.DeliveryAddresses, "Id", "DeliveryZip", shipment.DeliveryAddressId);
            ViewBag.PickupAddressId = new SelectList(db.PickupAddresses, "Id", "PickupZip", shipment.PickupAddressId);
            ViewBag.QuoteId = new SelectList(db.CustomerQuotes, "Id", "ServiceLevel", shipment.QuoteId);
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
            ViewBag.QuoteId = new SelectList(db.CustomerQuotes, "Id", "ServiceLevel", shipment.QuoteId);
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

    }
}
