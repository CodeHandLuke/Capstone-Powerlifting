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
    public class ExpectedProgramTotalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExpectedProgramTotals
        public ActionResult Index()
        {
            return View(db.ExpectedProgramTotals.ToList());
        }

        // GET: ExpectedProgramTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpectedProgramTotal expectedProgramTotal = db.ExpectedProgramTotals.Find(id);
            if (expectedProgramTotal == null)
            {
                return HttpNotFound();
            }
            return View(expectedProgramTotal);
        }

        // GET: ExpectedProgramTotals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpectedProgramTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpectedTotalId,Exercise,Reps,Weight")] ExpectedProgramTotal expectedProgramTotal)
        {
            if (ModelState.IsValid)
            {
                db.ExpectedProgramTotals.Add(expectedProgramTotal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expectedProgramTotal);
        }

        // GET: ExpectedProgramTotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpectedProgramTotal expectedProgramTotal = db.ExpectedProgramTotals.Find(id);
            if (expectedProgramTotal == null)
            {
                return HttpNotFound();
            }
            return View(expectedProgramTotal);
        }

        // POST: ExpectedProgramTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpectedTotalId,Exercise,Reps,Weight")] ExpectedProgramTotal expectedProgramTotal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expectedProgramTotal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expectedProgramTotal);
        }

        // GET: ExpectedProgramTotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpectedProgramTotal expectedProgramTotal = db.ExpectedProgramTotals.Find(id);
            if (expectedProgramTotal == null)
            {
                return HttpNotFound();
            }
            return View(expectedProgramTotal);
        }

        // POST: ExpectedProgramTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpectedProgramTotal expectedProgramTotal = db.ExpectedProgramTotals.Find(id);
            db.ExpectedProgramTotals.Remove(expectedProgramTotal);
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
