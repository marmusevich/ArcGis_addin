using System;
using System.IO;
using System.Text;

namespace WorckWithReestr
{
    //запись лога
    public class Logger
    {
        private static object sync = new object();
        // вывести инвормации о ошибке в лог
        public static void Write(Exception ex, string addetMessage = "")
        {
            try
            {
                string pathToLog = Path.Combine(SharedClass.GetAppDataPathAndCreateDirIfNeed(), "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
                string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyy}.log", AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
                string fullText = "";
                if (ex != null)
                    fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}.{2}()] {3} {4}\r\n", DateTime.Now, ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message, addetMessage);
                else
                    fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] {1}\r\n", DateTime.Now, addetMessage);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
    }
}