using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Utils;

namespace AtominaCraft.Worlds.Chunks {
    /// <summary>
    ///     A simple class used to generate blocks within a chunk. Generates going upwards, generating the Z axis and across
    ///     the X axis
    /// </summary>
    public static class ChunkGenerator {
        public static Chunk GenerateFlat(World world, ChunkLocation location, int height) {
            Chunk chunk = new Chunk(world, location);
            for (int y = 0; y < height; y++)
                FillChunkLayer(chunk, Block.Dirt, y);
            //for (int x = 0; x < GridLatch.ChunkWidth; x++)
            //{
            //    for (int z = 0; z < GridLatch.ChunkWidth; z++)
            //    {
            //        BlockLocation blockLocation = new BlockLocation(chunk, x, y, z);
            //        Block block = new Block(world, blockLocation);
            //        chunk.Blocks.Add(blockLocation, block);
            //        // a cheap way to decrease lag lol 
            //        //if (x == 0 || x == GridLatch.ChunkIndexableWidth || z == 0 || z == GridLatch.ChunkIndexableWidth || y == 0 || y == (height - 1))
            //        //    block.ShouldRender = true;
            //        //else
            //        //    block.ShouldRender = false;
            //    }
            //}
            return chunk;
        }

        public static Chunk GenerateFlatGaps(World world, ChunkLocation location, int height, int fillHeight, int gapHeight) {
            Chunk chunk = new Chunk(world, location);
            Block block = Block.Dirt;
            for (int i = 0, toFill = fillHeight; i < height; i += gapHeight, toFill += gapHeight + fillHeight) {
                for (int y = i; y < toFill; y++, i++) {
                    FillChunkLayer(chunk, block, y);
                    block = Block.Electromagnet;
                }

                block = Block.Snow;
            }

            //GenerateChunkLayer(world, chunk, y);

            return chunk;
        }

        public static void FillChunkLayer(Chunk chunk, Block block, int y) {
            for (int x = 0; x < GridLatch.ChunkWidth; x++) {
                for (int z = 0; z < GridLatch.ChunkWidth; z++) {
                    chunk.SetBlockAt(x, y, z, block.id);
                }
            }
        }
    }
}