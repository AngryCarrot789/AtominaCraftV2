﻿using AtominaCraft.Blocks;
using AtominaCraft.Client.BlockRendering.Mesh;
using System;
using System.Collections.Generic;

namespace AtominaCraft.Worlds.Chunks
{
    public class Chunk
    {
        private static BlockLocation TemporarBlockLocation = new BlockLocation(null, 0, 0, 0);

        public ChunkLocation Location { get; set; }
        public World World { get; set; }

        public Dictionary<BlockLocation, Block> Blocks { get; set; }

        public bool HaltMeshRegeneration { get; set; }

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
                Blocks.TryGetValue(location, out Block block);
                if (block == null)
                    return null;
                return block;
            }
            catch { return null; }
        }

        // probably a bit inefficienct but eh
        public void SetBlockAt(BlockLocation location, Block block)
        {
            if (Blocks.TryGetValue(location, out Block unused))
            {
                Blocks[location] = block;
            }
            else
            {
                Blocks.Add(location, block);
            }
        }

        /// <summary>
        /// Checks if a block at the given location exists 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool DoesBlockExist(BlockLocation location)
        {
            return Blocks.TryGetValue(location, out Block unused);
        }

        /// <summary>
        /// Removes the given block from the chunk (if it exists in there... that is)
        /// </summary>
        /// <param name="location"></param>
        public void BreakBlockNaturally(Block block)
        {
            if (block.HasLocation())
            {
                BreakBlockNaturally(block.Location);
            }
        }

        /// <summary>
        /// Removes the block at the given location from the chunk (if it exists in there... that is)
        /// </summary>
        /// <param name="location"></param>
        public void BreakBlockNaturally(BlockLocation location)
        {
            if (DoesBlockExist(location))
            {
                Blocks.Remove(location);
                RegenerateMesh();
            }
        }

        public void RegenerateMesh()
        {
            if (!HaltMeshRegeneration)
            {
                WorldMeshMap.RegenerateChunk(this);
            }
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
