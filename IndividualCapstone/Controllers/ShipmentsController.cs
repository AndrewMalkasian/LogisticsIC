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

namespace IndividualCapstone.Controllers
{
    public class ShipmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shipments
        public ActionResult Index()
        {
            var shipments = db.Shipments.Include(s => s.DeliveryAddressId).Include(s => s.PickupAddressId).Include(s=>s.Quote);
            return View(shipments.ToList());
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
        

        // GET: Shipments/Create
        public ActionResult Create()
        {
            ShipmentPackageAddressDelAndPickupViewModel viewModel = new ShipmentPackageAddressDelAndPickupViewModel();
            return View(viewModel);
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipmentPackageAddressDelAndPickupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
            
                db.Packages.Add(viewModel.Package);
                db.PickupAddresses.Add(viewModel.PickupAddress);
                db.DeliveryAddresses.Add(viewModel.DeliveryAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

            //ViewBag.DeliveryAddressId = new SelectList(db.DeliveryAddresses, "Id", "DeliveryZip", shipment.DeliveryAddressId);
            //ViewBag.PickupAddressId = new SelectList(db.PickupAddresses, "Id", "PickupZip", shipment.PickupAddressId);
            //ViewBag.QuoteId = new SelectList(db.CustomerQuotes, "Id", "ServiceLevel", shipment.QuoteId);
            //return View(shipment);
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
