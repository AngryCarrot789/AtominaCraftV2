using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AtominaCraft.Resources.Windows
{
    public static class WindowManager
    {
        private static Thread FormsThead { get; set; }

        public static bool HasInitialised { get; private set; }

        public static void Initialise()
        {
            if (HasInitialised)
            {
                return;
            }

            FormsThead = new Thread(RunApplication);
            FormsThead.Name = "Forms Thread";
            FormsThead.Start();
        }

        private static void RunApplication()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HasInitialised = true;
            Application.Run();
        }
    }
}
