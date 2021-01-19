using System;

namespace AtominaCraft.Logging
{
    public static class DebugText
    {
        //public static Label Label { get; set; }
        //public static Label MultithreadLabel { get; set; }
        public static bool CanWrite { get; set; }

        public static void Write(string text, bool useMultithread = false)
        {
            if (!CanWrite) return;

            try
            {
                if (useMultithread)
                {
                    //Action write = () => { MultithreadLabel.Text += text; };
                    //MultithreadLabel.Invoke(write);
                }
                else
                {
                    //Action write = () => { Label.Text += text; };
                    //Label.Invoke(write);
                }
            }
            catch { }
        }

        public static void WriteLine(string text, bool useMultithread = false)
        {
            Write(text + '\n', useMultithread);
        }

        public static void Clear(bool useMultithread = false)
        {
            if (!CanWrite) return;

            if (useMultithread)
            {
                //Action write = () => { MultithreadLabel.Text = ""; };
                //MultithreadLabel.Invoke(write);
            }
            else
            {
                //Action write = () => { Label.Text = ""; };
                //Label.Invoke(write);
            }
        }
    }
}
