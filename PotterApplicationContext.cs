using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    class PotterApplicationContext : ApplicationContext
    {
        private NotifyIcon notifyIcon;
        private Timesheet timesheet;
        private WatchdogHandler watchdogHandler;
        private ActivityHandler activityHandler;

        public PotterApplicationContext()
        {
            Application.ApplicationExit += new EventHandler(delegate (object sender, EventArgs e)
            {
                notifyIcon.Visible = false;
                Logger.Append("PotterApplicationContext.ApplicationExit");
            });
            timesheet = new Timesheet();
            activityHandler = new ActivityHandler(timesheet);
            watchdogHandler = new WatchdogHandler(timesheet, activityHandler.InitiateToQueryUserActivity);
            InitializeTrayIcon();
            Logger.Append("Created PotterApplicationContext");
        }

        ~PotterApplicationContext()
        {
            Logger.Append("~PotterApplicationContext");
        }

        private void InitializeTrayIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Configuration().Icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Double-click to show time tracker, right-click to show menu";
            notifyIcon.ContextMenuStrip = createContextMenu();

            notifyIcon.DoubleClick += new EventHandler(delegate (object sender, EventArgs e)
            {
                Logger.Append("TrayIcon.DoubleClick");
                activityHandler.InitiateToQueryUserActivity(false, true, false);
            });
            notifyIcon.Visible = true;
        }

        private ContextMenuStrip createContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.AddRange(new ToolStripItem[] {
                new ToolStripMenuItem("Show potter time tracker", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    Logger.Append("TrayIcon.Show");
                    activityHandler.InitiateToQueryUserActivity(false, true, false);
                })),
                new ToolStripMenuItem("Settings...", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    Logger.Append("TrayIcon.Settings");
                    activityHandler.InitiateToQueryUserActivity(true, false, false);
                })),
                new ToolStripMenuItem("About...", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    Logger.Append("TrayIcon.About");
                    AboutBox aboutBox = new AboutBox();
                    aboutBox.ShowDialog();
                })),
                new ToolStripMenuItem("Exit", null, new EventHandler(delegate (object sender, EventArgs e)
                {
                    Logger.Append("TrayIcon.Exit");
                    Application.Exit();
                }))
            });

            contextMenu.Opened += new EventHandler(delegate (object sender, EventArgs e)
            {
                notifyIcon.ContextMenuStrip = createContextMenu(); // workaround for randomly freezing context menu after Opened event
            });

            return contextMenu;
        }
    }
}
