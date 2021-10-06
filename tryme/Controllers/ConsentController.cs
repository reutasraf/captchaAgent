using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyPacer4.Controllers
{
    public class ConsentController : Controller
    {
        // GET: Consent
        public ActionResult Index()
        {
           
            string userAgent = Request.Headers["User-Agent"];

            System.Web.HttpBrowserCapabilitiesBase bc = Request.Browser;
            if (IsNotInternetExplorer(userAgent))
            {
                //return View("rr");
            }

            Session["IsMobileDevice_"] = Request.Browser.IsMobileDevice;
            Session["assignmentId_"] = Request.QueryString["assignmentId"];
            Session["workerId_"] = Request.QueryString["workerId"];
            Session["hitId_"] = Request.QueryString["hitId"];
            //Session["assignmentId_"] = "ASSIGNMENT_ID_NOT_AVAILABLE";
            if (Session["assignmentId_"] != null && Session["assignmentId_"].ToString().Equals("ASSIGNMENT_ID_NOT_AVAILABLE"))
            {
                Session["stop"] = 1;
                // return View("Preview");
            }
            else
            {
                Session["stop"] = 0;
            }

            return View("ConsentIndex");
        }

        public static bool IsNotInternetExplorer(string userAgent)
        {
            
            if (userAgent.Contains("Chrome") && (!userAgent.Contains("Edge")&&!userAgent.Contains("Edg")))
            {
                return true;
            }
            return false;
            
        }

        public ActionResult Disagree()
        {
            return View("Disagree");
        }
        /*public ActionResult Agree()
        {
            return View("Disagree");
        }*/
    }
}