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
using AtominaCraft.Worlds.Chunks.MeshGeneration.Face;
using AtominaCraft.Blocks.Rendering;
using AtominaCraft.Worlds.Chunks.MeshGeneration;
using System.Text;
using System.IO;
using AtominaCraft.BlockGrid;

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

                GraphicsLoader.Load();

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

        //private ChunkMesh TestMesh;

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

            for (int x = -2; x <= 2; x++)
            {
                for (int z = -2, total = 1; z <= 2; z++, total++)
                {
                    Chunk chunk = ChunkGenerator.GenerateFlat(earth, new ChunkLocation(x, z), total);
                    earth.Chunks.Add(chunk.Location, chunk);
                }
            }

            //Chunk chunk = ChunkGenerator.GenerateFlat(earth, new ChunkLocation(0, 0), 1);
            //earth.Chunks.Add(chunk.Location, chunk);
            //
            //TestMesh = ChunkMeshGenerator.GenerateChunk(chunk);
            //
            //StringBuilder sb = new StringBuilder(10000000);
            //
            //for (int i = 0; i < TestMesh.Vertices.Count; i += 3)
            //{
            //    float vertex1 = (float)TestMesh.Vertices[i + 0];
            //    float vertex2 = (float)TestMesh.Vertices[i + 1];
            //    float vertex3 = (float)TestMesh.Vertices[i + 2];
            //    sb.AppendLine($"v {vertex1} {vertex2} {vertex3}");
            //}
            //
            //File.WriteAllText(@"C:\Users\kettl\Desktop\suckoff1.txt", sb.ToString());

            //ChunkMeshGenerator.GenerateChunk(chunk);
            //earth.Chunks.Add(chunk1.Location, chunk1);
            //earth.Chunks.Add(chunk2.Location, chunk2);
            //earth.Chunks.Add(chunk3.Location, chunk3);
            //earth.Chunks.Add(chunk4.Location, chunk4);
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
                    Forms.Application.Exit();
                }

                if (KeyboardState.IsKeyPressed(Keys.F3))
                {
                    DrawDebug = !DrawDebug;
                }

                if (KeyboardState.IsKeyPressed(Keys.E))
                {
                    CursorVisible = true;
                    CursorGrabbed = false;
                }

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

            // less harsh on the GC than creating 100s of vectors every second
            foreach (Chunk chunk in Player.World.Chunks.Values)
            {
                Tesselator.DrawChunkBBB(Player.Camera, chunk);
                DebugDraw.DrawChunk(Player.Camera, chunk);
                //Tesselator.DrawChunkMesh(Player.Camera, chunk, TestMesh);
                //DebugDraw.DrawAABB(Player.Camera, Player.BoundingBox);
            }

            DebugText.Clear();

            if (Player.Chunk != null)
            {
                DebugDraw.DrawChunkCenterOutline(Player.Camera, Player.Chunk);
            }

            DebugText.WriteLine(Player.Position.ToString());

            //DebugText.SetText(text);
            //GL.FrontFace(FrontFaceDirection.Cw);
            //obj.Draw(Player.Camera);
            //GL.FrontFace(FrontFaceDirection.Ccw);
            //DebugText.WriteLine($"Player Position: {Player.Position}");
            //DebugText.WriteLine($"Player Look:     X: {Player.CameraRotationX}, Y: {Player.CameraRotationY}");
            //DebugDraw.DrawAABB(Player.Camera, Player.BoundingBox);
            DebugDraw.DrawXYZ(Player.Camera.Projection, Player.CameraRotationY, Player.CameraRotationX);

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Delta.Time = (float)args.Time;
            UpdateGame();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            RenderGame();
        }
    }
}