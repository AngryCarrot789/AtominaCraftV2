using AtominaCraft.Blocks;
using System;
using System.Collections.Generic;

namespace AtominaCraft.Worlds.Chunks
{
    public class Chunk
    {
        //public const int Height = 256;
        //public const int Width = 16;
        //public const int IndexableHeight = 255;
        //public const int IndexableWidth = 15;

        // These are editable values btw. If the math breaks when changing these... something isnt right
        public const int Height = 16;
        public const int Width = 4;
        public const int IndexableHeight = Height - 1;
        public const int IndexableWidth = Width - 1;

        private static BlockLocation TemporarBlockLocation = new BlockLocation(null, 0, 0, 0);

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
            TemporarBlockLocation.Set(this, x, y, z);
            return GetBlockAt(TemporarBlockLocation);
        }

        public Block GetBlockAt(BlockLocation location)
        {
            try
            {
                int hash = location.GetHashCode();
                Blocks.TryGetValue(location, out Block block);
                if (block == null)
                    return null;
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
