using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExperimentCaptcha.Models;

namespace EmptyPacer4.Controllers
{
    public class DraftUsersController : Controller
    {
        private ExperimentDBContext db = new ExperimentDBContext();

        // GET: DraftUsers
        public ActionResult Index()
        {
            return View(db.DraftUsers.ToList());
        }

        // GET: DraftUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DraftUser draftUser = db.DraftUsers.Find(id);
            if (draftUser == null)
            {
                return HttpNotFound();
            }
            return View(draftUser);
        }

        // GET: DraftUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DraftUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,AssignmentId,HitId,Mobile")] DraftUser draftUser)
        {
            if (ModelState.IsValid)
            {
                db.DraftUsers.Add(draftUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(draftUser);
        }

        // GET: DraftUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DraftUser draftUser = db.DraftUsers.Find(id);
            if (draftUser == null)
            {
                return HttpNotFound();
            }
            return View(draftUser);
        }

        // POST: DraftUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,AssignmentId,HitId,Mobile")] DraftUser draftUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(draftUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(draftUser);
        }

        // GET: DraftUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DraftUser draftUser = db.DraftUsers.Find(id);
            if (draftUser == null)
            {
                return HttpNotFound();
            }
            return View(draftUser);
        }

        // POST: DraftUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DraftUser draftUser = db.DraftUsers.Find(id);
            db.DraftUsers.Remove(draftUser);
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
