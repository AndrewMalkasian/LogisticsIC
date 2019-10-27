using IndividualCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndividualCapstone.Controllers
{
    public class DeliveryAddressesController : Controller
    {
        ApplicationDbContext db;
        public DeliveryAddressesController()
        {
            db = new ApplicationDbContext();
        }
        // GET: DeliveryAddresses
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeliveryAddresses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: DeliveryAddresses/Create
        public ActionResult Create()
        {
          
        
            return View();
        }

        [HttpPost]
        public ActionResult Create(DeliveryAddress deliveryAddress)
        {

            db.DeliveryAddresses.Add(deliveryAddress);
            db.SaveChanges();
            return RedirectToAction("shipment", "Create");
        }

        // GET: DeliveryAddresses/Edit/5
       
        public ActionResult Edit(int id)
        {
            return View("Index","Quotes");
        }

        // POST: DeliveryAddresses/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DeliveryAddresses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryAddresses/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
