using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Worlds.Chunks;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtominaCraft.Client.BlockRendering.Mesh.Generator
{
    public static class BlockMeshGenerator
    {
        public struct BlockFaceVisibility
        {
            public bool Top, Front, Left, Bottom, Right, Back;
            public BlockFaceVisibility(bool top, bool front, bool left, bool bottom, bool right, bool back)
            {
                Top = top;
                Front = front;
                Left = left;
                Bottom = bottom;
                Right = right;
                Back = back;
            }
        }

        public static void GenerateChunk(Chunk chunk)
        {
            Dictionary<Block, CubeMesh> blockMeshes = new Dictionary<Block, CubeMesh>(chunk.Blocks.Count);
            List<float> vertices = new List<float>(108);
            List<float> uvs = new List<float>(108);
            List<float> normals = new List<float>(108);

            for (int y = 0; y < GridLatch.ChunkHeight; y++)
            {
                for (int x = 0; x < GridLatch.ChunkWidth; x++)
                {
                    for (int z = 0; z < GridLatch.ChunkWidth; z++)
                    {
                        Block block = chunk.GetBlockAt(x, y, z);
                        if (block == null) continue;

                        BlockMeshBuilder meshBuilder = GenerateBlock(GetVisibleFaces(block));
                        meshBuilder.WriteVertices(vertices);
                        meshBuilder.WriteUVs(uvs);
                        meshBuilder.WriteNormals(normals, vertices);

                        CubeMesh mesh = new CubeMesh(vertices.ToList(), uvs.ToList(), normals.ToList())
                        {
                            Location = block.Location
                        };
                        blockMeshes.Add(block, mesh);

                        vertices.Clear();
                        uvs.Clear();
                        normals.Clear();
                    }
                }
            }

            WorldMeshMap.AddChunkCubeMeshMap(chunk, blockMeshes);
        }

        public static BlockMeshBuilder GenerateBlock(BlockFaceVisibility visibility)
        {
            return BlockMeshBuilder.GenerateBlockMesh(
                visibility.Top,
                visibility.Front,
                visibility.Left,
                visibility.Bottom,
                visibility.Right,
                visibility.Back);
        }

        public static BlockFaceVisibility GetVisibleFaces(Block block)
        {
            BlockFaceVisibility visibility = new BlockFaceVisibility();
            Chunk chunk = block.Location.Chunk;
            BlockLocation location = block.Location;
        
            Block left = chunk.GetBlockAt(location.X - 1, location.Y, location.Z);
            Block righ = chunk.GetBlockAt(location.X + 1, location.Y, location.Z);
            Block topp = chunk.GetBlockAt(location.X, location.Y + 1, location.Z);
            Block botm = chunk.GetBlockAt(location.X, location.Y - 1, location.Z);
            Block back = chunk.GetBlockAt(location.X, location.Y, location.Z - 1);
            Block frnt = chunk.GetBlockAt(location.X, location.Y, location.Z + 1);
        
            visibility.Left   = (left == null || left.IsEmpty() || left.IsTransparent);
            visibility.Right  = (righ == null || righ.IsEmpty() || righ.IsTransparent);
            visibility.Top    = (topp == null || topp.IsEmpty() || topp.IsTransparent);
            visibility.Bottom = (botm == null || botm.IsEmpty() || botm.IsTransparent);
            visibility.Back   = (back == null || back.IsEmpty() || back.IsTransparent);
            visibility.Front  = (frnt == null || frnt.IsEmpty() || frnt.IsTransparent);
            return visibility;
        }
    }
}
