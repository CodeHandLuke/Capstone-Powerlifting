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
    public class SavedWorkoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SavedWorkouts
        public ActionResult Index()
        {
            var savedWorkouts = db.SavedWorkouts.Include(s => s.User);
            return View(savedWorkouts.ToList());
        }

        // GET: SavedWorkouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedWorkout savedWorkout = db.SavedWorkouts.Find(id);
            if (savedWorkout == null)
            {
                return HttpNotFound();
            }
            return View(savedWorkout);
        }

        // GET: SavedWorkouts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName");
            return View();
        }

        // POST: SavedWorkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SavedWorkoutId,Date,Exercise,OneRMPercentage,Reps,Weight,WorkoutId,NoteText,SavedWorkoutDateId,UserId")] SavedWorkout savedWorkout)
        {
            if (ModelState.IsValid)
            {
				var appUserId = User.Identity.GetUserId();
				var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
				savedWorkout.UserId = currentUser.UserId;
				db.SavedWorkouts.Add(savedWorkout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", savedWorkout.UserId);
            return View(savedWorkout);
        }

        // GET: SavedWorkouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedWorkout savedWorkout = db.SavedWorkouts.Find(id);
            if (savedWorkout == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", savedWorkout.UserId);
            return View(savedWorkout);
        }

        // POST: SavedWorkouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SavedWorkoutId,Date,Exercise,OneRMPercentage,Reps,Weight,WorkoutId,NoteText,SavedWorkoutDateId,UserId")] SavedWorkout savedWorkout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedWorkout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", savedWorkout.UserId);
            return View(savedWorkout);
        }

        // GET: SavedWorkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedWorkout savedWorkout = db.SavedWorkouts.Find(id);
            if (savedWorkout == null)
            {
                return HttpNotFound();
            }
            return View(savedWorkout);
        }

        // POST: SavedWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavedWorkout savedWorkout = db.SavedWorkouts.Find(id);
            db.SavedWorkouts.Remove(savedWorkout);
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
