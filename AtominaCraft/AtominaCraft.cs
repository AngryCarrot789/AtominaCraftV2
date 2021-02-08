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
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Windows;
using Forms = System.Windows.Forms;
using AtominaCraft.ZResources.Controls;
using AtominaCraft.ZLaunch;
using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.Client.BlockRendering.Mesh.Generator;
using AtominaCraft.Client.BlockRendering;
using AtominaCraft.Blocks;
using AtominaCraft.BlockGrid;

namespace AtominaCraft
{
    public class AtominaCraft : GameWindow
    {
        byte[] reserveMemory = new byte[1048576];
        public static AtominaCraft Instance { get; private set; }

        public List<World> Worlds { get; private set; }

        public EntityPlayerCamera Player { get; set; }

        public bool IsRunning { get; set; }
        public bool DrawDebug { get; set; }

        private ToggleButton FullscreenToggle { get; set; }
        private ToggleButton CursorGrappedToggle { get; set; }

        public AtominaCraft(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Instance = this;
        }

        public bool InitialiseGameWindow()
        {
            GC.AddMemoryPressure(33554432);
            FullscreenToggle = new ToggleButton((isToggled) =>
            {
                if (isToggled) 
                    WindowState = WindowState.Fullscreen; 
                else 
                    WindowState = WindowState.Normal;
            });
            CursorGrappedToggle = new ToggleButton((isToggled) =>
            {
                if (isToggled)
                {
                    CursorVisible = true;
                    CursorGrabbed = false;
                }
                else
                {
                    CursorVisible = false;
                    CursorGrabbed = true;
                }
            });

            Size = new OpenTK.Mathematics.Vector2i(1280, 720);
            IsRunning = false;

            UtilityLauncher.PreOpenGLLaunch();

            if (InitialiseOpenGL())
            {
                UtilityLauncher.AfterOpenGLLaunch();
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

                //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                //GL.ShadeModel(ShadingModel.Smooth);

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
                UtilityLauncher.PreGameLoopLaunch();
                Console.WriteLine("Starting Game Loop");
                base.Run();
            }
        }

        protected override void OnLoad()
        {
            // Called just after OpenGL is initialised
            //DebugText.CanWrite = true;

            // Initialise Game

            //BlockMesh.Generate();
            //ChunkMeshGenerator.Initialise();

            Player = new EntityPlayerCamera();
            Worlds = new List<World>();

            World earth = new World();
            earth.SetMainPlayer(Player);
            Player.MoveTo(new Vector3(0, 5, 0));

            // Top    1 
            // Front  1 
            // Left   1 
            // Bottom 1 
            // Right  1 
            // Back   1 

            // Top    2 
            // Front  2 
            // Left   2 
            // Bottom 2 
            // Right  2 
            // Back   2 

            //BlockFace topp = BlockFace.GenerateFace(FaceDirection.Top);
            //BlockFace fron = BlockFace.GenerateFace(FaceDirection.Front);
            //BlockFace left = BlockFace.GenerateFace(FaceDirection.Left);
            //BlockFace bott = BlockFace.GenerateFace(FaceDirection.Bottom);
            //BlockFace righ = BlockFace.GenerateFace(FaceDirection.Right);
            //BlockFace back = BlockFace.GenerateFace(FaceDirection.Back);
            //
            //topp.Face1.WriteVertices(vertices);
            //fron.Face1.WriteVertices(vertices);
            //left.Face1.WriteVertices(vertices);
            //bott.Face1.WriteVertices(vertices);
            //righ.Face1.WriteVertices(vertices);
            //back.Face1.WriteVertices(vertices);
            //topp.Face2.WriteVertices(vertices);
            //fron.Face2.WriteVertices(vertices);
            //left.Face2.WriteVertices(vertices);
            //bott.Face2.WriteVertices(vertices);
            //righ.Face2.WriteVertices(vertices);
            //back.Face2.WriteVertices(vertices);
            //
            //topp.Face1.WriteTextureCoordinates(uvs);
            //fron.Face1.WriteTextureCoordinates(uvs);
            //left.Face1.WriteTextureCoordinates(uvs);
            //bott.Face1.WriteTextureCoordinates(uvs);
            //righ.Face1.WriteTextureCoordinates(uvs);
            //back.Face1.WriteTextureCoordinates(uvs);
            //topp.Face2.WriteTextureCoordinates(uvs);
            //fron.Face2.WriteTextureCoordinates(uvs);
            //left.Face2.WriteTextureCoordinates(uvs);
            //bott.Face2.WriteTextureCoordinates(uvs);
            //righ.Face2.WriteTextureCoordinates(uvs);
            //back.Face2.WriteTextureCoordinates(uvs);

            //mesh = new Mesh(vertices, uvs);
            //obj = new GameObject()
            //{
            //    Mesh = mesh,
            //    Position = new Vector3(2.0f, 5.0f, 1.0f),
            //    Shader = GraphicsLoader.TextureShader,
            //    Texture = BlockTextureLinker.TextureMap[BlockTextureLinker.GetTextureNameFromID(4)]
            //};
            //Tesselator.Generate();
            //mesh = Tesselator.GenerateChunk(chunk1);

            //Chunk chunk1 = ChunkGenerator.GenerateFlat(earth, new ChunkLocation(-1, -1), 4);
            //Chunk chunk2 = ChunkGenerator.GenerateFlat(earth, new ChunkLocation(-1,  0), 3);
            //Chunk chunk3 = ChunkGenerator.GenerateFlat(earth, new ChunkLocation( 0, -1), 2);
            //Chunk chunk4 = ChunkGenerator.GenerateFlat(earth, new ChunkLocation( 0,  0), 1);

            /*
             * 
             * 
             *          Chunk block generators
             *          generates blocks... in a chunk :)))
             *          you could manually do them too
             * 
             * 
             */
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1, total = 1; z <= 1; z++, total++)
                {
                    Chunk chunk = ChunkGenerator.GenerateFlatGaps(earth, new ChunkLocation(x, z), 8, x + 3, z + 3);
                    //Chunk chunk = ChunkGenerator.GenerateFlat(earth, new ChunkLocation(x, z), 1);
                    BlockMeshGenerator.GenerateChunk(chunk);
                    earth.Chunks.Add(chunk.Location, chunk);
                }
            }

            Worlds.Add(earth);

            Inputs.Keyboard = KeyboardState;
            Inputs.Mouse = MouseState;

            CursorVisible = false;
            CursorGrabbed = true;

            base.OnLoad();
        }

        GameObject cube;

        public void UpdateGame()
        {
            try
            {
                if (KeyboardState.IsKeyDown(Keys.Escape))
                {
                    this.Close();
                    Forms.Application.Exit();
                }

                if (KeyboardState.IsKeyPressed(Keys.F3))
                {
                    DrawDebug = !DrawDebug;
                }

                if (KeyboardState.IsKeyPressed(Keys.E))
                    CursorGrappedToggle.ButtonDown();
                else
                    CursorGrappedToggle.ButtonUp();

                if (KeyboardState.IsKeyDown(Keys.F11))
                    FullscreenToggle.ButtonDown();
                else
                    FullscreenToggle.ButtonUp();

                // Day/night
                if (KeyboardState.IsKeyDown(Keys.I))
                {
                    Player.World.Sky.SkyColour += 0.002f;
                    Player.World.Sky.SkyColour.ClampInstance(0.1f, 1.0f);
                }
                if (KeyboardState.IsKeyDown(Keys.K))
                {
                    Player.World.Sky.SkyColour -= 0.002f;
                    Player.World.Sky.SkyColour.ClampInstance(0.1f, 1.0f);
                }

                // this does work but 
                // Block block = Player.Chunk.GetBlockAt(GridLatch.MTWGetWorldBlock(Player.World, Player.Position));
                // doesnt really... rip
                //if (MouseState.IsButtonDown(MouseButton.Left))
                //{
                //    Block block = Player.Chunk.GetBlockAt(GridLatch.MTWGetWorldBlock(Player.World, Player.Position));
                //    if (block != null)
                //        block.BreakNaturally();
                //}

                Player.World.Update();
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

            Player.World.Sky.Draw(Player.Camera);

            // mesh.Draw(Player.Camera);

            foreach (Chunk chunk in Player.World.Chunks.Values)
            {
                //Tesselator.DrawChunkBBB(Player.Camera, chunk);
                Tesselator.DrawChunkBlocks(Player.Camera, chunk);
                DebugDraw.DrawChunk(Player.Camera, chunk);
                //Tesselator.DrawChunkMesh(Player.Camera, chunk, TestMesh);
                //DebugDraw.DrawAABB(Player.Camera, Player.BoundingBox);
            }

            //cube.Draw(Player.Camera);
            DebugText.Clear();
            DebugText.WriteLine($"Player pos:   {Player.Position}");
            DebugText.WriteLine($"Player chunk: {Player.Chunk?.Location?.ToString()}");

            //if (Player.Chunk != null)
            //{
            //    DebugDraw.DrawChunkCenterOutline(Player.Camera, Player.Chunk);
            //}

            //DebugText.SetText(text);
            //GL.FrontFace(FrontFaceDirection.Cw);
            //obj.Draw(Player.Camera);
            //GL.FrontFace(FrontFaceDirection.Ccw);
            //DebugText.WriteLine($"Player Position: {Player.Position}");
            //DebugText.WriteLine($"Player Look:     X: {Player.CameraRotationX}, Y: {Player.CameraRotationY}");
            //DebugDraw.DrawAABB(Player.Camera, Player.BoundingBox);
            //DebugDraw.DrawXYZ(Player.Camera.Projection, Player.CameraRotationY, Player.CameraRotationX);

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            UpdateGame();
            Delta.Time = (float)args.Time;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            RenderGame();
        }
    }
}