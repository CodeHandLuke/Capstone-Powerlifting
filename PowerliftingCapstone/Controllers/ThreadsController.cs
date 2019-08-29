using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PowerliftingCapstone.Models;

namespace PowerliftingCapstone.Controllers
{
    public class ThreadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Threads
        public ActionResult Index()
        {
            return View(db.Threads.OrderByDescending(t => t.LastPost).ToList());
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }

			var userId = ReturnCurrentUserId();
			var userName = ReturnUserName(userId);
			ViewBag.UserName = userName;

			thread.ThreadPosts = db.Posts.Where(p => p.ThreadId == id).ToList();

            return View(thread);
        }

        // GET: Threads/Create
        public ActionResult Create()
        {
            return View();
        }

		public int ReturnCurrentUserId()
		{
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var id = currentUser.UserId;
			return id;
		}

		public string ReturnUserName(int id)
		{
			var currentUser = db.UserProfiles.Where(u => u.UserId == id).FirstOrDefault();
			var userName = $"{currentUser.FirstName} {currentUser.LastName}";
			return userName;
		}

		// POST: Threads/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
		//"ThreadId,DateTime,PostedBy,ThreadTitle,Posts,LastPost"
        public ActionResult Create([Bind(Include = "ThreadId,ThreadTitle")] Thread thread)
        {
            if (ModelState.IsValid)
            {
				var id = ReturnCurrentUserId();
				var userName = ReturnUserName(id);
				thread.DateTime = DateTime.Now;
				thread.PostedBy = userName;
				thread.Posts = 0;
				thread.LastPost = DateTime.Now;
                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thread);
        }

        // GET: Threads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadId,DateTime,PostedBy,ThreadTitle,Posts,LastPost")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thread);
        }

        // GET: Threads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Threads.Find(id);
            db.Threads.Remove(thread);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpPost, ActionName("Post")]
		public ActionResult Post(Thread thread, string postText)
		{
			
			if (thread == null)
			{
				return HttpNotFound();
			}
			var id = thread.ThreadId;
			var foundThread = db.Threads.Where(t => t.ThreadId == id).FirstOrDefault();
			var appUserId = User.Identity.GetUserId();
			var currentUser = db.UserProfiles.Where(u => u.ApplicationId == appUserId).FirstOrDefault();
			var userProfileId = currentUser.UserId;
			var newPost = new Post();

			foundThread.Posts++;
			foundThread.LastPost = DateTime.Now;

			newPost.DateTime = DateTime.Now;
			newPost.UserId = userProfileId;
			newPost.ThreadId = thread.ThreadId;
			newPost.PostText = postText;

			db.Posts.Add(newPost);
			db.SaveChanges();

			return RedirectToAction("Details", new { id });
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
