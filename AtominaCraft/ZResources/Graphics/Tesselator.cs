using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Client.BlockRendering;
using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.Entities.Player;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;

namespace AtominaCraft.ZResources.Graphics
{
    public static class Tesselator
    {
        public static GameObject Cube { get; set; }
        public static BlockObject BlockObject { get; set; }

        static Tesselator()
        {
            Cube = new GameObject();
            Cube.Shader = GraphicsLoader.TextureShader;
            Cube.Mesh = GraphicsLoader.Cube;

            BlockObject = new BlockObject();
            BlockObject.Shader = GraphicsLoader.TextureShader;
            BlockObject.Mesh = new CubeMesh(Cube.Mesh.Vertices, Cube.Mesh.UVs, Cube.Mesh.Normals);
            BlockObject.Texture = TextureMap.GetTexture("dirt");
        }

        public static List<float> BlockUVs;

        public static void DrawChunkBBB(PlayerCamera camera, Chunk chunk)
        {
            foreach (Block block in chunk.Blocks.Values)
            {
                if (block.ShouldRender)
                {
                    BlockLocation blockLocation = block.Location.GetWorldLocation(chunk.Location);
                    if (!block.IsEmpty())
                    {
                        //DebugDraw.DrawCube(camera, GridLatch.GetBlockWorldSpace(blockLocation), GridLatch.BlockScale);
                        //DebugDraw.DrawAABB(camera, block.BoundingBox);
                        DrawBlock(
                            block,
                            blockLocation.X,
                            blockLocation.Y,
                            blockLocation.Z,
                            camera);
                    }
                }
            }
        }

        public static void DrawChunkBlocks(PlayerCamera camera, Chunk chunk)
        {
            foreach (KeyValuePair<Block, CubeMesh> pair in WorldMeshMap.GetChunkCubeMeshMap(chunk))
            {
                if (pair.Key.ShouldRender)
                {
                    BlockObject.Mesh = pair.Value;
                    BlockObject.Texture = TextureMap.GetTextureFromID(pair.Key.ID);
                    BlockObject.Position = GridLatch.WTMGetWorldBlock(chunk.Location, pair.Key.Location);
                    BlockObject.Draw(camera);
                }
            }
        }

        public static void DrawBlock(Block block, int x, int y, int z, PlayerCamera camera)
        {
            string textureName = TextureMap.GetTextureNameFromID(block.ID);
            if (textureName == "")
                return;

            Texture texture = TextureMap.GetTexture(textureName);
            Cube.Texture = texture;
            Cube.Position = GridLatch.WTMGetBlock(x, y, z);
            Cube.Draw(camera);
        }

        //public static ChunkMesh GenerateChunk(Chunk chunk)
        //{
        //    List<float> vertices = new List<float>(1024);
        //    //List<float> uvs = new List<float>(1024);
        //    for (int y = 0; y < Chunk.Height - 1; y++)
        //    {
        //        for (int x = 0; x < Chunk.Width - 1; x++)
        //        {
        //            for (int z = 0; z < Chunk.Width - 1; z++)
        //            {
        //                Block block = chunk.GetBlockAt(x, y, z);
        //                List<float> verts = GenerateBlockMesh(block);
        //                if (verts != null)
        //                    vertices.AddRange(verts);
        //            }
        //        }
        //        vertices.Clear();
        //    }
        //    ChunkMesh mesh = new ChunkMesh(chunk, vertices, BlockUVs);
        //    return mesh;
        //}

        //public static List<float> GetCulled(HashSet<BlockDirection> directionsVisible)
        //{
        //    List<float> culled = new List<float>();
        //    foreach(BlockDirection direction in directionsVisible)
        //    {
        //        switch (direction)
        //        {
        //            case BlockDirection.Up:    culled.AddRange(TopVertices);    break;
        //            case BlockDirection.Down:  culled.AddRange(BottomVertices); break;
        //            case BlockDirection.North: culled.AddRange(BackVertices);   break;
        //            case BlockDirection.South: culled.AddRange(FrontVertices);  break;
        //            case BlockDirection.East:  culled.AddRange(RightVertices);  break;
        //            case BlockDirection.West:  culled.AddRange(LeftVertices);   break;
        //        }
        //    }
        //    return culled;
        //}

        //public static float[] GetUVs()
        //{
        //    return BlockUVs;
        //}
    }
}
