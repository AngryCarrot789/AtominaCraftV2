using AtominaCraft.Blocks;
using AtominaCraft.Memory.Pools;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Maths;
using System;

namespace AtominaCraft.BlockGrid
{
    /// <summary>
    ///     A class used for calculating 'matrix locations' (aka, locations used by transformation matrixes) 
    ///     based on grid-latched 'world location', and vise versa, e.g. getting the world coordinates of a block, chunk locations, etc
    /// 
    ///     <para>
    ///         functions that return Matrixes/floats are normally called "WTM"; WorldToMatrix,
    ///     </para>
    ///     <para>
    ///         functions that return integers are normally called "MTW"; MatrixToWorld.
    ///     </para>
    ///     <para>
    ///         dont get confused for the "LocalToWorld", "CameraToMatrix" sort of functions... these are not those :)
    ///     </para>
    /// </summary>
    public static class GridLatch
    {
        /// <summary>
        /// The "Matrix" scale of a block, in theory the legnth starting at the center going to the edges
        /// <para>
        /// DONT EDIT THIS OTHERWISE THE ENTIRE WROLD WILL BREAK XDXDXDXD
        /// </para>
        /// </summary>
        public const float BlockScaleSide = 0.5f;
        /// <summary>
        /// The total "length/size" of a block, in theory starting at the corners not the center
        /// <para>
        ///     This CANNOT be a decimal number. it must be an integral (aka "integer based") number because it does
        /// </para>
        /// </summary>
        public const int BlockWidth = 1;

        //public const int ChunkHeight = 256;
        //public const int ChunkWidth = 16;
        //public const int ChunkIndexableHeight = 255;
        //public const int ChunkIndexableWidth = 15;

        // These are editable values btw. If the math breaks when changing these... something isnt right
        public const int ChunkHeight = 64;
        public const int ChunkWidth = 8;
        public const int ChunkIndexableHeight = ChunkHeight - 1;
        public const int ChunkIndexableWidth = ChunkWidth - 1;

        public static Vector3 BlockScale = new Vector3(BlockScaleSide, BlockScaleSide, BlockScaleSide);
        public static Vector3 ChunkScale = new Vector3(ChunkWidth / 2, ChunkHeight / 2, ChunkWidth / 2);


        // Stops having to create 100000s of vectors per second
        private static BlockLocation TempB { get; set; }
        private static ChunkLocation TempC { get; set; }

        static GridLatch()
        {
            TempB = new BlockLocation(0, 0, 0);
            TempC = new ChunkLocation(0, 0);
        }

        /// <summary>
        /// Offsets a block's location based on the chunk position
        /// </summary>
        /// <param name="block">The location of a block in any axis</param>
        /// <param name="chunk">The location of the chunk in any axis</param>
        /// <returns></returns>
        public static int GetBlockXZOffsetByChunk(int block, int chunk)
        {
            return (chunk * ChunkWidth) + block;
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
                chunkLocation.X * ChunkWidth,
                0,
                chunkLocation.Z * ChunkWidth);
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
                x + BlockScaleSide,
                y + BlockScaleSide,
                z + BlockScaleSide);
        }

        /// <summary>
        /// Gets the transformation matrix world coordinates of a block based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3 WTMGetBlock(BlockLocation location)
        {
            return new Vector3(
                location.X + BlockScaleSide,
                location.Y + BlockScaleSide,
                location.Z + BlockScaleSide);
        }

        /// <summary>
        /// Gets the transformation matrix world coordinates of a chunk based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3 WTMGetChunk(int x, int z)
        {
            return new Vector3(
                (x * ChunkWidth) + (ChunkWidth / 2),
                ChunkScale.Y,
                (z * ChunkWidth) + (ChunkWidth / 2));
        }

        /// <summary>
        /// Gets the transformation matrix world coordinates of a chunk based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3 WTMGetChunk(ChunkLocation location)
        {
            return WTMGetChunk(location.X, location.Z);
        }

        public static Vector3 WTMGetWorldBlock(ChunkLocation chunk, BlockLocation location)
        {
            return WTMGetChunk(chunk) + WTMGetBlock(location) - ChunkScale;
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
            return new BlockLocation(
                pos.X > 0 ? MTWGetPositiveNumber(pos.X) : (MTWGetNegativeNumber(pos.X) - BlockWidth),
                pos.Y > 0 ? MTWGetPositiveNumber(pos.Y) : (MTWGetNegativeNumber(pos.Y) - BlockWidth),
                pos.Z > 0 ? MTWGetPositiveNumber(pos.Z) : (MTWGetNegativeNumber(pos.Z) - BlockWidth));
        }

        /// <summary>
        /// Gets the world coordinates of a chunk from the Matrix location
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static ChunkLocation MTWGetChunk(Vector3 pos)
        {
            return new ChunkLocation(
                pos.X > 0 ? MTWGetChunkPositive(pos.X) : MTWGetChunkNegative(pos.X),
                pos.Z > 0 ? MTWGetChunkPositive(pos.Z) : MTWGetChunkNegative(pos.Z));
        }

        public static int MTWGetChunkPositive(float a)
        {
            return MTWGetPositiveNumber(a) / ChunkWidth;
        }

        // floor and ceil work weirdly with negative numbers. idk if 
        // i should have to do '- ChunkWidth'... cant be botherd to fix it tho :))))
        public static int MTWGetChunkNegative(float a)
        {
            return (MTWGetNegativeNumber(a) - ChunkWidth) / ChunkWidth;
        }

        public static int MTWGetPositiveNumber(float a)
        {
            return (int)MathF.Floor(a);
        }

        public static int MTWGetNegativeNumber(float a)
        {
            return (int)MathF.Ceiling(a);
        }
    }
}
