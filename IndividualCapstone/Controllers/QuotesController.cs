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
    public class QuotesController : Controller
    {
        private ApplicationDbContext db;
        public static T FromJSON<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Deserialize<T>(json);
        }

        public int fuelSurcharge { get; private set; }

        public QuotesController()
        {
            db = new ApplicationDbContext();

        }

        // GET: Quotes
        public async Task<ActionResult> Index()
        {
           
            return View(db.CustomerQuotes.ToList());
        }

        // GET: Quotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.CustomerQuotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: Quotes/Create
        public ActionResult Create()
        {
            return View("Create", "Shipment");
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
                db.CustomerQuotes.Add(quote);
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
            Quote quote = db.CustomerQuotes.Find(id);
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
            Quote quote = db.CustomerQuotes.Find(id);
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
            Quote quote = db.CustomerQuotes.Find(id);
            db.CustomerQuotes.Remove(quote);
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
        //return RedirectToAction("Create", new { latLong = geocodedLocation, locationName = selectedLocation });

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public float WeightVsDimensionalWeight(Package package)
        {
            package.DimensionalWeight = package.Length * package.Width * package.Height / package.DimFactor;
            if (package.DimensionalWeight >= package.Weight)
            {
                return package.DimensionalWeight;
            }
            else
            {
                return package.Weight;
            }
        }
    }
        //public static double GetTwoAddresses(string address1, string address2)
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

        //public double CostForFiveServiceAreas(ServiceArea serviceArea)
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


        //}
       
}
      
      


    

