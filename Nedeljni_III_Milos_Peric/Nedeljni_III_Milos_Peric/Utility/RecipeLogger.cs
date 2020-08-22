using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Milos_Peric.Utility
{
    internal class RecipeLogger
    {
        public static string logMessage = "";
        public static string logUserName = "";
        private static StringBuilder fileName = new StringBuilder();

        public static void LogToFile()
        {
            try
            {
                fileName.Append(@"..\..\").Append(logUserName).Append(".txt");
                DateTime currentTime = DateTime.Now;
                using (StreamWriter streamWriter = new StreamWriter(fileName.ToString(), append: true))
                {
                    streamWriter.WriteLine(logMessage + " Time: " + currentTime.ToString());
                }
                logMessage = "";
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: cannot write log to file.");
                Debug.WriteLine(e.Message);
            }
        }
    }
}
