using System;
using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Windows;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace AtominaCraft {
    internal static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Native.AllocConsole();
            WindowManager.Initialise();

            GameWindowSettings gws = GameWindowSettings.Default;
            gws.IsMultiThreaded = false;
            gws.UpdateFrequency = 400;
            gws.RenderFrequency = 100;

            NativeWindowSettings nws = NativeWindowSettings.Default;
            nws.Title = "AtominaCraft";
            nws.Profile = ContextProfile.Compatability;

            using (AtominaCraft atominaCraft = new AtominaCraft(gws, nws)) {
                atominaCraft.InitialiseGameWindow();
                atominaCraft.RunGameLoop();
            }
        }
    }
}