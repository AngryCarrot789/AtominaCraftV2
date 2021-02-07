using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Chunks
{
    public static class ChunkGenerator
    {
        public static Chunk GenerateFlat(World world, ChunkLocation location, int height)
        {
            Chunk chunk = new Chunk(world, location);
            for(int x = 0; x < Chunk.Width; x++)
            {
                for (int z = 0; z < Chunk.Width; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        BlockLocation blockLocation = new BlockLocation(chunk, x, y, z);
                        Block block = new Block(world, blockLocation);
                        if (x == 0 || x == Chunk.IndexableWidth || z == 0 || z == Chunk.IndexableWidth || y == 0 || y == (height - 1))
                            block.ShouldRender = true;
                        else
                            block.ShouldRender = false;
                        chunk.Blocks.Add(blockLocation, block);
                    }
                }
            }
            return chunk;
        }
    }
}
