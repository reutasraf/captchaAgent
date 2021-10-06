using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExperimentCaptcha.Models;

namespace tryme.Controllers
{
    public class SecretDBController : Controller
    {
        private ExperimentDBContext db = new ExperimentDBContext();

        // GET: SecretDB
        public ActionResult Index()
        {
            return View(db.Experiments.ToList());
        }

        // GET: SecretDB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experiment experiment = db.Experiments.Find(id);
            if (experiment == null)
            {
                return HttpNotFound();
            }
            return View(experiment);
        }

        // GET: SecretDB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecretDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,AssignmentId,TypeOfExperiment,NumOfSucssess,NumOfFaults,Time,CountChecker,CountCheckerBob,Goods,Bads,Bobs,Paces,BobNumOfSucssess,BobNumOfFaults,BobTime,HitId,BeginTime,FinishTime,Mobile,Gender,Age,Education,Country,NumOfTries,IP,startTimeGame,Instruction_time,quizTime")] Experiment experiment)
        {
            if (ModelState.IsValid)
            {
                db.Experiments.Add(experiment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(experiment);
        }

        // GET: SecretDB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experiment experiment = db.Experiments.Find(id);
            if (experiment == null)
            {
                return HttpNotFound();
            }
            return View(experiment);
        }

        // POST: SecretDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,AssignmentId,TypeOfExperiment,NumOfSucssess,NumOfFaults,Time,CountChecker,CountCheckerBob,Goods,Bads,Bobs,Paces,BobNumOfSucssess,BobNumOfFaults,BobTime,HitId,BeginTime,FinishTime,Mobile,Gender,Age,Education,Country,NumOfTries,IP,startTimeGame,Instruction_time,quizTime")] Experiment experiment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experiment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(experiment);
        }

        // GET: SecretDB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experiment experiment = db.Experiments.Find(id);
            if (experiment == null)
            {
                return HttpNotFound();
            }
            return View(experiment);
        }

        // POST: SecretDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experiment experiment = db.Experiments.Find(id);
            db.Experiments.Remove(experiment);
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
