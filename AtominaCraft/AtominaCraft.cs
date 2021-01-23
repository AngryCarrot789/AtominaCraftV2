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
using AtominaCraft.Blocks.Rendering;
using AtominaCraft.ZResources.Windows;
using AtominaCraft.Worlds.Chunks.Rendering;

namespace AtominaCraft
{
    public class AtominaCraft : GameWindow
    {
        public static AtominaCraft Instance { get; private set; }

        public List<World> Worlds { get; private set; }

        public EntityPlayerCamera Player { get; set; }

        public bool IsRunning { get; set; }
        public bool DrawDebug { get; set; }

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

            GraphicsLoader.Load();

            // Initialise Game

            //BlockMesh.Generate();
            //ChunkMeshGenerator.Initialise();

            Player = new EntityPlayerCamera();
            Worlds = new List<World>();

            World earth = new World();
            earth.SetMainPlayer(Player);
            Player.MoveTo(new Vector3(0, 0, 3));

            Chunk chunk = ChunkGenerator.GenerateFlat(earth, new ChunkLocation(1, 1), 6);
            //ChunkMeshGenerator.GenerateChunk(chunk);
            earth.Chunks.Add(chunk.Location, chunk);
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
                if (KeyboardState.IsKeyDown(Keys.Escape))
                {
                    this.Close();
                    Application.Exit();
                }

                if (KeyboardState.IsKeyPressed(Keys.F3))
                {
                    DrawDebug = !DrawDebug;
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

            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            Player.World.Sky.Draw(Player.Camera);
            GL.ClearColor(0.2f, 0.2f, 0.8f, 1.0f);

            Player.Camera.WorldView = Player.WorldToCamera();
            Player.Camera.SetSize(Size.X, Size.Y, GameSettings.RENDER_NEAR_MIN, GameSettings.RENDER_FAR, GameSettings.RENDER_FOV);
            Player.Camera.UseViewport();

            Vector3 position = new Vector3();
            foreach (Chunk chunk in Player.World.Chunks.Values)
            {
                //ChunkMesh mesh = ChunkMeshGenerator.GetMesh(chunk);
                //if (mesh != null)
                //{
                //    mesh.Draw(Player.Camera);
                //}
                foreach (Block block in chunk.Blocks.Values)
                {
                    block.Location.Extract(position);
                    BlockRenderer.DrawBlock(block, Player.Camera);
                }
            }

            DebugText.Clear();
            DebugText.WriteLine($"Player Position: {Player.Position}");
            DebugText.WriteLine($"Player Look:     X: {Player.CameraRotationX}, Y: {Player.CameraRotationY}");
            DebugDraw.DrawAABB(Player.Camera, Player.BoundingBox);

            DebugDraw.DrawXYZ(Player.Camera.Projection, Player.CameraRotationY, Player.CameraRotationX);

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