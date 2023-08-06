using System;
using AtominaCraft.Blocks;
using AtominaCraft.Utils;
using REghZy.MathsF;

namespace AtominaCraft.BlockGrid {
    /// <summary>
    ///     A class used for calculating 'matrix locations' (aka, locations used by transformation matrixes)
    ///     based on grid-latched 'world location', and vise versa, e.g. getting the world coordinates of a block, chunk
    ///     locations, etc
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
    public static class GridLatch {
        /// <summary>
        ///     The "Matrix" scale of a block, in theory the legnth starting at the center going to the edges
        ///     <para>
        ///         DONT EDIT THIS OTHERWISE THE ENTIRE WROLD WILL BREAK XDXDXDXD
        ///     </para>
        /// </summary>
        public const float BlockScaleSide = 0.5f;

        /// <summary>
        ///     The total "length/size" of a block, in theory starting at the corners not the center
        ///     <para>
        ///         This CANNOT be a decimal number. it must be an integral (aka "integer based") number because it does
        ///     </para>
        /// </summary>
        public const int BlockWidth = 1;

        //public const int ChunkHeight = 256;
        //public const int ChunkWidth = 16;
        //public const int ChunkIndexableHeight = 255;
        //public const int ChunkIndexableWidth = 15;

        // These are editable values btw. If the math breaks when changing these... something isnt right
        public const int ChunkHeight = 128;
        public const int ChunkWidth = 16;

        public static Vector3f BlockScale = new Vector3f(BlockScaleSide, BlockScaleSide, BlockScaleSide);
        public static Vector3f ChunkScale = new Vector3f(ChunkWidth / 2.0f, ChunkHeight / 2.0f, ChunkWidth / 2.0f);


        static GridLatch() {

        }

        /// <summary>
        ///     Offsets a block's location based on the chunk position
        /// </summary>
        /// <param name="block">The location of a block in any axis</param>
        /// <param name="chunk">The location of the chunk in any axis</param>
        /// <returns></returns>
        public static int GetBlockXZOffsetByChunk(int block, int chunk) {
            return chunk * ChunkWidth + block;
        }

        // /// <summary>
        // ///     Returns a copy of the given block location offset by the chunk location
        // /// </summary>
        // /// <param name="block"></param>
        // /// <param name="chunk"></param>
        // /// <returns></returns>
        // public static Location GetBlockOffsetByChunk(Location block, ChunkLocation chunk) {
        //     TempB.Set(
        //         GetBlockXZOffsetByChunk(block.x, chunk.x),
        //         block.y,
        //         GetBlockXZOffsetByChunk(block.z, chunk.z));
        //     return TempB;
        // }
        // /// <summary>
        // ///     Returns a blank block location (starting at 0,0) offset by a given chunk location
        // /// </summary>
        // /// <param name="chunkLocation"></param>
        // /// <returns></returns>
        // public static Location GetBlankChunkOffset(ChunkLocation chunkLocation) {
        //     TempB.Set(
        //         chunkLocation.x * ChunkWidth,
        //         0,
        //         chunkLocation.z * ChunkWidth);
        //     return TempB;
        // }

        //
        //     OpenGL/Matrix stuff
        //
        //      
        //          World to matrix
        //
        //
        //

        /// <summary>
        ///     Gets the transformation matrix world coordinates of a block based on its location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3f WTMGetBlock(int x, int y, int z) {
            return new Vector3f(x + BlockScaleSide, y + BlockScaleSide, z + BlockScaleSide);
        }

        /// <summary>
        ///     Gets the transformation matrix world coordinates of a block based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3f WTMGetBlock(in BlockChunkCoord pos) {
            return new Vector3f(pos.x + BlockScaleSide, pos.y + BlockScaleSide, pos.z + BlockScaleSide);
        }

        /// <summary>
        ///     Gets the transformation matrix world coordinates of a chunk based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3f WTMGetChunk(int x, int z) {
            return new Vector3f(
                x * ChunkWidth + ChunkWidth / 2,
                ChunkScale.y,
                z * ChunkWidth + ChunkWidth / 2);
        }

        /// <summary>
        ///     Gets the transformation matrix world coordinates of a chunk based on its location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Vector3f WTMGetChunk(in ChunkLocation location) {
            return WTMGetChunk(location.x, location.z);
        }

        public static Vector3f WTMGetWorldBlock(in ChunkLocation chunk, in BlockChunkCoord pos) {
            return WTMGetChunk(chunk) + WTMGetBlock(pos) - ChunkScale;
        }

        //
        //  Matrix to world
        //

        /// <summary>
        ///     Gets the world coordinates of a block from the Matrix location
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Location MTWGetBlock(in Vector3f pos) {
            return new Location(
                pos.x > 0 ? MTWGetPositiveNumber(pos.x) : MTWGetNegativeNumber(pos.x) - BlockWidth,
                pos.y > 0 ? MTWGetPositiveNumber(pos.y) : MTWGetNegativeNumber(pos.y) - BlockWidth,
                pos.z > 0 ? MTWGetPositiveNumber(pos.z) : MTWGetNegativeNumber(pos.z) - BlockWidth);
        }

        //public static BlockLocation MTWGetWorldBlock(World world, Vector3 pos)
        //{
        //    return new BlockLocation((int)pos.X & 15, (int)pos.Y, (int)pos.Z & 15);
        //}

        /// <summary>
        ///     Gets the world coordinates of a chunk from the Matrix location
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static ChunkLocation MTWGetChunk(in Vector3f pos) {
            return new ChunkLocation(
                pos.x > 0 ? MTWGetChunkPositive(pos.x) : MTWGetChunkNegative(pos.x),
                pos.z > 0 ? MTWGetChunkPositive(pos.z) : MTWGetChunkNegative(pos.z));
        }

        public static int MTWGetChunkPositive(float a) {
            return MTWGetPositiveNumber(a) / ChunkWidth;
        }

        // floor and ceil work weirdly with negative numbers. idk if 
        // i should have to do '- ChunkWidth'... cant be botherd to fix it tho :))))
        public static int MTWGetChunkNegative(float a) {
            return (MTWGetNegativeNumber(a) - ChunkWidth) / ChunkWidth;
        }

        public static int MTWGetPositiveNumber(float a) {
            return (int) MathF.Floor(a);
        }

        public static int MTWGetNegativeNumber(float a) {
            return (int) MathF.Ceiling(a);
        }
    }
}