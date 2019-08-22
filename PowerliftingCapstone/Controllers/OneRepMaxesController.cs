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
    public class OneRepMaxesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OneRepMaxes
        public ActionResult Index()
        {
            var oneRepMaxes = db.OneRepMaxes.Include(o => o.User);
            return View(oneRepMaxes.ToList());
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
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName");
            return View();
        }

        // POST: OneRepMaxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OneRepMaxId,Date,Squat,Bench,Deadlift,Total,Wilks,UserId")] OneRepMax oneRepMax)
        {
            if (ModelState.IsValid)
            {
                db.OneRepMaxes.Add(oneRepMax);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName", oneRepMax.UserId);
            return View(oneRepMax);
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
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName", oneRepMax.UserId);
            return View(oneRepMax);
        }

        // POST: OneRepMaxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OneRepMaxId,Date,Squat,Bench,Deadlift,Total,Wilks,UserId")] OneRepMax oneRepMax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oneRepMax).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserProfileId", "FirstName", oneRepMax.UserId);
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
