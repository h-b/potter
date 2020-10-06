using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    static class Program
    {
        static NotifyIcon notifyIcon;
        static ContextMenu contextMenu = new ContextMenu();
        static bool wasScreenSaverRunning = false;
        static bool wasWorkstationLocked = false;
        static System.Windows.Forms.Timer watchdog = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer queryUserActivity = new System.Windows.Forms.Timer();
        static bool handlingUserActivity = false;
        static bool handlingWatchdog = false;
        static Timesheet timesheet = new Timesheet();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeTrayIcon();
            InitializeTimers();
            Application.Run();
            notifyIcon.Dispose();
        }

        private static void QueryUserActivity(object sender, EventArgs e)
        {
            if (handlingUserActivity)
            {
                return;
            }
            handlingUserActivity = true;

            queryUserActivity.Stop();
            Activity activity = new Activity();
            DialogResult dialogResult = activity.ShowDialog();
            DateTime toTime = DateTime.Now;

            timesheet.AddActivity(DateTime.Now, activity.Current);

            queryUserActivity.Interval = 60 * 1000 * (dialogResult == DialogResult.OK ? Configuration.DefaultTimeInterval : Configuration.OptionalTimeInterval);
            handlingUserActivity = false;
            queryUserActivity.Start();
        }

        private static void Watchdog(object sender, EventArgs e)
        {
            if (handlingWatchdog)
            {
                return;
            }
            handlingWatchdog = true;

            watchdog.Stop();
            bool isScreenSaverRunning = WindowsSpecific.IsScreensaverRunning();
            bool isWorkstationLocked = WindowsSpecific.IsWorkstationLocked();

            if (isScreenSaverRunning != wasScreenSaverRunning)
            {
                if (isScreenSaverRunning)
                {
                    timesheet.AddActivity(
                        DateTime.Now - TimeSpan.FromSeconds(WindowsSpecific.ScreenSaverTimeout),
                        "Inactive (screen saver plus preceding inactive time)");
                }
                else
                {
                    InitiateToQueryUserActivity();
                }

                wasScreenSaverRunning = isScreenSaverRunning;
            }

            if (isWorkstationLocked != wasWorkstationLocked)
            {
                if (isWorkstationLocked)
                {
                    if (!wasScreenSaverRunning)
                    {
                        timesheet.AddActivity(DateTime.Now, "Inactive (locked screen)");
                    }
                }
                else
                {
                    InitiateToQueryUserActivity();
                }

                wasWorkstationLocked = isWorkstationLocked;
            }
            handlingWatchdog = false;
            watchdog.Start();
        }

        private static void InitializeTrayIcon()
        {
            contextMenu.MenuItems.Add("Show potter time tracker");
            contextMenu.MenuItems.Add("Exit");
            contextMenu.MenuItems[0].Click += ShowApplication;
            contextMenu.MenuItems[1].Click += ExitApplication;

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Configuration().Icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Left-click to show time tracker, right-click to show menu";
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.MouseClick += ShowApplication;
        }

        private static void InitializeTimers()
        {
            watchdog.Tick += Watchdog;
            watchdog.Interval = 100;
            watchdog.Start();

            queryUserActivity.Tick += QueryUserActivity;
            InitiateToQueryUserActivity();
        }

        private static void InitiateToQueryUserActivity()
        {
            queryUserActivity.Stop();
            queryUserActivity.Interval = 1;
            queryUserActivity.Start();
        }

        private static void ShowApplication(object sender, EventArgs e)
        {
            InitiateToQueryUserActivity();
        }

        private static void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
