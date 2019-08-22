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
    public class WeeklyTotalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WeeklyTotals
        public ActionResult Index()
        {
            var weeklyTotals = db.WeeklyTotals.Include(w => w.User);
            return View(weeklyTotals.ToList());
        }

        // GET: WeeklyTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeeklyTotal weeklyTotal = db.WeeklyTotals.Find(id);
            if (weeklyTotal == null)
            {
                return HttpNotFound();
            }
            return View(weeklyTotal);
        }

        // GET: WeeklyTotals/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName");
            return View();
        }

        // POST: WeeklyTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WeeklyTotalId,Week,Exercise,Reps,Weight,UserId")] WeeklyTotal weeklyTotal)
        {
            if (ModelState.IsValid)
            {
                db.WeeklyTotals.Add(weeklyTotal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName", weeklyTotal.UserId);
            return View(weeklyTotal);
        }

        // GET: WeeklyTotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeeklyTotal weeklyTotal = db.WeeklyTotals.Find(id);
            if (weeklyTotal == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName", weeklyTotal.UserId);
            return View(weeklyTotal);
        }

        // POST: WeeklyTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WeeklyTotalId,Week,Exercise,Reps,Weight,UserId")] WeeklyTotal weeklyTotal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weeklyTotal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName", weeklyTotal.UserId);
            return View(weeklyTotal);
        }

        // GET: WeeklyTotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeeklyTotal weeklyTotal = db.WeeklyTotals.Find(id);
            if (weeklyTotal == null)
            {
                return HttpNotFound();
            }
            return View(weeklyTotal);
        }

        // POST: WeeklyTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeeklyTotal weeklyTotal = db.WeeklyTotals.Find(id);
            db.WeeklyTotals.Remove(weeklyTotal);
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
