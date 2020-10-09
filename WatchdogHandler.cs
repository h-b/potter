﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace potter
{
    class WatchdogHandler
    {
        System.Timers.Timer watchdog = new System.Timers.Timer();
        object handlingWatchdog = new object();
        bool wasScreenSaverRunning = false;
        bool wasWorkstationLocked = false;
        Timesheet timesheet;
        ActivityHandler.DelegateInitiateToQueryUserActivity InitiateToQueryUserActivity;

        public WatchdogHandler(Timesheet timesheet, ActivityHandler.DelegateInitiateToQueryUserActivity InitiateToQueryUserActivity)
        {
            this.timesheet = timesheet;
            this.InitiateToQueryUserActivity = InitiateToQueryUserActivity;
            watchdog.Elapsed += Watchdog;
            watchdog.Interval = 100;
            watchdog.Start();
        }

        private void Watchdog(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(handlingWatchdog))
            {
                try
                {
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
                            InitiateToQueryUserActivity(false);
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
                            InitiateToQueryUserActivity(false);
                        }

                        wasWorkstationLocked = isWorkstationLocked;
                    }
                    watchdog.Start();
                }
                finally
                {
                    Monitor.Exit(handlingWatchdog);
                }
            }
        }
    }
}