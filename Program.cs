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
        static Timesheet timesheet;
        static NotifyIcon notifyIcon;
        static ContextMenu contextMenu = new ContextMenu();
        static WatchdogHandler watchdogHandler;
        static ActivityHandler activityHandler;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (notifyIcon = InitializeTrayIcon())
            using (timesheet = new Timesheet())
            {
                activityHandler = new ActivityHandler(timesheet);
                watchdogHandler = new WatchdogHandler(timesheet, activityHandler.InitiateToQueryUserActivity);
                Application.Run();
            }
        }

        private static NotifyIcon InitializeTrayIcon()
        {
            contextMenu.MenuItems.Add("Show potter time tracker");
            contextMenu.MenuItems.Add("Configure potter time tracker");
            contextMenu.MenuItems.Add("Exit");
            contextMenu.MenuItems[0].Click += new EventHandler(delegate (object sender, EventArgs e)
            {
                activityHandler.InitiateToQueryUserActivity(false);
            });
            contextMenu.MenuItems[1].Click += new EventHandler(delegate (object sender, EventArgs e)
            {
                activityHandler.InitiateToQueryUserActivity(true);
            });
            contextMenu.MenuItems[2].Click += new EventHandler(delegate (object sender, EventArgs e)
            {
                Application.Exit();
            });

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Configuration().Icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Left-click to show time tracker, right-click to show menu";
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.MouseClick += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            {
                activityHandler.InitiateToQueryUserActivity(false);
            });
            return notifyIcon;
        }
    }
}
