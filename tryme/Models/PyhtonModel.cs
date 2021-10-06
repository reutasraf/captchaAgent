using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ExpCap.Models
{
    public class PyhtonModel
    {
        public PyhtonModel(string filename)
        {
            System.Diagnostics.Debug.WriteLine("need to happen once");
            File = filename;
        }

        public string File { get; private set; }

        public int GetNewPace(string info)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("{0} {1}", File, info);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return 1000;
                    return int.Parse(result);
                }
            }
        }

        public string GetExperimentType()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("{0} {1}", File, "GETTYPE");
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}