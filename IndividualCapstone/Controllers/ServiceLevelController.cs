using IndividualCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IndividualCapstone.Controllers
{
    public class ServiceLevelController : Controller
    {
        ApplicationDbContext db;
       
        
        public ServiceLevelController()
        {
            db = new ApplicationDbContext();
           
        }
      


        [HttpGet]
        public ActionResult GetTypeOfServiceLevelInList()
        {
            var serviceTypeList = new SelectList(new[] { "Next Day", "Two Day", "Three Day", "Economy" });
            ViewBag.serviceTypeList = serviceTypeList;

            return View();

        }
        // GET: ServiceLevel
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceLevel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceLevel/Create
        [HttpPost]
        public ActionResult Create(ServiceLevel serviceLevel)
        {
            try
            {
                db.ServiceLevels.Add(serviceLevel);
                db.SaveChanges();
                return View();
                //return this to the admin edit Service level on their controller
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceLevel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceLevel/Edit/5
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

        // GET: ServiceLevel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceLevel/Delete/5
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
