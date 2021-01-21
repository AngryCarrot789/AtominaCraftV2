using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.ZResources.Maths;
using System.Collections.Generic;
using System.Security.Policy;

namespace AtominaCraft.Worlds.Chunks
{
    public class Chunk
    {
        public ChunkLocation Location { get; set; }
        public World World { get; set; }

        public Dictionary<BlockLocation, Block> Blocks { get; set; }

        public Chunk(World world, ChunkLocation location)
        {
            Blocks = new Dictionary<BlockLocation, Block>();
            Location = location;
            World = world;
        }

        public void Update()
        {

        }

        public Block GetBlockAt(int x, int y, int z)
        {
            return GetBlockAt(new BlockLocation(x, y, z));
        }

        public Block GetBlockAt(BlockLocation location)
        {
            Blocks.TryGetValue(location, out Block block);
            return block;
        }
    }
}
