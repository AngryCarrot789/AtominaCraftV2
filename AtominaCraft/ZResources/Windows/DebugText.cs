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
                Action write = () => { Window.Write(text); };
                Window.Invoke(write);
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
                Action clear = () => { Window.Clear(); };
                Window.Invoke(clear);
            }
        }
    }
}
