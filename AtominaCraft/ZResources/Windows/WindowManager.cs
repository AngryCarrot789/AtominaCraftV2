using System.Threading;
using System.Windows.Forms;

namespace AtominaCraft.ZResources.Windows {
    public static class WindowManager {
        private static Thread FormsThead;

        public static bool HasInitialised { get; private set; }

        public static void Initialise() {
            if (HasInitialised)
                return;

            FormsThead = new Thread(RunApplication);
            FormsThead.Name = "Forms Thread";
            FormsThead.Start();
        }

        private static void RunApplication() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HasInitialised = true;
            DebugText.Window = new DebugWindow();
            DebugText.CanWrite = true;
            DebugText.Window.Show();
            Application.Run();
        }
    }
}