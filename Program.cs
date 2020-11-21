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
        internal static EventWaitHandle s_event;
        internal static string eventName = "potter#startup";

        [STAThread]
        static void Main()
        {
            Logger.Append("Starting application");
            bool created;
            s_event = new EventWaitHandle(false, EventResetMode.AutoReset, eventName, out created);
            if (created)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PotterApplicationContext());
            }
            else
            {
                s_event.Set();
                Logger.Append("Detected other running potter instance - sending message to open dialog.");
            }

            Logger.Append("Exiting application");
        }
    }
}
