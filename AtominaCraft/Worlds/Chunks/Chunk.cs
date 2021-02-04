using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace AtominaCraft.Worlds.Chunks
{
    public class Chunk
    {
        public const int Height = 256;
        public const int Width = 16;

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
            return GetBlockAt(new BlockLocation(this, x, y, z));
        }

        public Block GetBlockAt(BlockLocation location)
        {
            try
            {
                int hash = location.GetHashCode();
                Blocks.TryGetValue(location, out Block block);
                return block;
            }
            catch { return null; }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(World.GetHashCode(), Location.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            Chunk chunk = (Chunk)obj;
            return chunk.Location.Equals(Location);
        }
    }
}
