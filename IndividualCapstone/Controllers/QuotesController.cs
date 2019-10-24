using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IndividualCapstone.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace IndividualCapstone.Controllers
{
    public class QuotesController : Controller
    {
        private ApplicationDbContext db;

        public QuotesController()
        {
            db = new ApplicationDbContext();
        }
        // GET: CustomerQuotes
        public ActionResult Index()
        {
            return View(db.CustomerQuotes.ToList());
        }

        // GET: CustomerQuotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote customerQuote = db.CustomerQuotes.Find(id);
            if (customerQuote == null)
            {
                return HttpNotFound();
            }
            return View(customerQuote);
        }

        // GET: CustomerQuotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerQuotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pieces,Weight,Dimensions,PickupZip,PickupDate,PickupWindowStart,PickupWindowEnd,DeliveryZip,DeliveryWindowStart,DeliveryWindowEnd")] Quote customerQuote)
        {
            if (ModelState.IsValid)
            {
                db.CustomerQuotes.Add(customerQuote);
                // UpdateQuoteValue();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerQuote);
        }



        // GET: CustomerQuotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote customerQuote = db.CustomerQuotes.Find(id);
            if (customerQuote == null)
            {
                return HttpNotFound();
            }
            return View(customerQuote);
        }

        // POST: CustomerQuotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pieces,Weight,Dimensions,PickupZip,PickupDate,PickupWindowStart,PickupWindowEnd,DeliveryZip,DeliveryWindowStart,DeliveryWindowEnd")] Quote customerQuote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerQuote).State = EntityState.Modified;
                // UpdateQuoteValue();

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerQuote);
        }

        // GET: CustomerQuotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote customerQuote = db.CustomerQuotes.Find(id);
            if (customerQuote == null)
            {
                return HttpNotFound();
            }
            return View(customerQuote);
        }

        // POST: CustomerQuotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote customerQuote = db.CustomerQuotes.Find(id);
            db.CustomerQuotes.Remove(customerQuote);
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
        public int PiecesPriceLogic()
        {
            
            return 0;
        }
        
        //public async int TotalQuoteAmount()
        //{
       
           

        //}
        //public int WeightPriceLogic(Package package)
        ////{
        //    if ()
        //        if ()
        //        {
        //            //estimatedWeight ? 45 : 15
        //        }
        //    return estimatedWeight;

        //}
        public int DimensionalWeightPriceLogic(Package package)
        {
          package.DimensionalWeight = package.Length * package.Width * package.Height / package.DimFactor;
            if(package.DimensionalWeight >= package.Weight)
            {
                return package.DimensionalWeight;
            }
            else
            {
                return package.Weight;
            }
           
        }
        public async Task<string> GeocodeFromLocationToLatLongString(string location)
        {
            var http = new HttpClient();
            var url = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", location, Keys.GoogleApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleMapsGeoCodingApiJson.Rootobject>(result);
            var latLong = jsonData.results[0].geometry.location.lat.ToString() + "," + jsonData.results[0].geometry.location.lng.ToString();

            return latLong;
        }

        public int ZipCodeMileagePriceLogic()
        {
            if (true)
            {

            }
            return 0;
        }

        public int PickupTimePriceLogic()
        {
            
            return 0;
        }
        public int PickupDatePriceLogic()
        {
            return 0;
        }
        public int DeliveryDatePriceLogic()
        {
            return 0;
        }
        public int DeliveryTimePriceLogic()
        {
            return 0;
        }

    }
}
