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
    public class DowntimeTypeController : Controller
    {
        private ShiftContext db = new ShiftContext();

        //
        // GET: /DowntimeType/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PlantNameSortParm = String.IsNullOrEmpty(sortOrder) ? "PlantName_desc" : "";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "Type_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
 
            var downtype = from s in db.DowntimeTypes
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                downtype = downtype.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()) /*|| s.PlantID.ToString().Contains(searchString.ToUpper())*/);
            }
            switch (sortOrder)
            {
                case "PlantName_desc":
                    downtype = downtype.OrderByDescending(s => s.PlantID);
                    break;
                case "Type_desc":
                    downtype = downtype.OrderByDescending(s => s.Name);
                    break;
                default:
                    downtype = downtype.OrderBy(s => s.PlantID);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(downtype.ToPagedList(pageNumber,pageSize));
        }
         

        //
        // GET: /DowntimeType/Details/5

        public ActionResult Details(int id = 0)
        {
            DowntimeType downtimetype = db.DowntimeTypes.Find(id);
            if (downtimetype == null)
            {
                return HttpNotFound();
            }
            return View(downtimetype);
        }

        //
        // GET: /DowntimeType/Create

        public ActionResult Create()
        {
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name");
            return View();
        }

        //
        // POST: /DowntimeType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "plantID, Name")]DowntimeType downtimetype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DowntimeTypes.Add(downtimetype);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
           // ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", downtimetype.PlantID);
            return View(downtimetype);
        }

        //
        // GET: /DowntimeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DowntimeType downtimetype = db.DowntimeTypes.Find(id);
            if (downtimetype == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", downtimetype.PlantID);
            return View(downtimetype);
        }

        //
        // POST: /DowntimeType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DowntimeTypeID, plantID,Name")]DowntimeType downtimetype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(downtimetype).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", downtimetype.PlantID);
            return View(downtimetype);
        }

        //
        // GET: /DowntimeType/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            DowntimeType downtimetype = db.DowntimeTypes.Find(id);
            if (downtimetype == null)
            {
                return HttpNotFound();
            }
            return View(downtimetype);
        }

        //
        // POST: /DowntimeType/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                DowntimeType downtimetype = db.DowntimeTypes.Find(id);
                db.DowntimeTypes.Remove(downtimetype);
                db.SaveChanges();
            }
            catch(DataException /* dex */)
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