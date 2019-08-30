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
    public class ActualProgramTotalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActualProgramTotals
        public ActionResult Index()
        {
            var actualProgramTotals = db.ActualProgramTotals.Include(a => a.User);
            return View(actualProgramTotals.ToList());
        }

        // GET: ActualProgramTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActualProgramTotal actualProgramTotal = db.ActualProgramTotals.Find(id);
            if (actualProgramTotal == null)
            {
                return HttpNotFound();
            }
            return View(actualProgramTotal);
        }

        // GET: ActualProgramTotals/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName");
            return View();
        }

        // POST: ActualProgramTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActualTotalId,Exercise,Reps,Weight,SavedWorkoutDateId,UserId")] ActualProgramTotal actualProgramTotal)
        {
            if (ModelState.IsValid)
            {
                db.ActualProgramTotals.Add(actualProgramTotal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", actualProgramTotal.UserId);
            return View(actualProgramTotal);
        }

        // GET: ActualProgramTotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActualProgramTotal actualProgramTotal = db.ActualProgramTotals.Find(id);
            if (actualProgramTotal == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", actualProgramTotal.UserId);
            return View(actualProgramTotal);
        }

        // POST: ActualProgramTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActualTotalId,Exercise,Reps,Weight,SavedWorkoutDateId,UserId")] ActualProgramTotal actualProgramTotal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actualProgramTotal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "FirstName", actualProgramTotal.UserId);
            return View(actualProgramTotal);
        }

        // GET: ActualProgramTotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActualProgramTotal actualProgramTotal = db.ActualProgramTotals.Find(id);
            if (actualProgramTotal == null)
            {
                return HttpNotFound();
            }
            return View(actualProgramTotal);
        }

        // POST: ActualProgramTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActualProgramTotal actualProgramTotal = db.ActualProgramTotals.Find(id);
            db.ActualProgramTotals.Remove(actualProgramTotal);
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
