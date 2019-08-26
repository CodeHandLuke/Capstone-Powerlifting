using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PowerliftingCapstone.Models;

namespace PowerliftingCapstone.Controllers
{
    public class UserProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserProfiles
        public ActionResult Index()
        {
            var userProfiles = db.UserProfiles.Include(u => u.ApplicationUser);
            return View(userProfiles.ToList());
        }

        // GET: UserProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Age,Sex,Weight,Wilks,WorkoutOfDay,ApplicationId")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
				userProfile.ApplicationId = User.Identity.GetUserId();
				userProfile.WorkoutOfDay = 1;
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
				SeedLiftsTable();
				return RedirectToAction("Index"); //Redirect to first workout
            }

            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", userProfile.ApplicationId);
            return View(userProfile);
        }

        // GET: UserProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", userProfile.ApplicationId);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Age,Sex,Weight,Wilks,WorkoutOfDay,ApplicationId")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", userProfile.ApplicationId);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		int setOrder = 1;

		public void SeedLiftsTable()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var liftsCount = db.Lifts.Where(l => l.UserId == currentUser.UserId).Count();
			if (liftsCount < 45)
			{
				SeedWorkoutTableDayOne();
				SeedWorkoutTableDayTwo();
				SeedWorkoutTableDayThree();
				SeedWorkoutTableDayFour();
			}
		}
		public void SeedWorkoutTableDayOne()
		{
			SeedWorkoutTableSquat();
			SeedWorkoutTableBench();
			SeedWorkoutTableSquatSetTwo();
			setOrder = 1;
		}
		public void SeedWorkoutTableSquat()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var squatSets = 5;
			for (int i = 0; i < squatSets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 1;
				newLift.Exercise = "Squat";
				newLift.OneRMPercentage = 50;
				newLift.Reps = 5;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableBench()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var benchSets = 5;
			for (int i = 0; i < benchSets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 1;
				newLift.Exercise = "Benchpress";
				newLift.OneRMPercentage = 50;
				newLift.Reps = 5;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableSquatSetTwo()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var squatSets = 5;
			for (int i = 0; i < squatSets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 1;
				newLift.Exercise = "Squat";
				newLift.OneRMPercentage = 70;
				newLift.Reps = 2;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableDayTwo()
		{
			SeedWorkoutTableDeadlift();
			SeedWorkoutTableBenchTwo();
			SeedWorkoutTableDeadliftTwo();
			setOrder = 1;
		}

		public void SeedWorkoutTableDeadlift()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var deadSets = 5;
			for (int i = 0; i < deadSets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 2;
				newLift.Exercise = "Deadlift";
				newLift.OneRMPercentage = 50;
				newLift.Reps = 5;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableBenchTwo()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 5;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 2;
				newLift.Exercise = "Benchpress";
				newLift.OneRMPercentage = 50;
				newLift.Reps = 6;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableDeadliftTwo()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 4;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 2;
				newLift.Exercise = "Deadlift^Knee";
				newLift.OneRMPercentage = 70;
				newLift.Reps = 4;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableDayThree()
		{
			SeedWorkoutTableBenchThree();
			SeedWorkoutTableSquatThree();
			SeedWorkoutTableBenchThreeTwo();
			setOrder = 1;
		}

		public void SeedWorkoutTableBenchThree()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 5;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 3;
				newLift.Exercise = "Benchpress";
				newLift.OneRMPercentage = 80;
				newLift.Reps = 3;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableSquatThree()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 5;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 3;
				newLift.Exercise = "Squat";
				newLift.OneRMPercentage = 80;
				newLift.Reps = 3;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableBenchThreeTwo()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 5;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 3;
				newLift.Exercise = "Benchpress";
				newLift.OneRMPercentage = 75;
				newLift.Reps = 3;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableDayFour()
		{
			SeedWorkoutTableDeadliftFour();
			SeedWorkoutTableRackpull();
			setOrder = 1;
		}

		public void SeedWorkoutTableDeadliftFour()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 5;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 4;
				newLift.Exercise = "Def Deadlift";
				newLift.OneRMPercentage = 60;
				newLift.Reps = 2;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
			}
		}

		public void SeedWorkoutTableRackpull()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var sets = 5;
			for (int i = 0; i < sets; i++)
			{
				Lift newLift = new Lift();
				newLift.SetOrder = setOrder;
				newLift.WorkoutId = 4;
				newLift.Exercise = "Rackpull";
				newLift.OneRMPercentage = 75;
				newLift.Reps = 4;
				newLift.Completed = false;
				newLift.UserId = currentUser.UserId;
				db.Lifts.Add(newLift);
				db.SaveChanges();
				setOrder++;
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
