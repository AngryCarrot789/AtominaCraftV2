using System;

namespace AtominaCraft.Logging
{
    public class Logger
    {
        public string Name { get; set; }

        private Action<string> LogMessageCallback { get; set; }

        public Logger(Action<string> callback, string name)
        {
            LogMessageCallback = callback;
            Name = name;
        }

        public void Log(string info)
        {
            LogMessageCallback?.Invoke(info);
        }

        public void LogLine(string info, bool isSevere = false)
        {
            LogMessageCallback?.Invoke($"[{Name}]{(isSevere ? " [SEVERE] " : "")}{info}\n");
        }
    }
}
