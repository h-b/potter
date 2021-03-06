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
            const string inactiveMsg = "Inactive (screen saver plus preceding inactive time or locked screen)";

            if (Monitor.TryEnter(handlingWatchdog))
            {
                try
                {
                    watchdog.Stop();
                    bool isScreenSaverRunning = WindowsSpecific.IsScreensaverRunning();
                    bool isWorkstationLocked = WindowsSpecific.IsWorkstationLocked();

                    if (isScreenSaverRunning != wasScreenSaverRunning)
                    {
                        Logger.Append(string.Format("Detected changed screensaver status: {0} --> {1}", wasScreenSaverRunning, isScreenSaverRunning));

                        if (isScreenSaverRunning)
                        {
                            timesheet.AddActivity(
                                DateTime.Now - TimeSpan.FromSeconds(WindowsSpecific.ScreenSaverTimeout),
                                inactiveMsg, Timesheet.SystemCategory);
                        }
                        else
                        {
                            InitiateToQueryUserActivity(false, true, true);
                        }

                        wasScreenSaverRunning = isScreenSaverRunning;
                    }

                    if (isWorkstationLocked != wasWorkstationLocked)
                    {
                        Logger.Append(string.Format("Detected changed screenlocked status: {0} --> {1}", wasWorkstationLocked, isWorkstationLocked));

                        if (isWorkstationLocked)
                        {
                            if (!wasScreenSaverRunning)
                            {
                                timesheet.AddActivity(DateTime.Now, inactiveMsg, Timesheet.SystemCategory);
                            }
                        }
                        else
                        {
                            InitiateToQueryUserActivity(false, true, true);
                        }

                        wasWorkstationLocked = isWorkstationLocked;
                    }

                    if (Program.s_event.WaitOne(1))
                    {
                        Logger.Append("Received message from other potter instance to open the dialog.");
                        InitiateToQueryUserActivity(false, true, false);
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
