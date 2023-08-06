using System;

namespace AtominaCraft.ZResources.Windows {
    public static class DebugText {
        public static DebugWindow Window;

        public static bool CanWrite;

        public static void Write(string text) {
            if (CanWrite)
                try {
                    Action write = () => { Window.Write(text); };
                    Window.Invoke(write);
                }
                catch {
                }
        }

        public static void WriteLine(string text) {
            Write(text + '\n');
        }

        public static void Clear() {
            if (CanWrite)
                try {
                    Action clear = () => { Window.Clear(); };
                    Window.Invoke(clear);
                }
                catch {
                }
        }

        public static void SetText(string text) {
            if (CanWrite)
                try {
                    Action write = () => { Window.SetText(text); };
                    Window.Invoke(write);
                }
                catch {
                }
        }
    }
}