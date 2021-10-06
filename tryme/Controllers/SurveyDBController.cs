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
    public class SurveyDBController : Controller
    {
        private ExperimentDBContext db = new ExperimentDBContext();

        // GET: SurveyDB
        public ActionResult Index()
        {
            return View(db.SurveyInfos.ToList());
        }

        // GET: SurveyDB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            if (surveyInfo == null)
            {
                return HttpNotFound();
            }
            return View(surveyInfo);
        }

        // GET: SurveyDB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SurveyDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,AssignmentId,HitId,TypeOfExperiment,NumOfSucssess,NumOfFaults,BobNumOfSucssess,BobNumOfFaults,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q01,Q02,Q03,surveyTime,surveyTimeStart,surveyTimeEnd,hitTime,BobNumTrueScore,userNumOfTrueScore,captchaTime")] SurveyInfo surveyInfo)
        {
            if (ModelState.IsValid)
            {
                db.SurveyInfos.Add(surveyInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(surveyInfo);
        }

        // GET: SurveyDB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            if (surveyInfo == null)
            {
                return HttpNotFound();
            }
            return View(surveyInfo);
        }

        // POST: SurveyDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,AssignmentId,HitId,TypeOfExperiment,NumOfSucssess,NumOfFaults,BobNumOfSucssess,BobNumOfFaults,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q01,Q02,Q03,surveyTime,surveyTimeStart,surveyTimeEnd,hitTime,BobNumTrueScore,userNumOfTrueScore,captchaTime")] SurveyInfo surveyInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveyInfo);
        }

        // GET: SurveyDB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            if (surveyInfo == null)
            {
                return HttpNotFound();
            }
            return View(surveyInfo);
        }

        // POST: SurveyDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            db.SurveyInfos.Remove(surveyInfo);
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
