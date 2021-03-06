﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace potter
{
    public static class WindowsSpecific
    {
        public static int ScreenSaverTimeout
        {
            get
            {
                string timeout = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", "");

                if (string.IsNullOrEmpty(timeout))
                {
                    timeout = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Control Panel\Desktop", "ScreenSaveTimeOut", "");

                    if (string.IsNullOrEmpty(timeout))
                    {
                        return 0;
                    }
                }

                return Int32.Parse(timeout);
            }
        }
        
        // The following code is from https://stackoverflow.com/a/9858981 (thank you!)
        // Used to check if the screen saver is running
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uint uAction,
                                                       uint uParam,
                                                       ref bool lpvParam,
                                                       int fWinIni);

        // Used to check if the workstation is locked
        [DllImport("user32", SetLastError = true)]
        private static extern IntPtr OpenDesktop(string lpszDesktop,
                                                 uint dwFlags,
                                                 bool fInherit,
                                                 uint dwDesiredAccess);

        [DllImport("user32", SetLastError = true)]
        private static extern IntPtr OpenInputDesktop(uint dwFlags,
                                                      bool fInherit,
                                                      uint dwDesiredAccess);

        [DllImport("user32", SetLastError = true)]
        private static extern IntPtr CloseDesktop(IntPtr hDesktop);

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SwitchDesktop(IntPtr hDesktop);

        // Check if the workstation has been locked.
        public static bool IsWorkstationLocked()
        {
            const int DESKTOP_SWITCHDESKTOP = 256;
            IntPtr hwnd = OpenInputDesktop(0, false, DESKTOP_SWITCHDESKTOP);

            if (hwnd == IntPtr.Zero)
            {
                // Could not get the input desktop, might be locked already?
                hwnd = OpenDesktop("Default", 0, false, DESKTOP_SWITCHDESKTOP);
            }

            // Can we switch the desktop?
            if (hwnd != IntPtr.Zero)
            {
                if (SwitchDesktop(hwnd))
                {
                    // Workstation is NOT LOCKED.
                    CloseDesktop(hwnd);
                }
                else
                {
                    CloseDesktop(hwnd);
                    // Workstation is LOCKED.
                    return true;
                }
            }

            return false;
        }

        public static bool IsScreensaverRunning()
        {
            const int SPI_GETSCREENSAVERRUNNING = 114;
            bool isRunning = false;

            if (SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, ref isRunning, 0))
            {
                return isRunning;
            }
            else
            {
                return false; // could not detect screen saver status
            }
        }
    }
}
