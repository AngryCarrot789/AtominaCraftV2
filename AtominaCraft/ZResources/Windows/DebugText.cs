using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.ZResources.Windows
{
    public static class DebugText
    {
        public static DebugWindow Window { get; set; }

        public static bool CanWrite { get; set; }

        public static void Write(string text)
        {
            if (CanWrite)
            {
                try
                {
                    Action write = () => { Window.Write(text); };
                    Window.Invoke(write);
                }
                catch { }
            }
        }

        public static void WriteLine(string text)
        {
            Write(text + '\n');
        }

        public static void Clear()
        {
            if (CanWrite)
            {
                try
                {
                    Action clear = () => { Window.Clear(); };
                    Window.Invoke(clear);
                }
                catch { }
            }
        }

        public static void SetText(string text)
        {
            if (CanWrite)
            {
                try
                {
                    Action write = () => { Window.SetText(text); };
                    Window.Invoke(write);
                }
                catch { }
            }
        }
    }
}
