using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AtominaCraft.Client.BlockRendering.Mesh.Generator;
using AtominaCraft.Entities.Player;
using AtominaCraft.Utils;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZLaunch;
using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Controls;
using AtominaCraft.ZResources.GFX;
using AtominaCraft.ZResources.Logging;
using REghzy.MathsF;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using REghZy.MathsF;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace AtominaCraft {
    public class AtominaCraft : GameWindow {
        private ToggleButton _cursorGrabbedToggle;

        private ToggleButton _fullscreenToggle;

        private GameObject cube;
        public bool DrawDebug;

        public bool IsRunning;

        public EntityPlayerCamera Player;

        public AtominaCraft(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) {
            Instance = this;
        }

        public static AtominaCraft Instance { get; private set; }

        public List<World> Worlds { get; private set; }

        public bool InitialiseGameWindow() {
            // GC.AddMemoryPressure(33554432);
            this._fullscreenToggle = new ToggleButton(isToggled => {
                if (isToggled)
                    this.WindowState = WindowState.Fullscreen;
                else
                    this.WindowState = WindowState.Normal;
            });
            this._cursorGrabbedToggle = new ToggleButton(isToggled => {
                if (isToggled) {
                    this.CursorVisible = true;
                    this.CursorGrabbed = false;
                }
                else {
                    this.CursorVisible = false;
                    this.CursorGrabbed = true;
                }
            });

            this.Size = new Vector2i(1280, 720);
            this.IsRunning = false;

            UtilityLauncher.PreOpenGLLaunch();

            if (InitialiseOpenGL()) {
                UtilityLauncher.AfterOpenGLLaunch();
                LogManager.OpenGLLogger.Log("Successfully initialised Game Engine");
                return true;
            }

            LogManager.OpenGLLogger.Log("Failed to initialise Game Engine");
            return false;
        }

        public bool InitialiseOpenGL() {
            GLFWBindingsContext binding = new GLFWBindingsContext();
            GL.LoadBindings(binding);

            if (GLFW.Init()) {
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

            Console.WriteLine("Failed to initialise GLFW and OpenGL");
            return false;
        }

        public void RunGameLoop() {
            if (!this.IsRunning) {
                this.IsRunning = true;
                UtilityLauncher.PreGameLoopLaunch();
                Console.WriteLine("Starting Game Loop");
                base.Run();
            }
        }

        protected override void OnLoad() {
            // Called just after OpenGL is initialised
            //DebugText.CanWrite = true;

            // Initialise Game

            //BlockMesh.Generate();
            //ChunkMeshGenerator.Initialise();

            this.Player = new EntityPlayerCamera();
            this.Worlds = new List<World>();

            World earth = new World();
            earth.SetMainPlayer(this.Player);
            this.Player.MoveTo(new Vector3f(0, 5, 0));

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
            // Chunk chunk;
            // for (int x = -1; x <= 1; x++) {
            //     for (int z = -1, total = 1; z <= 1; z++, total++) {
            //         chunk = ChunkGenerator.GenerateFlatGaps(earth, new ChunkLocation(x, z), 8, x + 3, z + 4);
            //         earth.chunks.Put(chunk.coords.GetLongHash(), chunk);
            //     }
            // }

            Chunk chunk = earth.GetChunkAt(0, 0);
            for (int i = 0; i < 64; i++) {
                chunk.SetBlockAt(0, i, 0, 2);
            }

            int x = 0;
            int y = 0;
            int z = 0;
            int state = 0;
            bool flag1 = false;
            for (int i = 0; i < 256; i++) {
                chunk.SetBlockAt(x, y, z, 1);
                z = ++z & 15;
                x++;
                if (++state == 8) {
                    y++;
                    state = 0;
                }

                if (flag1) {
                    x--;
                }
                else {
                    x++;
                }

                if (x >= 15) {
                    flag1 = true;
                }
                else if (x <= 0) {
                    flag1 = false;
                }
            }

            this.Worlds.Add(earth);

            Inputs.Keyboard = this.KeyboardState;
            Inputs.Mouse = this.MouseState;

            this.CursorVisible = false;
            this.CursorGrabbed = true;

            base.OnLoad();
        }

        public void UpdateGame() {
            try {
                if (this.KeyboardState.IsKeyDown(Keys.Escape)) {
                    Close();
                    Application.Exit();
                }

                if (this.KeyboardState.IsKeyPressed(Keys.F3))
                    this.DrawDebug = !this.DrawDebug;

                if (this.KeyboardState.IsKeyPressed(Keys.E))
                    this._cursorGrabbedToggle.ButtonDown();
                else
                    this._cursorGrabbedToggle.ButtonUp();

                if (this.KeyboardState.IsKeyDown(Keys.F11))
                    this._fullscreenToggle.ButtonDown();
                else
                    this._fullscreenToggle.ButtonUp();

                // Day/night
                if (this.KeyboardState.IsKeyDown(Keys.I)) {
                    this.Player.World.Sky.SkyColour += 0.002f;
                    this.Player.World.Sky.SkyColour.ClampInstance(0.1f, 1.0f);
                }

                if (this.KeyboardState.IsKeyDown(Keys.K)) {
                    this.Player.World.Sky.SkyColour -= 0.002f;
                    this.Player.World.Sky.SkyColour.ClampInstance(0.1f, 1.0f);
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

                this.Player.World.Update();
            }
            catch (Exception exception) {
                // CrashReport start = new CrashReport(exception, "Game Loop", "Exception in game loop");
                throw exception;
                //ShutdownGame();
            }
        }

        public void RenderGame() {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.2f, 0.2f, 0.8f, 1.0f);

            this.Player.Camera.worldView = this.Player.WorldToCamera();
            this.Player.Camera.SetSize(this.Size.X, this.Size.Y, GameSettings.RENDER_NEAR_MIN, GameSettings.RENDER_FAR);
            this.Player.Camera.UseViewport();

            this.Player.World.Sky.Draw(this.Player.Camera);

            // mesh.Draw(Player.Camera);

            EntityPlayerCamera player = this.Player;
            BlockWorldCoord playerPos = player.BlockPositon;
            const int range = 10;
            for (int cX = (playerPos.x >> 4) - range, endX = cX + (range * 2); cX <= endX; ++cX) {
                for (int cZ = (playerPos.z >> 4) - range, endZ = cZ + (range * 2); cZ <= endZ; ++cZ) {
                    if (player.World.TryGetChunk(new ChunkLocation(cX, cZ), out Chunk chunk)) {
                        Tesselator.DrawChunkBlocks(player.Camera, chunk);
                        DebugDraw.DrawChunk(player.Camera, chunk);
                    }
                }
            }

            if (player.Chunk != null) {
                DebugDraw.DrawChunkCenterOutline(this.Player.Camera, this.Player.Chunk);
            }

            // foreach (Chunk chunk in this.Player.World.chunks.Values) {
            //     // Tesselator.DrawChunkBBB(Player.Camera, chunk);
            //     Tesselator.DrawChunkBlocks(this.Player.Camera, chunk);
            //     DebugDraw.DrawChunk(this.Player.Camera, chunk);
            //     //Tesselator.DrawChunkMesh(Player.Camera, chunk, TestMesh);
            //     if (this.Player.Chunk != null)
            //         DebugDraw.DrawChunkCenterOutline(this.Player.Camera, this.Player.Chunk);
            //     // DebugDraw.DrawAABB(Player.Camera, Player.BoundingBox);
            //     // DebugDraw.DrawXYZ(Player.Camera.Matrix(), this.Player.CameraRotationX, this.Player.CameraRotationY);
            // }

            //cube.Draw(Player.Camera);
            // DebugText.Clear();
            // DebugText.WriteLine($"Player pos:   {this.Player.Position}");
            // DebugText.WriteLine($"Player chunk: {this.Player.Chunk?.Location}");

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
            DebugDraw.DrawXYZ(this.Player.Camera.projection, this.Player.camRotY, this.Player.camRotX);
            this.Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            UpdateGame();
            Delta.Time = (float) args.Time;
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            RenderGame();
        }
    }
}