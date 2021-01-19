using AtominaCraft.Crash;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Logging;
using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using AtominaCraft.Worlds;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using System.Windows.Forms;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.Blocks;

namespace AtominaCraft
{
    public class AtominaCraft : GameWindow
    {
        public static AtominaCraft Instance { get; private set; }

        public List<World> Worlds { get; private set; }

        public EntityPlayerCamera Player { get; set; }

        public bool IsRunning { get; set; }

        public AtominaCraft(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Instance = this;
        }

        public bool Initialise()
        {
            Size = new OpenTK.Mathematics.Vector2i(1280, 720);
            IsRunning = false;
            LogManager.Initialise();
            if (InitialiseOpenGL())
            {
                Graphics.Initialise();
                DebugDraw.Initialise();

                LogManager.OpenGLLogger.Log("Successfully initialised Game Engine");
                return true;
            }
            else
            {
                LogManager.OpenGLLogger.Log("Failed to initialise Game Engine");
                return false;
            }
        }

        public bool InitialiseOpenGL()
        {
            GLFWBindingsContext binding = new GLFWBindingsContext();
            GL.LoadBindings(binding);

            if (GLFW.Init())
            {
                GL.ClearColor(0.2f, 0.4f, 0.8f, 1.0f);
                GL.Enable(EnableCap.CullFace);
                GL.CullFace(CullFaceMode.Back);
                GL.Enable(EnableCap.DepthTest);
                GL.DepthFunc(DepthFunction.Less);
                GL.DepthMask(true);

                LogManager.OpenGLLogger.Log("Successfully initialised GLFW and OpenGL");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to initialise GLFW and OpenGL");
                return false;
            }
        }

        public void RunGameLoop()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Console.WriteLine("Starting Game Loop");
                base.Run();
            }
        }

        protected override void OnLoad()
        {
            // Called just after OpenGL is initialised
            DebugText.CanWrite = true;

            try
            {
                Graphics.Initialise();
                Graphics.Load();
            }
            catch (Exception exception)
            {
                CrashReport start = new CrashReport(exception, "Graphics Loader", "Exception after trying to load meshes/shaders/textures");
                throw exception;
            }

            // Initialise Game

            Player = new EntityPlayerCamera();

            Worlds = new List<World>();
            World earth = new World();
            earth.MainPlayer = Player;
            Player.World = earth;
            Player.Position.Set(0, 0, 3);
            Worlds.Add(earth);

            Inputs.Keyboard = KeyboardState;
            Inputs.Mouse = MouseState;

            CursorVisible = false;
            CursorGrabbed = true;

            base.OnLoad();
        }

        public void UpdateGame()
        {
            try
            {
                // Calculate delta time

                DebugText.Clear(true);

                //Mouse.UpdateMousePosition();

                if (KeyboardState.IsKeyDown(Keys.Escape))
                {
                    this.Close();
                    Application.Exit();
                }

                Player.World.Update();

                //Mouse.EndFrame();

            }
            catch (Exception exception)
            {
                CrashReport start = new CrashReport(exception, "Game Loop", "Exception in game loop");
                throw exception;
                //ShutdownGame();
            }
        }

        public void RenderGame()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.2f, 0.2f, 0.8f, 1.0f);

            Player.Camera.WorldView = Player.WorldToCamera();
            Player.Camera.SetSize(Size.X, Size.Y, GameSettings.RENDER_NEAR_MIN, GameSettings.RENDER_FAR, GameSettings.RENDER_FOV);
            Player.Camera.UseViewport();

            Vector3 position = new Vector3();
            foreach(Chunk chunk in Player.World.Chunks)
            {
                foreach(Block block in chunk.Blocks)
                {
                    block.Location.Extract(position);
                    DebugDraw.DrawCube(Player.Camera, position, Vector3.Halfs);
                }
            }

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            Delta.Time = (float)args.Time;
            UpdateGame();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            RenderGame();
        }
    }
}