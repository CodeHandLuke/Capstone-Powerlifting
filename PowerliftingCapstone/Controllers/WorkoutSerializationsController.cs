using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerliftingCapstone.Models;

namespace PowerliftingCapstone.Controllers
{
    public class WorkoutSerializationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkoutSerializations
        public ActionResult Index()
        {
            return View(db.WorkoutSerializations.ToList());
        }

        // GET: WorkoutSerializations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutSerialization workoutSerialization = db.WorkoutSerializations.Find(id);
            if (workoutSerialization == null)
            {
                return HttpNotFound();
            }
            return View(workoutSerialization);
        }

        // GET: WorkoutSerializations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkoutSerializations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutSerialId,WorkoutId,WeekDay")] WorkoutSerialization workoutSerialization)
        {
            if (ModelState.IsValid)
            {
                db.WorkoutSerializations.Add(workoutSerialization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workoutSerialization);
        }

        // GET: WorkoutSerializations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutSerialization workoutSerialization = db.WorkoutSerializations.Find(id);
            if (workoutSerialization == null)
            {
                return HttpNotFound();
            }
            return View(workoutSerialization);
        }

        // POST: WorkoutSerializations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkoutSerialId,WorkoutId,WeekDay")] WorkoutSerialization workoutSerialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workoutSerialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workoutSerialization);
        }

        // GET: WorkoutSerializations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutSerialization workoutSerialization = db.WorkoutSerializations.Find(id);
            if (workoutSerialization == null)
            {
                return HttpNotFound();
            }
            return View(workoutSerialization);
        }

        // POST: WorkoutSerializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkoutSerialization workoutSerialization = db.WorkoutSerializations.Find(id);
            db.WorkoutSerializations.Remove(workoutSerialization);
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
