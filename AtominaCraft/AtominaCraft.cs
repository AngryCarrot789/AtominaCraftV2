using AtominaCraft.Crash;
using AtominaCraft.Entities.Player;
using AtominaCraft.Logging;
using AtominaCraft.Resources;
using AtominaCraft.Resources.Graphics;
using AtominaCraft.Resources.Maths;
using AtominaCraft.Worlds;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using System.Windows.Forms;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

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

        GameObject cube;

        float[] vertices =
        {
            // Front face - 2 triangles
            -1.0f,  1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,

            // Top Face
            -1.0f,  1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f,  1.0f, -1.0f,
            
            // Right Face
             1.0f,  1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
            
            // Bottom Face
            -1.0f, -1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
            -1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
             1.0f, -1.0f,  1.0f,
             1.0f, -1.0f, -1.0f,
            
            // Back Face
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,
            
            // Left Face
            -1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
        };

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

            cube = new GameObject();
            cube.Position = new Vector3(0.5f, 0.0f, 1.0f);
            cube.Rotation = Vector3.Zero;
            cube.Scale = Vector3.Ones;

            Graphics.GetMesh("cube", out Mesh cubeMesh);
            Graphics.GetTexture("electromagnet", out Texture cubeTexture);
            cube.Mesh = cubeMesh;
            cube.Texture = cubeTexture;
            cube.Shader = Graphics.TextureShader;

            // Initialise Game

            Player = new EntityPlayerCamera();

            Worlds = new List<World>();
            World earth = new World();
            earth.Player = Player;
            Player.World = earth;
            Player.Position.Set(0, 0, 3);
            Worlds.Add(earth);

            Inputs.Keyboard = KeyboardState;
            Inputs.Mouse = MouseState;
            Size = new OpenTK.Mathematics.Vector2i(1280, 720);

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

                Player.Update();

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
            Player.Camera.SetSize(this.Size.X, this.Size.Y, GameSettings.RENDER_NEAR_MIN, GameSettings.RENDER_FAR, GameSettings.RENDER_FOV);
            Player.Camera.UseViewport();

            cube.DrawOutline(Player.Camera);
            cube.Draw(Player.Camera);

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