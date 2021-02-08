using AtominaCraft.Blocks;
using AtominaCraft.Client.BlockRendering.Mesh.Generator;
using AtominaCraft.Worlds.Chunks;
using System.Collections.Generic;

namespace AtominaCraft.Client.BlockRendering.Mesh
{
    public static class WorldMeshMap
    {
        private static Dictionary<Chunk, Dictionary<Block, CubeMesh>> ChunkCubeMeshMap;

        public static void Initialise()
        {
            ChunkCubeMeshMap = new Dictionary<Chunk, Dictionary<Block, CubeMesh>>(8);
        }

        public static void ClearUnusedBlocks(Chunk chunk)
        {
            List<Block> unusedBlocks = new List<Block>(ChunkCubeMeshMap.Count);
            if (ChunkCubeMeshMap.TryGetValue(chunk, out Dictionary<Block, CubeMesh> cubeMeshMap))
            {
                foreach (Block block in cubeMeshMap.Keys)
                {
                    if (block.World == null)
                    {
                        unusedBlocks.Add(block);
                    }
                }
                foreach (Block block in unusedBlocks)
                {
                    cubeMeshMap.Remove(block);
                }
            }
        }

        public static Dictionary<Block, CubeMesh> GetChunkCubeMeshMap(Chunk chunk)
        {
            return ChunkCubeMeshMap[chunk];
        }

        public static void AddChunkCubeMeshMap(Chunk chunk, Dictionary<Block, CubeMesh> map)
        {
            ChunkCubeMeshMap.Add(chunk, map);
        }
    }
}
