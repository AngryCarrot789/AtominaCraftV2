using System;

namespace AtominaCraft.ZResources.Logging {
    public static class LogManager {
        public static Logger ApplicationLogger { get; private set; }
        public static Logger WindowLogger { get; private set; }
        public static Logger OpenGLLogger { get; private set; }
        public static Logger GraphicsLogger { get; private set; }

        public static void Initialise() {
            ApplicationLogger = CreateLogger("Application");
            WindowLogger = CreateLogger("Window");
            OpenGLLogger = CreateLogger("OpenGL");
            GraphicsLogger = CreateLogger("Graphics Loader");
        }

        public static Logger CreateLogger(string loggerName) {
            return new Logger(LogRawLine, loggerName);
        }

        public static void LogRawLine(string message) {
            Console.WriteLine($"{DateTime.Now.ToString("T")} {message}");
        }

        public static void LogRawUntimed(string message) {
            Console.WriteLine(message);
        }

        //ublic static void GLDebugProc(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        //
        //   OpenGLLogger.Log($"[{id}] [{source}, {type}, {severity}]");
        //
    }
}