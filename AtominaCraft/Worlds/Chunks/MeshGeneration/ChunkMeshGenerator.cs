using AtominaCraft.Blocks;
using AtominaCraft.Worlds.Chunks.MeshGeneration.Face;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Chunks.MeshGeneration
{
    public static class ChunkMeshGenerator
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

        public static ChunkMesh GenerateChunk(Chunk chunk)
        {
            List<float> vertices = new List<float>();
            List<float> uvs = new List<float>();
            List<float> normals = new List<float>();

            for(int y = 0; y < Chunk.Height; y++)
            {
                for (int x = 0; x < Chunk.Width; x++)
                {
                    for (int z = 0; z < Chunk.Width; z++)
                    {
                        Block block = chunk.GetBlockAt(x, y, z);
                        if (block == null) continue;

                        //BlockFaceVisibility visibility = GetVisibleFaces(block);
                        BlockFaceVisibility visibility = new BlockFaceVisibility(true, true, true, true, true, true);
                        BlockMesh blockMesh = GenerateBlock(visibility);
                        blockMesh.WriteVertices(vertices);
                        blockMesh.WriteUVs(uvs);
                        blockMesh.WriteNormals(normals, vertices);
                    }
                }
            }

            ChunkMesh mesh = new ChunkMesh(vertices, uvs, normals);
            return mesh;
        }

        public static BlockMesh GenerateBlock(BlockFaceVisibility visibility)
        {
            return BlockMesh.GenerateBlockMesh(
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
            Block back = chunk.GetBlockAt(location.X, location.Y, location.Z + 1);
            Block frnt = chunk.GetBlockAt(location.X, location.Y, location.Z - 1);

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
