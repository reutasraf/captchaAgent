using ExperimentCaptcha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyPacer4.Controllers
{
    public class SecretSaveController : Controller
    {
        private ExperimentDBContext db = new ExperimentDBContext();
        // GET: SecretSave
        public FileResult Index()
        {
            var query = from b in db.Experiments
                        select b;
            string time = DateTime.Now.ToString().Replace(' ', '_').Replace('/', '_').Replace(':', '_');
            string path = "C:/inetpub/wwwroot/EmptyPacer4/Models/db.txt";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path))
            {
                foreach (var item in query)
                {
                    file.WriteLine(item.ToString());
                }
            }
            return new FilePathResult(path, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
    }
}