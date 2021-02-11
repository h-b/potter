using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    class Timesheet : IDisposable
    {
        private string previousActivity = null;
        private string previousCategory = null;
        private string previousUserActivity = null;
        private string previousUserCategory = null;
        private DateTime previousStartTime = DateTime.MinValue;
        internal static string SystemCategory = "(system)";

        void IDisposable.Dispose()
        {
            DateTime now = DateTime.Now;
            Update(previousStartTime, now, previousActivity, previousCategory);
            Update(now, now, "Application exit", "(system)");
        }

        public void AddActivity(DateTime startTime, string activity, string category)
        {
            if (activity != previousActivity || category != previousCategory)
            {
                if (!string.IsNullOrWhiteSpace(previousActivity) && previousStartTime != DateTime.MinValue)
                {
                    Update(previousStartTime, startTime, previousActivity, string.IsNullOrWhiteSpace(previousCategory) ? "": previousCategory);
                }

                previousStartTime = startTime;
                previousActivity = activity;
                previousCategory = category;

                if (category != SystemCategory)
                {
                    previousUserActivity = activity;
                    previousUserCategory = category;
                }
            }
        }

        internal static string dateFormat = "yyyy-MM-dd";
        internal static string timeFormat = "HH:mm";

        private void Update(DateTime startTime, DateTime endTime, string activity, string category)
        {
            string cmd = Configuration.ExecuteCommand;

            DateTime roundedStartTime = RoundToNearest(startTime, Configuration.RoundTimes);
            DateTime roundedEndTime = RoundToNearest(endTime, Configuration.RoundTimes);

            cmd = cmd.Replace("$FROM_DATE", roundedStartTime.ToString(dateFormat));
            cmd = cmd.Replace("$TO_DATE", roundedEndTime.ToString(dateFormat));
            cmd = cmd.Replace("$FROM_TIME", roundedStartTime.ToString(timeFormat));
            cmd = cmd.Replace("$TO_TIME", roundedEndTime.ToString(timeFormat));
            cmd = cmd.Replace("$ACTIVITY", activity);
            cmd = cmd.Replace("$CATEGORY", category);

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
