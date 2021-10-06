using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ExperimentCaptcha.Models
{
    public class Experiment
    {
        public int ID { set; get; }

        public string UserID { set; get; }

        public string AssignmentId { set; get; }

        public string TypeOfExperiment { set; get; }

        public int NumOfSucssess { set; get; }

        public int NumOfFaults { set; get; }

        public int Time { set; get; }

        public string CountChecker { set; get; }

        public string CountCheckerBob { set; get; }
        //new properties
        public string Goods { set; get; }
        public string Bads { set; get; }
        public string Bobs { set; get; }
        public string Paces { set; get; }
        //end of new properties
        
        public int BobNumOfSucssess { set; get; }

        public int BobNumOfFaults { set; get; }

        public int BobTime { set; get; }

        public string HitId { set; get; }
        public string BeginTime { set; get; }
        public string FinishTime { set; get; }
        public string Mobile { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string Education { set; get; }
        public string Country { set; get; }
        public string NumOfTries { set; get; }
        public string IP { set; get; }
        public string startTimeGame { set; get; }
        public string Instruction_time { set; get; }
        public string quizTime { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var info in this.GetType().GetProperties())
            {
                var value = info.GetValue(this, null) ?? "(null)";
                //sb.AppendLine(info.Name + ": " + value.ToString());
                sb.Append(value.ToString() + ",");
            }
            string s = sb.ToString();
            return s.Remove(s.Length - 1);
        }
    
    }


    public class SurveyInfo
    {
        public int ID { set; get; }

        public string UserID { get; set; }

        public string AssignmentId { get; set; }

        public string HitId { get; set; }

        public string TypeOfExperiment { get; set; }

        public string NumOfSucssess { get; set; }
        public string NumOfFaults { get; set; }
        public string BobNumOfSucssess { get; set; }
        public string BobNumOfFaults { get; set; }


        public string Q1 { get; set; }

        public string Q2 { get; set; }

        public string Q3 { get; set; }

        public string Q4 { get; set; }

        public string Q5 { get; set; }

        public string Q6 { get; set; }

        public string Q7 { get; set; }

        public string Q8 { get; set; }

        public string Q01 { get; set; }

        public string Q02 { get; set; }

        public string Q03 { get; set; }
        public string surveyTime { get; set; }
        public string surveyTimeStart { get; set; }
        public string surveyTimeEnd { get; set; }
        public string hitTime { get; set; }
        public string BobNumTrueScore { get; set; }
        public string userNumOfTrueScore { get; set; }
        public string captchaTime { get; set; }
        



    }

    public class ExperimentDBContext : DbContext
    {

        public ExperimentDBContext() : base("reut21")
        {

        }

        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<SurveyInfo> SurveyInfos { get; set; }
        public DbSet<DraftUser> DraftUsers { get; set; }
        
    }
}