using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    class ActivityHandler
    {
        System.Timers.Timer queryUserActivity = new System.Timers.Timer();
        object handlingUserActivity = new object();
        bool showConfiguration = false;
        bool startNow = false;
        Timesheet timesheet;
        public delegate void DelegateInitiateToQueryUserActivity(bool showConfiguration, bool startNow);

        public ActivityHandler(Timesheet timesheet)
        {
            this.timesheet = timesheet;
            queryUserActivity.Elapsed += QueryUserActivity;
            InitiateToQueryUserActivity(false, true);
        }

        internal void InitiateToQueryUserActivity(bool showConfiguration, bool startNow)
        {
            this.showConfiguration = showConfiguration;
            this.startNow = startNow;
            queryUserActivity.Interval = 1;
            queryUserActivity.Start();
        }

        private void QueryUserActivity(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(handlingUserActivity))
            {
                try
                {
                    queryUserActivity.Stop();
                    Activity activity = new Activity(Configuration.CurrentActivity, Configuration.CurrentCategory, showConfiguration);
                    showConfiguration = false;
                    DateTime startTime = DateTime.Now;

                    DialogResult dialogResult = activity.ShowDialog();

                    try
                    {
                        Configuration.CurrentActivity = activity.Current;
                        Configuration.CurrentCategory = activity.Category;
                    }
                    catch (System.Exception ex)
                    {
                        Configuration.ShowSaveError(ex);
                    }

                    if (!startNow)
                    {
                        // the activity does not start when the dialog opens, but only after the user closes the dialog
                        startTime = DateTime.Now;
                    }

                    timesheet.AddActivity(startTime, activity.Current, activity.Category);

                    queryUserActivity.Interval = 60 * 1000 * (dialogResult == DialogResult.OK ? Configuration.DefaultTimeInterval : Configuration.OptionalTimeInterval);
                    queryUserActivity.Start();
                }
                finally
                {
                    Monitor.Exit(handlingUserActivity);
                }
            }
        }
    }
}
