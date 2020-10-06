using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    class Timesheet
    {
        private string previousDescription = null;
        private DateTime previousStartTime = DateTime.MinValue;

        ~Timesheet()
        {
            DateTime now = DateTime.Now;
            Update(previousStartTime, now, previousDescription);
            Update(now, now, "Application exit");
        }

        public void AddActivity(DateTime startTime, string description)
        {
            if (description != previousDescription)
            {
                if (!string.IsNullOrWhiteSpace(previousDescription) && previousStartTime != DateTime.MinValue)
                {
                    Update(previousStartTime, startTime, previousDescription);
                }

                previousStartTime = startTime;
                previousDescription = description;
            }
        }

        private void Update(DateTime startTime, DateTime endTime, string project)
        {
            string cmd = Configuration.ExecuteCommand;

            cmd = cmd.Replace("$FROM", RoundToNearest(startTime, Configuration.RoundTimes).ToShortTimeString());
            cmd = cmd.Replace("$TO", RoundToNearest(endTime, Configuration.RoundTimes).ToShortTimeString());
            cmd = cmd.Replace("$PROJECT", project);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = cmd.Split(" ".ToCharArray())[0];
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = false;
            psi.Arguments = cmd.Substring(psi.FileName.Length + 1);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            try
            {
                Process.Start(psi);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Error updating time sheet: {0}", ex.Message), "Time Tracker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static DateTime RoundToNearest(DateTime dt, int minutes)
        {
            return DateTime.MinValue.Add(new TimeSpan(0,
                (((int)(dt.Subtract(DateTime.MinValue)).Add(new TimeSpan(0, minutes / 2, 0)).TotalMinutes) / minutes) * minutes, 0));
        }
    }
}
