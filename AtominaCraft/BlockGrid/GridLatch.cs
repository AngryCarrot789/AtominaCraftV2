using AtominaCraft.Blocks;
using AtominaCraft.Memory.Pools;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Maths;
using System;

namespace AtominaCraft.BlockGrid
{
    /// <summary>
    /// A class used for calculating 'matrix locations' (aka, locations used by transformation matrixes) 
    /// based on grid-latched 'world location', and vise versa, e.g. getting the world coordinates of a block, chunk locations, etc
    /// </summary>
    public static class GridLatch
    {
        public const float BLOCK_SCALE = 0.5f;
        public const float BLOCK_SIZE = BLOCK_SCALE * 2;
        public static Vector3 BlockScale = new Vector3(BLOCK_SCALE, BLOCK_SCALE, BLOCK_SCALE);
        public static Vector3 ChunkScale = new Vector3(8, 128, 8);


        // Stops having to create 100000s of vectors per second
        private static BlockLocation TempB { get; set; }

        static GridLatch()
        {
            TempB = new BlockLocation(0, 0, 0);
        }

        /// <summary>
        /// Offsets a block's location based on the chunk position
        /// </summary>
        /// <param name="block">The location of a block in any axis</param>
        /// <param name="chunk">The location of the chunk in any axis</param>
        /// <returns></returns>
        public static int GetBlockXZOffsetByChunk(int block, int chunk)
        {
            return (chunk << 4) + block;
        }

        /// <summary>
        /// Returns a copy of the given block location offset by the chunk location
        /// </summary>
        /// <param name="block"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public static BlockLocation GetBlockOffsetByChunk(BlockLocation block, ChunkLocation chunk)
        {
            TempB.Set(
                GetBlockXZOffsetByChunk(block.X, chunk.X),
                block.Y,
                GetBlockXZOffsetByChunk(block.Z, chunk.Z));
            return TempB;
        }

        /// <summary>
        /// Returns a blank block location (starting at 0,0) offset by a given chunk location
        /// </summary>
        /// <param name="chunkLocation"></param>
        /// <returns></returns>
        public static BlockLocation GetBlankChunkOffset(ChunkLocation chunkLocation)
        {
            TempB.Set(
                chunkLocation.X << 4,
                0,
                chunkLocation.Z << 4);
            return TempB;
        }

        //
        //     OpenGL/Matrix stuff
        //
        //      
        //          World to matrix
        //
        //
        //

        /// <summary>
        /// Gets the transformation matrix world coordinates of a block based on its location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 WTMGetBlock(int x, int y, int z)
        {
            return new Vector3(
                x + BLOCK_SCALE,
                y + BLOCK_SCALE,
                z + BLOCK_SCALE);
        }

        /// <summary>
        /// Gets the transformation matrix world coordinates of a block based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3 WTMGetBlock(BlockLocation location)
        {
            return new Vector3(
                location.X + BLOCK_SCALE,
                location.Y + BLOCK_SCALE,
                location.Z + BLOCK_SCALE);
        }

        /// <summary>
        /// Gets the transformation matrix world coordinates of a chunk based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3 WTMGetChunk(int x, int z)
        {
            return new Vector3(
                (x << 4) + (Chunk.Width / 2),
                Chunk.Height / 2,
                (z << 4) + (Chunk.Width / 2));
        }

        /// <summary>
        /// Gets the transformation matrix world coordinates of a chunk based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3 WTMGetChunk(ChunkLocation location)
        {
            return new Vector3(
                (location.X << 4) + (Chunk.Width / 2),
                Chunk.Height / 2,
                (location.Z << 4) + (Chunk.Width / 2));
        }

        //
        //  Matrix to world
        //

        /// <summary>
        /// Gets the world coordinates of a block from the Matrix location
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static BlockLocation MTWGetBlock(Vector3 pos)
        {
            TempB.Set(
                (int)Math.Floor(pos.X),
                (int)Math.Floor(pos.Y),
                (int)Math.Floor(pos.Z));
            return TempB;
        }
    }
}
