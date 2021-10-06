using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpCap.Models;
using ExperimentCaptcha.Models;

namespace ExperimentCaptcha.Controllers
{
    public class ExperimentsController : Controller
    {
        private ExperimentDBContext db = new ExperimentDBContext();
        private PyhtonModel pyModel = new PyhtonModel("C:/inetpub/wwwroot/HelperPacer/PythonPacers/main.py");
        //private PyhtonModel pyModel = new PyhtonModel("/PythonPacers/main.py");
        public static String TIME_STAMP_FORMAT = "dd.MM.yyyy HH:mm:ss";



        // GET: Experiments
        public ActionResult Index()
        {
            string jj= getTimeStamp();
            Session["startTask"] = jj;
            string mobile = "not_mobile";
            if (Session["IsMobileDevice_"] == null)
            {
                return View("rr");
            }
            if ((bool)Session["IsMobileDevice_"])
            {
                mobile = "mobile_user";
            }

            var assignmentId = Session["assignmentId_"];

            Session["nameOfEXperiment"] = "";
            Session["good_"] = "";
            Session["bad_"] = "";
            Session["bob_"] = "";
            Session["paces_"] = "";

            Session["q1"] = "To what extent did you find the virtual player to be a competent partner?";
            Session["q2"] = "Please explain your answer.";
            Session["q3"] = "To what extent do you trust the virtual player?";
            Session["q4"] = "To what extent are you satisfied with the virtual player?";
            Session["q5"] = "To what extent do you find the virtual player to be likeable?"; 
            Session["q6"] = "To what extent would you recommend the virtual player to a friend, as a partner to work with?";
            Session["q7"] = "If you could choose - would you rather play with or without the virtual player?";
            
            Session["q8"] = "Please state any other thoughts you have about the virtual player.";

            Session["q01"] = "Please state your general opinion and comments about the task. If you have any suggestions for improvement in the task/HIT, they are also welcome.";
            Session["q02"] = "Did you find any bugs in the HIT? If so, what are they?";

            Session["IP"] =  GetIPAddress();
            //friend assignment

            if (assignmentId == null)
            {
                Session["user_id"] = "friend1";
                Session["turkAss"] = "turkAss";
                Session["hitId"] = "hit id friend";
                Session["mobile"] = mobile;
                //assignmentId = "aaa"; //DEBUG FOR MYSELF
            }

            else if (Session["assignmentId_"].ToString().Equals("ASSIGNMENT_ID_NOT_AVAILABLE"))
            {
                return View();
            }
            else
            {
                string userId = Session["workerId_"].ToString();

                string UserId = null;

                

                DraftUser[] arr = db.DraftUsers.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    DraftUser du = arr[i];
                    if (du.UserID.Equals(userId))
                    {
                        UserId = userId;
                    }
                }


                

                if (UserId != null && UserId != "") //already participated
                {
                    return RedirectToAction("Redirect");
                }
                if (userId == null)
                    userId = "";
                Session["user_id"] = userId;   //save participant's user ID
                Session["turkAss"] = Session["assignmentId_"]; ;  //save participant's assignment ID
                Session["hitId"] = Session["hitId_"];
                //Session["BeginTime"] = DateTime.Now.ToString();  
                Session["mobile"] = mobile;
            }

            DraftUser draftUser = new DraftUser();
            draftUser.UserID = Session["user_id"].ToString();
            draftUser.AssignmentId = Session["turkAss"].ToString();
            draftUser.HitId = Session["hitId"].ToString();
            draftUser.Mobile = Session["mobile"].ToString();
            if (ModelState.IsValid)
            {
                //System.Diagnostics.Debug.WriteLine("user is:"+draftUser.UserID);
                //must uncomment!!!

                db.DraftUsers.Add(draftUser);
                db.SaveChanges();
            }
            //add_data();






            return View();
        }

        public void add_data()
        {

            using (var reader = new System.IO.StreamReader(@"C:/inetpub/wwwroot/HelperPacer/add.csv"))
            {
               
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    DraftUser dd = new DraftUser();
                    dd.UserID = line.ToString();
                    dd.AssignmentId = Session["turkAss"].ToString();
                    dd.HitId = Session["hitId"].ToString();
                    dd.Mobile = Session["mobile"].ToString();
                    if (ModelState.IsValid)
                    {
                        //System.Diagnostics.Debug.WriteLine("user is:"+draftUser.UserID);
                        //must uncomment!!!

                        db.DraftUsers.Add(dd);
                        db.SaveChanges();
                    }
                }
            }



            
        }
        


        [HttpPost]
        public ActionResult SaveData(int NumOfSucssess,
        int NumOfFaults,
        int Time,
        string CountChecker,
        string Timeline,
        string TimelineBob,
        int BobNumOfSucssess,
        int BobNumOfFaults,
        int BobTime,
        int pace,
        int finF,
        int interval,
        string start,
        int elapsed,
        string name)
        {

            ///from here:
            //System.Diagnostics.Debug.WriteLine(NumOfSucssess);
            //System.Diagnostics.Debug.WriteLine(NumOfFaults);
            //string end_instructions = getTimeStamp();
            System.Diagnostics.Debug.WriteLine(Timeline);
            System.Diagnostics.Debug.WriteLine(CountChecker);
            //System.Diagnostics.Debug.WriteLine(BobNumOfSucssess);
            //System.Diagnostics.Debug.WriteLine(BobNumOfFaults);
            //System.Diagnostics.Debug.WriteLine(BobTime);
            string info = "{" +
                "\'NumOfSucssess\':" + NumOfSucssess.ToString() +
                ",\'NumOfFaults\':" + NumOfFaults.ToString() +
                ",\'BobNumOfSucssess\':" + BobNumOfSucssess.ToString() +
                ",\'CountChecker\':" + "\'" + CountChecker + "\'" +
                ",\'Timeline\':" + "\'" + Timeline + "\'" +
                ",\'TimelineBob\':" + "\'" + TimelineBob + "\'" +
                ",\'pace\':" + pace.ToString() +
                ",\'interval\':" + interval.ToString() +
                ",\'StartPoint\':" + "\'" + start + "\'" +
                ",\'ElapsedTime\':" + elapsed.ToString() +
                "}";


            




            string[] vals = CountChecker.Split(',');
            //DataProgressModel.good.Add(vals[0]);
            //DataProgressModel.bad.Add(vals[1]);
            //DataProgressModel.bob.Add(vals[2]);
            //DataProgressModel.paces.Add(pace.ToString());
            Session["good_"] += "# " + vals[0];
            Session["bad_"] += "# " + vals[1];
            Session["bob_"] += "# " + vals[2];
            Session["paces_"] += "# " + pace.ToString();

            string s = Session["gender"].ToString();


            if (finF == 0) // not finished. need to update pace.
            {
                int result = 7500;
                //return Json(true);
                return Json(new { newPace = result });
            }

            //till here.
            

            System.Diagnostics.Debug.WriteLine("This should only be printed at the end!");
            //string goods = DataProgressModel.Get_str(DataProgressModel.good);
            //string bads = DataProgressModel.Get_str(DataProgressModel.bad);
            //string bobs = DataProgressModel.Get_str(DataProgressModel.bob);
            //string paces = DataProgressModel.Get_str(DataProgressModel.paces);
            //System.Diagnostics.Debug.WriteLine(goods);
            //System.Diagnostics.Debug.WriteLine(bads);
            //System.Diagnostics.Debug.WriteLine(bobs);
            //System.Diagnostics.Debug.WriteLine(paces);

            Session["NumOfSucssess"] = NumOfSucssess.ToString();
            Session["NumOfFaults"] = NumOfFaults.ToString();
            Session["BobNumOfSucssess"] = BobNumOfSucssess.ToString();
            Session["BobNumOfFaults"] = BobNumOfFaults.ToString();
            string ins_time = GetTimeDiff(Session["startTask"].ToString(), Session["start_game"].ToString());

            Experiment exp = new Experiment();
            exp.NumOfSucssess = NumOfSucssess;
            exp.NumOfFaults = NumOfFaults;
            exp.Time = Time;
            exp.CountChecker = Timeline;
            exp.CountCheckerBob = TimelineBob;
            //--new--
            exp.Goods = Session["good_"].ToString();
            exp.Bads = Session["bad_"].ToString();
            exp.Bobs = Session["bob_"].ToString();
            exp.Paces = Session["paces_"].ToString();
            // --end of new--
            exp.BobNumOfSucssess= BobNumOfSucssess;
            exp.BobNumOfFaults = BobNumOfFaults;
            exp.BobTime = BobTime;

            exp.Gender = Session["gender"].ToString();

            exp.UserID =  Session["user_id"].ToString();
            exp.AssignmentId= Session["turkAss"].ToString();
            exp.HitId=Session["hitId"].ToString();
            exp.BeginTime= Session["start_game"].ToString();  
            exp.Mobile=Session["mobile"].ToString();
            exp.Gender=Session["gender"].ToString();
            exp.Age=Session["age"].ToString();
            exp.Education=Session["education"].ToString();
            exp.Country=Session["country"].ToString();
            //string jj = Session["NumOfFaultsQuestions"];
            exp.NumOfTries = Session["NumOfFaultsQuestions"].ToString();
            exp.IP = Session["IP"].ToString();
            exp.startTimeGame = Session["start_game"].ToString();
            exp.Instruction_time = ins_time;
            exp.quizTime = GetTimeDiff(Session["start_game"].ToString(), getTimeStamp());
            //exp.TypeOfExperiment = "FirstTimedPacerNotUSA";
            Session["TypeOfExperiment"] = name;
            exp.TypeOfExperiment = name;



            exp.FinishTime= DateTime.Now.ToString();
            //return Json(true);


            if (ModelState.IsValid)
            {
                db.Experiments.Add(exp);
                //db.SaveChanges();
                DraftUser draftUser = db.DraftUsers//the question mark here makes the int nullable
                .Where(c => c.UserID == exp.UserID)
                .First();

                if (draftUser != null)
                {
                    //db.DraftUsers.Remove(draftUser); 
                }
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return Json(true);
        }



        public static String getTimeStamp()
        {
            TimeZoneInfo israelTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
            DateTime utc = DateTime.UtcNow;
            DateTime dateTimeEnd = TimeZoneInfo.ConvertTimeFromUtc(utc, israelTimeZone);
            return dateTimeEnd.ToString(TIME_STAMP_FORMAT);
        }

        public static String GetTimeDiff(String time_start, String time_end)
        {
            DateTime dateTimeStart = DateTime.ParseExact(time_start, TIME_STAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dateTimeEnd = DateTime.ParseExact(time_end, TIME_STAMP_FORMAT, System.Globalization.CultureInfo.InvariantCulture);

            TimeSpan span = dateTimeEnd.Subtract(dateTimeStart);
            String hours = timeToStr(span.Hours);
            String minutes = timeToStr(span.Minutes);
            String seconds = timeToStr(span.Seconds);

            String diff = hours + ":" + minutes + ":" + seconds;
            return diff;
        }

        public static String timeToStr(long timeUnit)
        {
            String str = timeUnit + "";
            if (str.Length == 1)
            {
                str = "0" + str;
            }
            return str;
        }



        [HttpPost]
        public ActionResult SendPersonalInfo(String[] answers, String[] additionalAnswers)
        {
            
            Session["q1"]= answers[0];
            Session["q2"] = answers[1];
            Session["q3"] = answers[2];
            Session["q4"] = answers[3];
            Session["q5"] = answers[4];
            Session["q6"] = answers[5];
            Session["q7"] = answers[6];
            Session["q8"] = answers[7];
            Session["q9"] = additionalAnswers[0];
            Session["q10"] = additionalAnswers[1];
            return Json(true);
        }


        [HttpPost]
        public ActionResult SendPersonalInfoCont(String[] answers)
        {
            string end_time_survey = getTimeStamp();
            string time_sur = GetTimeDiff(Session["start_time_survey"].ToString(), end_time_survey);
            string time_task = GetTimeDiff(Session["startTask"].ToString(), end_time_survey);

            //return Json(true);
            SurveyInfo si = new SurveyInfo();
            if (Session["user_id"] == null || Session["turkAss"] == null || Session["hitId"] == null)
            {
                si.UserID = "";
                si.AssignmentId = "";
                si.HitId = "";
            }
            else
            {
                si.UserID = Session["user_id"].ToString();
                si.AssignmentId = Session["turkAss"].ToString();
                si.HitId = Session["hitId"].ToString();
            }

            
            si.TypeOfExperiment = Session["TypeOfExperiment"].ToString();
            si.NumOfSucssess = Session["NumOfSucssess"].ToString();
            si.NumOfFaults = Session["NumOfFaults"].ToString();
            si.BobNumOfSucssess = Session["BobNumOfSucssess"].ToString();
            si.BobNumOfFaults = Session["BobNumOfFaults"].ToString();

           
            si.Q1 = Session["q1"].ToString();
            si.Q2 = Session["q2"].ToString();
            si.Q3 = Session["q3"].ToString();
            si.Q4 = Session["q4"].ToString();
            si.Q5 = Session["q5"].ToString();
            si.Q6 = Session["q6"].ToString();
            si.Q7 = Session["q7"].ToString();
            si.Q8 = Session["q8"].ToString();
            si.Q01 = Session["q9"].ToString();
            si.Q02 = Session["q10"].ToString();
            si.Q03 = answers[0].ToString();
            si.surveyTime = time_sur;
            si.surveyTimeStart = Session["start_time_survey"].ToString();
            si.surveyTimeEnd = end_time_survey;
            si.hitTime = time_task;

            if (ModelState.IsValid)
            {
                db.SurveyInfos.Add(si);

                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return Json(true);
        }

        protected string GetIPAddress()
        {
            //If I use the Binding Address Localhost:5000 then the IP is returned as "::1" (Localhost IPv6 address). If I bind my Webapi on the IP Address and try to reach it from another client computer, I get Client's IP Address in API Response.
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }


        public ActionResult PersonalDetailsData(string gender, string age, string education, string country,int numOfTries)
        {

            Session["gender"] = gender;
            Session["age"] = age;
            Session["education"] = education;
            Session["country"] = country;
            Session["BeginTime"] = DateTime.Now.ToString();
            Session["NumOfFaultsQuestions"] = numOfTries;
            Session["start_game"] = getTimeStamp();

            

            return Json(true);
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult Survey()
        {
            Session["start_time_survey"] = getTimeStamp();
            return View();
        }
        public ActionResult SurveyCont()
        {
            return View();
        }

        public ActionResult Redirect()
        {
            return View();
        }

        public ActionResult Err()
        {
            return View();
        }


        public ActionResult RedirectToCancel()
        {
            return View();
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
