using System;

namespace AtominaCraft.ZResources.Logging {
    public class Logger {
        private readonly Action<string> LogMessageCallback;
        public string Name;

        public Logger(Action<string> callback, string name) {
            this.LogMessageCallback = callback;
            this.Name = name;
        }

        public void Log(string info) {
            this.LogMessageCallback?.Invoke(info);
        }

        public void LogLine(string info, bool isSevere = false) {
            this.LogMessageCallback?.Invoke($"[{this.Name}]{(isSevere ? " [SEVERE] " : "")}{info}\n");
        }
    }
}