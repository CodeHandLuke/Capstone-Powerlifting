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
    public class OneRepMaxesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

		// GET: OneRepMaxes
		public ActionResult Index()
        {
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
            var oneRepMaxes = db.OneRepMaxes.Where(o => o.UserId == currentUser.UserId);
            return View(oneRepMaxes.ToList());
        }

		public ActionResult ShareableMax(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OneRepMax oneRepMax = db.OneRepMaxes.Find(id);
			if (oneRepMax == null)
			{
				return HttpNotFound();
			}
			return View(oneRepMax);
		}

        // GET: OneRepMaxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OneRepMax oneRepMax = db.OneRepMaxes.Find(id);
            if (oneRepMax == null)
            {
                return HttpNotFound();
            }
            return View(oneRepMax);
        }

        // GET: OneRepMaxes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName");
            return View();
        }

        // POST: OneRepMaxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OneRepMaxId,Squat,Bench,Deadlift")] OneRepMax oneRepMax)
        {
            if (ModelState.IsValid)
            {
				var appUserId = User.Identity.GetUserId();
				var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();

				oneRepMax.Date = DateTime.Now;
				oneRepMax.Total = oneRepMax.Squat + oneRepMax.Bench + oneRepMax.Deadlift;
				var wilksCoefficient = CalcuateWilks(oneRepMax.Total);
				oneRepMax.Wilks = Math.Round(wilksCoefficient, 2);
				currentUser.Wilks = oneRepMax.Wilks;
				oneRepMax.UserId = currentUser.UserId;
                db.OneRepMaxes.Add(oneRepMax);
                db.SaveChanges();
                return RedirectToAction("InitializeWorkout", "Lifts");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", oneRepMax.UserId);
            return View(oneRepMax);
        }

		public double CalcuateWilks(double total)
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			double wilksCoefficient = 0;
			double x = currentUser.Weight;
			double y = x * x;
			double z = x * y;
			if (currentUser.Sex == "Male")
			{
				double a = -216.0475144;
				double b = 16.2606339;
				double c = -0.002388645;
				double d = -0.00113732;
				double e = 7.01863E-06;
				double f = -1.291E-08;

				wilksCoefficient += total * 500 / (a + (b * x) + (c * y) + (d * z) + (e * (x * z)) + (f * (y * z)));
			}

			else if (currentUser.Sex == "Female")
			{
				double a = 594.31747775582;
				double b = -27.23842536447;
				double c = 0.82112226871;
				double d = -0.00930733913;
				double e = 4.731582E-05;
				double f = -1.291E-08;

				wilksCoefficient += total * 500 / (a + (b * x) + (c * y) + (d * z) + (e * (x * z)) + (f * (y * z)));
			}
			return wilksCoefficient;
		}

        // GET: OneRepMaxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OneRepMax oneRepMax = db.OneRepMaxes.Find(id);
            if (oneRepMax == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", oneRepMax.UserId);
            return View(oneRepMax);
        }

        // POST: OneRepMaxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OneRepMaxId,Date,Squat,Bench,Deadlift,Total,Wilks")] OneRepMax oneRepMax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oneRepMax).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", oneRepMax.UserId);
            return View(oneRepMax);
        }

        // GET: OneRepMaxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OneRepMax oneRepMax = db.OneRepMaxes.Find(id);
            if (oneRepMax == null)
            {
                return HttpNotFound();
            }
            return View(oneRepMax);
        }

        // POST: OneRepMaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OneRepMax oneRepMax = db.OneRepMaxes.Find(id);
            db.OneRepMaxes.Remove(oneRepMax);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		public ActionResult Leaderboard()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var oneRepMaxes = db.OneRepMaxes.Include(o => o.User);
			return View(oneRepMaxes.ToList());
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		//*** D3 line graph showing 1RM progress by month

		public ActionResult OneRepMaxProgress()
		{
			return View();
		}

		public string CreateLineGraph()
		{
			List<ExerciseObjectViewModel> exerciseObjectViewModels = new List<ExerciseObjectViewModel>();
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var squatMax = CreateSquatObject();
			var benchMax = CreateBenchObject();
			var deadliftMax = CreateDeadliftObject();
			exerciseObjectViewModels.Add(squatMax);
			exerciseObjectViewModels.Add(benchMax);
			exerciseObjectViewModels.Add(deadliftMax);
			return JsonConvert.SerializeObject(exerciseObjectViewModels);
		}

		public ExerciseObjectViewModel CreateSquatObject()
		{
			ExerciseObjectViewModel squatMax = new ExerciseObjectViewModel();
			squatMax.Name = "Squat";
			squatMax.OneRepMaxValues = CreateOneRepMaxSquatValues();
			return squatMax;
		}

		public ExerciseObjectViewModel CreateBenchObject()
		{
			ExerciseObjectViewModel benchMax = new ExerciseObjectViewModel();
			benchMax.Name = "Bench";
			benchMax.OneRepMaxValues = CreateOneRepMaxBenchValues();
			return benchMax;
		}

		public ExerciseObjectViewModel CreateDeadliftObject()
		{
			ExerciseObjectViewModel deadliftMax = new ExerciseObjectViewModel();
			deadliftMax.Name = "Deadlift";
			deadliftMax.OneRepMaxValues = CreateOneRepMaxDeadliftValues();
			return deadliftMax;
		}

		public List<OneRepMaxValueViewModel> CreateOneRepMaxSquatValues()
		{
			List<OneRepMaxValueViewModel> listOneRepMaxes = new List<OneRepMaxValueViewModel>();
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var id = currentUser.UserId;
			var squatMaxes = db.OneRepMaxes.Where(s => s.UserId == id);
			foreach (var item in squatMaxes)
			{
				OneRepMaxValueViewModel oneRepMaxSquat = new OneRepMaxValueViewModel();
				oneRepMaxSquat.Date = item.Date.ToString("MMyyyy");
				oneRepMaxSquat.Weight = item.Squat;
				listOneRepMaxes.Add(oneRepMaxSquat);
			}
			return listOneRepMaxes;
		}

		public List<OneRepMaxValueViewModel> CreateOneRepMaxBenchValues()
		{
			List<OneRepMaxValueViewModel> listOneRepMaxes = new List<OneRepMaxValueViewModel>();
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var id = currentUser.UserId;
			var benchMaxes = db.OneRepMaxes.Where(s => s.UserId == id);
			foreach (var item in benchMaxes)
			{
				OneRepMaxValueViewModel oneRepMaxBench = new OneRepMaxValueViewModel();
				oneRepMaxBench.Date = item.Date.ToString("MMyyyy");
				oneRepMaxBench.Weight = item.Bench;
				listOneRepMaxes.Add(oneRepMaxBench);
			}
			return listOneRepMaxes;
		}

		public List<OneRepMaxValueViewModel> CreateOneRepMaxDeadliftValues()
		{
			List<OneRepMaxValueViewModel> listOneRepMaxes = new List<OneRepMaxValueViewModel>();
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var id = currentUser.UserId;
			var deadliftMaxes = db.OneRepMaxes.Where(s => s.UserId == id);
			foreach (var item in deadliftMaxes)
			{
				OneRepMaxValueViewModel oneRepMaxDeadlift = new OneRepMaxValueViewModel();
				oneRepMaxDeadlift.Date = item.Date.ToString("MMyyyy");
				oneRepMaxDeadlift.Weight = item.Deadlift;
				listOneRepMaxes.Add(oneRepMaxDeadlift);
			}
			return listOneRepMaxes;
		}
	}
}
