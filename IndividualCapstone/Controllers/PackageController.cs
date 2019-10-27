using IndividualCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndividualCapstone.Controllers
{
    public class PackageController : Controller
    {
        ApplicationDbContext db;
        public PackageController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Package
        public ActionResult Index()
        {
            return View();
        }

        // GET: Package/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Package/Create
        public ActionResult Create()
        {
            Package package = new Package();
            return View(package);
        }

        // POST: Doctors/Create
        [HttpPost]
        public ActionResult Create(Package package)
        {
            try
            {
                db.Packages.Add(package);
                db.SaveChanges();
                return RedirectToAction("Shipment", "Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Package/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Package/Edit/5
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

        // GET: Package/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Package/Delete/5
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
