using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace potter
{
    internal static class Logger
    {
        private static string logFilePath;
        private static object logMutex = new object();

        static Logger()
        {
            logFilePath = Path.Combine(Path.GetTempPath(), "potter.log");
        }

        internal static void Append(string entry)
        {
            DateTime timestamp = DateTime.Now;

            Monitor.Enter(logMutex);
            try
            {
                using (var file = File.Open(logFilePath, FileMode.Append))
                using (var stream = new StreamWriter(file))
                {
                    stream.Write(timestamp.ToShortDateString());
                    stream.Write(" ");
                    stream.Write(timestamp.ToShortTimeString());
                    stream.Write(" ");
                    stream.Write(Process.GetCurrentProcess().Id);
                    stream.Write(" ");
                    stream.Write(Thread.CurrentThread.ManagedThreadId);

                    stream.Write(": ");
                    stream.WriteLine(entry);
                }
            }
            finally
            {
                Monitor.Exit(logMutex);
            }
        }
    }
}
