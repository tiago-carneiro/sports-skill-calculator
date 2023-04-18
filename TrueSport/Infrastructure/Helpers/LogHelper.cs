using System;
using System.Text;

namespace TrueSport
{
    public static class LogHelper
    {
        static string ConcactException(Exception ex, StringBuilder str = null)
        {
            if (str == null)
                str = new StringBuilder();

            str.AppendLine($"Message: {ex.Message}");
            str.AppendLine($"StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
                str.AppendLine(ConcactException(ex.InnerException, str));

            return str.ToString();
        }

        public static void Log(string TAG, Exception ex)
        {
#if DEBUG
            Log(TAG, ConcactException(ex));
#endif

            //TODO send to your crashreport services
        }

        public static void Log(string TAG, string msg)
        {
#if DEBUG
            Console.WriteLine($"[{TAG}] {msg}");
#endif
        }
    }
}

