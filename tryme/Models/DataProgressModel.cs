using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpCap.Models
{
    public class DataProgressModel
    {
        public static List<string> good = new List<string>();
        public static List<string> bad = new List<string>();
        public static List<string> bob = new List<string>();
        public static List<string> paces = new List<string>();


        public static string Get_str(List<string> l)
        {
            string sep = "", s = "";
            foreach(var e in l)
            {
                s += sep + e;
                sep = ",";
            }
            return s;
        }
    }
}