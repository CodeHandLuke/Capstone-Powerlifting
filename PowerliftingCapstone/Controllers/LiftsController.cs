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
    public class LiftsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lifts
        public ActionResult Index()
        {
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			DetermineLiftWeights();
			var lifts = db.Lifts.Where(o => o.WorkoutId == currentUser.WorkoutOfDay);
			return View(lifts.ToList());
        }

        // GET: Lifts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lift lift = db.Lifts.Find(id);
            if (lift == null)
            {
                return HttpNotFound();
            }
            return View(lift);
        }

        // GET: Lifts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramId,SetOrder,WorkoutId,Exercise,OneRMPercentage,Reps,Weight,Completed")] Lift lift)
        {
            if (ModelState.IsValid)
            {
                db.Lifts.Add(lift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lift);
        }

        // GET: Lifts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lift lift = db.Lifts.Find(id);
            if (lift == null)
            {
                return HttpNotFound();
            }
            return View(lift);
        }

        // POST: Lifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramId,SetOrder,WorkoutId,Exercise,OneRMPercentage,Reps,Weight,Completed")] Lift lift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lift);
        }

        // GET: Lifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lift lift = db.Lifts.Find(id);
            if (lift == null)
            {
                return HttpNotFound();
            }
            return View(lift);
        }

        // POST: Lifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lift lift = db.Lifts.Find(id);
            db.Lifts.Remove(lift);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		//This method will be used to calculate the how heavy each lift has to be using the lifter's one rep max
		public void DetermineLiftWeights() 
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var oneRepMaxList = db.OneRepMaxes.Where(o => o.UserId == currentUser.UserProfileId).ToList();
			var oneRepMax = oneRepMaxList.LastOrDefault();
			var squatM = oneRepMax.Squat;
			var benchMax = oneRepMax.Bench;
			var deadMax = oneRepMax.Deadlift;
			var foundLifts = db.Lifts.ToList();
			foreach (var item in foundLifts)
			{
				var oneRepMaxMultiplier = item.OneRMPercentage * .01;
				if (item.Exercise == "Squat")
				{
					item.Weight = squatM * oneRepMaxMultiplier;
					db.SaveChanges();
				}

				else if (item.Exercise == "Benchpress")
				{
					item.Weight = benchMax * oneRepMaxMultiplier;
					db.SaveChanges();
				}

				else if (item.Exercise == "Deadlift")
				{
					item.Weight = deadMax * oneRepMaxMultiplier;
					db.SaveChanges();
				}
			}
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
