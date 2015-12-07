using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftReports.Models;
using ShiftReports.DAL;
using PagedList;

namespace ShiftReports.Controllers
{
    public class ProductionController : Controller
    {
        private ShiftContext db = new ShiftContext();

        //
        // GET: /Production/

        public ActionResult Index(String sortOrder,string currentFilter, string searchString, int? page)
        {
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "User_desc" : "";
            ViewBag.ShiftSortParm = String.IsNullOrEmpty(sortOrder) ? "Shift_desc" : "";
            ViewBag.PlantSortParm = String.IsNullOrEmpty(sortOrder) ? "Plant_desc" : "";
            ViewBag.ActualMixSortParm = String.IsNullOrEmpty(sortOrder) ? "ActualMix_desc" : "";
            ViewBag.CrumbSortParm = String.IsNullOrEmpty(sortOrder) ? "Crumb_desc" : "";
            ViewBag.CMPSortParm = String.IsNullOrEmpty(sortOrder) ? "CMP_desc" : "";
            ViewBag.ManningSortParm = String.IsNullOrEmpty(sortOrder) ? "Manning_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var productiondata = from s in db.ProductionData
                                 select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                productiondata = productiondata.Where(s => s.Plant.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                    s.User.FirstMidName.ToUpper().Contains(searchString.ToUpper()) ||
                                    s.User.LastName.ToUpper().Contains(searchString.ToUpper()) ||
                                    s.Shift.Name.ToUpper().Contains(searchString.ToUpper()));
            }


            switch (sortOrder)
            {
                case "User_desc":
                    productiondata = productiondata.OrderByDescending(s => s.User);
                    break;
                case "Shift_desc":
                    productiondata = productiondata.OrderByDescending(s => s.Shift);
                    break;
                case "Plant_desc":
                    productiondata = productiondata.OrderByDescending(s => s.Plant);
                    break;
                case "ActualMix_desc":
                    productiondata = productiondata.OrderByDescending(s => s.ActualMix);
                    break;
                case "Crumb_desc":
                    productiondata = productiondata.OrderByDescending(s => s.CrumbWaste);
                    break;
                case "CMP_desc":
                    productiondata = productiondata.OrderByDescending(s => s.Cmp_Waste);
                    break;
                case "Manning_desc":
                    productiondata = productiondata.OrderByDescending(s => s.Manning);
                    break;
                case "Date_desc":
                    productiondata = productiondata.OrderBy(s => s.Date);
                    break;
                default:
                   productiondata = productiondata.OrderBy(s => s.Date);
                    break;
            
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(productiondata.ToPagedList(pageNumber,pageSize));
        }

        //
        // GET: /Production/Details/5

        public ActionResult Details(int id = 0)
        {
            Production production = db.ProductionData.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        //
        // GET: /Production/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName");
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name");
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name");
            return View();
        }

        //
        // POST: /Production/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID, ShiftID, PlantID, ActualMix, CrumbWaste, Cmp_Waste,Manning, Date")]Production production)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.ProductionData.Add(production);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", production.UserID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", production.ShiftID);
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", production.PlantID);
            return View(production);
        }

        //
        // GET: /Production/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Production production = db.ProductionData.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", production.UserID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", production.ShiftID);
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", production.PlantID);
            return View(production);
        }

        //
        // POST: /Production/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductionID, UserID, ShiftID, PlantID, ActualMix, CrumbWaste, Cmp_Waste, Manning, Date ")]Production production)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(production).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", production.UserID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", production.ShiftID);
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", production.PlantID);
            return View(production);
        }

        //
        // GET: /Production/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Production production = db.ProductionData.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        //
        // POST: /Production/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Production production = db.ProductionData.Find(id);
                db.ProductionData.Remove(production);
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