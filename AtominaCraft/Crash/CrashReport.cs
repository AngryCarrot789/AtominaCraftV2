using System;

namespace AtominaCraft.Crash {
    /// <summary>
    ///     Used to give information on what crashed the game
    /// </summary>
    public class CrashReport {
        public Exception Exception;
        public string KnownCause;
        public string KnownDescription;

        public string Message;
        public string StackTrace;

        public CrashReport() {
            this.Exception = new Exception("dummy");
            this.Message = "[No Message]";
            this.StackTrace = "[No StackTrace]";
            this.KnownCause = "[No Cause]";
            this.KnownDescription = "[No Description]";
        }

        public CrashReport(Exception cause, string knownCause, string description) {
            this.Exception = cause;
            this.Message = cause.Message;
            this.StackTrace = cause.StackTrace;
            this.KnownCause = knownCause;
            this.KnownDescription = description;
        }
    }
}