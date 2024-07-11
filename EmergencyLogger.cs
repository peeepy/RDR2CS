using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDR2CS
{
    public static class EmergencyLogger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "emergency_log.txt");

        public static void LogError(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}";
                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception e) 
            {
                // If even this fails, we can't do much more
                Console.WriteLine(e.ToString());
            }
        }
    }
}
