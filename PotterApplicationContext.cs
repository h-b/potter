using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    class PotterApplicationContext : ApplicationContext
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        private Timesheet timesheet;
        private WatchdogHandler watchdogHandler;
        private ActivityHandler activityHandler;

        public PotterApplicationContext()
        {
            Application.ApplicationExit += new EventHandler(delegate (object sender, EventArgs e)
            {
                notifyIcon.Visible = false;
            });
            notifyIcon = InitializeTrayIcon();
            timesheet = new Timesheet();
            activityHandler = new ActivityHandler(timesheet);
            watchdogHandler = new WatchdogHandler(timesheet, activityHandler.InitiateToQueryUserActivity);
            notifyIcon.Visible = true;
        }

        private NotifyIcon InitializeTrayIcon()
        {
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.AddRange(new ToolStripItem[] {
                new ToolStripMenuItem("Show potter time tracker", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    activityHandler.InitiateToQueryUserActivity(false, true);
                })),
                new ToolStripMenuItem("Settings...", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    activityHandler.InitiateToQueryUserActivity(true, false);
                })),
                new ToolStripMenuItem("About...", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    AboutBox aboutBox = new AboutBox();
                    aboutBox.ShowDialog();
                })),
                new ToolStripMenuItem("Exit", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    Application.Exit();
                }))
            });

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Configuration().Icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Double-click to show time tracker, right-click to show menu";
            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.DoubleClick += new EventHandler(delegate (object sender, EventArgs e)
            {
                activityHandler.InitiateToQueryUserActivity(false, true);
            });

            return notifyIcon;
        }
    }
}
