using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.ZLaunch
{
    public static class UtilityLauncher
    {
        public static void PreOpenGLLaunch()
        {
            LogManager.Initialise();
        }

        public static void AfterOpenGLLaunch()
        {
            DebugDraw.Initialise();
            GraphicsLoader.Load();
        }

        public static void PreGameLoopLaunch()
        {
            WorldMeshMap.Initialise();
        }
    }
}
