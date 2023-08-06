using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.ZResources.GFX;
using AtominaCraft.ZResources.Logging;

namespace AtominaCraft.ZLaunch {
    public static class UtilityLauncher {
        public static void PreOpenGLLaunch() {
            LogManager.Initialise();
        }

        public static void AfterOpenGLLaunch() {
            DebugDraw.Initialise();
            GraphicsLoader.Load();
        }

        public static void PreGameLoopLaunch() {

        }
    }
}