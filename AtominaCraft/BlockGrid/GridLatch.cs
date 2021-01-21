using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.BlockGrid
{
    /// <summary>
    /// A class used for getting world locations of blocks based on their grid-related location
    /// </summary>
    public static class GridLatch
    {
        public const float BLOCK_SCALE = 0.5f;
        public const float BLOCK_SIZE = BLOCK_SCALE * 2;

        // Stops having to create 100000s of vectors per second
        private static Vector3 TempV { get; set; }
        private static BlockLocation TempB { get; set; }

        static GridLatch()
        {
            TempV = new Vector3(0, 0, 0);
            TempB = new BlockLocation(0, 0, 0);
        }

        public static Vector3 GetBlockWorldSpace(BlockLocation location)
        {
            TempV.Set(
                location.X + BLOCK_SCALE, 
                location.Y + BLOCK_SCALE, 
                location.Z + BLOCK_SCALE);
            return TempV;
        }

        public static BlockLocation GetBlockGridSpace(Vector3 pos)
        {
            TempB.Set(
                (int)Math.Floor(pos.X),
                (int)Math.Floor(pos.Y),
                (int)Math.Floor(pos.Z));
            return TempB;
        }
    }
}
