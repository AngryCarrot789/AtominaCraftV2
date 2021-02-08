using System;

namespace AtominaCraft.Crash
{
    /// <summary>
    /// Used to give information on what crashed the game
    /// </summary>
    public class CrashReport
    {
        public Exception Exception { get; set; }

        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string KnownCause { get; set; }
        public string KnownDescription { get; set; }

        public CrashReport()
        {
            Exception = new Exception("dummy");
            Message = "[No Message]";
            StackTrace = "[No StackTrace]";
            KnownCause = "[No Cause]";
            KnownDescription = "[No Description]";
        }

        public CrashReport(Exception cause, string knownCause, string description)
        {
            Exception = cause;
            Message = cause.Message;
            StackTrace = cause.StackTrace;
            KnownCause = knownCause;
            KnownDescription = description;
        }
    }
}
