using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PowerliftingCapstone.Models;

namespace PowerliftingCapstone.Controllers
{
    public class SavedWorkoutDateTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SavedWorkoutDateTimes
        public ActionResult Index()
        {
            return View(db.SavedWorkoutDateTimes.ToList());
        }

        // GET: SavedWorkoutDateTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedWorkoutDateTime savedWorkoutDateTime = db.SavedWorkoutDateTimes.Find(id);
            if (savedWorkoutDateTime == null)
            {
                return HttpNotFound();
            }
            return View(savedWorkoutDateTime);
        }

        // GET: SavedWorkoutDateTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SavedWorkoutDateTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SavedWorkoutDateId,Date,WorkoutId,UserId")] SavedWorkoutDateTime savedWorkoutDateTime)
        {
            if (ModelState.IsValid)
            {
				var appUserId = User.Identity.GetUserId();
				var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
				savedWorkoutDateTime.UserId = currentUser.UserProfileId;
				db.SavedWorkoutDateTimes.Add(savedWorkoutDateTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(savedWorkoutDateTime);
        }

        // GET: SavedWorkoutDateTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedWorkoutDateTime savedWorkoutDateTime = db.SavedWorkoutDateTimes.Find(id);
            if (savedWorkoutDateTime == null)
            {
                return HttpNotFound();
            }
            return View(savedWorkoutDateTime);
        }

        // POST: SavedWorkoutDateTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SavedWorkoutDateId,Date,WorkoutId,UserId")] SavedWorkoutDateTime savedWorkoutDateTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedWorkoutDateTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(savedWorkoutDateTime);
        }

        // GET: SavedWorkoutDateTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedWorkoutDateTime savedWorkoutDateTime = db.SavedWorkoutDateTimes.Find(id);
            if (savedWorkoutDateTime == null)
            {
                return HttpNotFound();
            }
            return View(savedWorkoutDateTime);
        }

        // POST: SavedWorkoutDateTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavedWorkoutDateTime savedWorkoutDateTime = db.SavedWorkoutDateTimes.Find(id);
            db.SavedWorkoutDateTimes.Remove(savedWorkoutDateTime);
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
