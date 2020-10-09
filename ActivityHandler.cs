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
        Timesheet timesheet;
        public delegate void DelegateInitiateToQueryUserActivity(bool showConfiguration);

        public ActivityHandler(Timesheet timesheet)
        {
            this.timesheet = timesheet;
            queryUserActivity.Elapsed += QueryUserActivity;
            InitiateToQueryUserActivity(false);
        }

        internal void InitiateToQueryUserActivity(bool showConfiguration)
        {
            this.showConfiguration = showConfiguration;
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
                    Activity activity = new Activity(showConfiguration);
                    showConfiguration = false;
                    DialogResult dialogResult = activity.ShowDialog();
                    DateTime toTime = DateTime.Now;

                    timesheet.AddActivity(DateTime.Now, activity.Current);

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
