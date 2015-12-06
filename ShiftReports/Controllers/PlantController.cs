using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftReports.Models;
using ShiftReports.DAL;

namespace ShiftReports.Controllers
{
    public class PlantController : Controller
    {
        private ShiftContext db = new ShiftContext();

        //
        // GET: /Plant/

        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.MRPHSortParm = String.IsNullOrEmpty(sortOrder) ? "MRPH_desc" : "";
            var plants = from s in db.Plants
                         select s;

            switch (sortOrder)
            {
                case "Name_desc":
                    plants = plants.OrderByDescending(s => s.Name);
                    break;
                case "MRPH_desc":
                    plants = plants.OrderByDescending(s => s.MixRatePerHour);
                    break;
                default:
                    plants = plants.OrderBy(s => s.Name);
                    break;
            }
            return View(plants.ToList());
        }

        //
        // GET: /Plant/Details/5

        public ActionResult Details(int id = 0)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        //
        // GET: /Plant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Plant/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, MixRatePerHour")]Plant plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Plants.Add(plant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(plant);
        }

        //
        // GET: /Plant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        //
        // POST: /Plant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlantID, Name, MixRatePerHour")]Plant plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(plant).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                  //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                  ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            
             }
            return View(plant);
        }

        //
        // GET: /Plant/Delete/5

        public ActionResult Delete(bool? saveChangesError=false, int id = 0)
        {
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        //
        // POST: /Plant/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Plant plant = db.Plants.Find(id);
                db.Plants.Remove(plant);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}