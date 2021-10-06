using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperimentCaptcha.Models
{
    public class DraftUser
    {
        public int ID { set; get; }

        public string UserID { set; get; }

        public string AssignmentId { set; get; }

        public string HitId { set; get; }

        public string Mobile { set; get; }
    }
}