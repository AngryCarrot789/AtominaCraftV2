using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Blocks.Rendering;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Chunks.Rendering
{
    public class ChunkMesh
    {
        private Chunk Chunk { get; }

        public Mesh Mesh { get; set; }

        public ChunkMesh(Chunk chunk)
        {
            Chunk = chunk;
        }
        
        public void GenerateMeshData()
        {
            List<float> vertices = new List<float>();
            List<float> uvs = new List<float>();

            for (int x = 0; x < Chunk.Width; x++)
            {
                for (int z = 0; z < Chunk.Width; z++)
                {
                    for (int y = 0; y < Chunk.Height; y++)
                    {
                        HashSet<BlockDirection> visible = GetVisibleSides(new BlockLocation(x, y, z));

                        vertices.AddRange(BlockMesh.GetCulled(visible));
                        uvs.AddRange(BlockMesh.GetUVs());
                    }
                }
            }

            Mesh = new Mesh(vertices, uvs);
        }

        public HashSet<BlockDirection> GetVisibleSides(BlockLocation location)
        {
            HashSet<BlockDirection> directions = new HashSet<BlockDirection>();
            Block above = Chunk.GetBlockAt(location.Translated( 0,  1,  0));
            Block below = Chunk.GetBlockAt(location.Translated( 0, -1,  0));
            Block forwa = Chunk.GetBlockAt(location.Translated( 0,  0, -1));
            Block right = Chunk.GetBlockAt(location.Translated( 1,  0,  0));
            Block backw = Chunk.GetBlockAt(location.Translated( 0,  0,  1));
            Block left  = Chunk.GetBlockAt(location.Translated(-1,  0,  0));
            if (IsBlockTransparent(above)) directions.Add(BlockDirection.Up);
            if (IsBlockTransparent(below)) directions.Add(BlockDirection.Down);
            if (IsBlockTransparent(forwa)) directions.Add(BlockDirection.North);
            if (IsBlockTransparent(right)) directions.Add(BlockDirection.East);
            if (IsBlockTransparent(backw)) directions.Add(BlockDirection.South);
            if (IsBlockTransparent(left )) directions.Add(BlockDirection.West);

            if (location.Y == 0)                directions.Add(BlockDirection.Down);
            if (location.Y == Chunk.Height - 1) directions.Add(BlockDirection.Up);
            if (location.X == 0)                directions.Add(BlockDirection.West);
            if (location.X == Chunk.Width - 1)  directions.Add(BlockDirection.East);
            if (location.Z == 0)                directions.Add(BlockDirection.North);
            if (location.Z == Chunk.Width - 1)  directions.Add(BlockDirection.South);

            return directions;
        }

        public bool IsBlockTransparent(Block block)
        {
            return block == null || block.IsTransparent;
        }

        public void Draw(PlayerCamera camera)
        {
            Matrix4 mv = WorldToLocal().Transposed();
            Matrix4 mvp = camera.Matrix() * LocalToWorld();
            BlockTextureLinker.TextureMap["dirt"].Use();
            GraphicsLoader.TextureShader.Use();
            GraphicsLoader.TextureShader.SetMatrix(mvp, mv);
            Mesh.Draw();
        }

        public Matrix4 LocalToWorld()
        {
            return Matrix4.CreateLocalToWorld(Chunk.Location.GetWorldSpaceCenter(), Vector3.Zero, Vector3.Ones);
        }

        public Matrix4 WorldToLocal()
        {
            return Matrix4.CreateWorldToLocal(Chunk.Location.GetWorldSpaceCenter(), Vector3.Zero, Vector3.Ones);
        }
    }
}
