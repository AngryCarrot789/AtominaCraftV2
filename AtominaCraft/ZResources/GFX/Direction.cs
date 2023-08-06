using AtominaCraft.Utils;

namespace AtominaCraft.ZResources.GFX {
    public class Direction {
        public static readonly Direction DOWN;
        public static readonly Direction UP;
        public static readonly Direction NORTH;
        public static readonly Direction SOUTH;
        public static readonly Direction WEST;
        public static readonly Direction EAST;
        public static readonly Direction UNKNOWN;

        public static readonly Direction[] Directions;
        public static readonly Direction[] ValidDirections;
        public static readonly Direction[] Opposites;
        public static readonly Direction[] ValidOpposites;

        public readonly int Index;
        public readonly int x;
        public readonly int y;
        public readonly int z;
        public readonly BlockChunkCoord ChunkCoord;
        public readonly BlockWorldCoord WorldCoord;

        public Direction Opposite { get; private set; }

        static Direction() {
            DOWN = new Direction(0, 0, -1, 0);
            UP = new Direction(1, 0, 1, 0);
            NORTH = new Direction(2, 0, 0, -1);
            SOUTH = new Direction(3, 0, 0, 1);
            WEST = new Direction(4, -1, 0, 0);
            EAST = new Direction(5, 1, 0, 0);
            UNKNOWN = new Direction(6, 0, 0, 0);
            Directions = new Direction[] {DOWN, UP, NORTH, SOUTH, WEST, EAST, UNKNOWN};
            ValidDirections = new Direction[] {DOWN, UP, NORTH, SOUTH, WEST, EAST};
            Opposites = new Direction[] {UP, DOWN, SOUTH, NORTH, EAST, WEST, UNKNOWN};
            ValidOpposites = new Direction[] {UP, DOWN, SOUTH, NORTH, EAST, WEST};

            DOWN.Opposite = UP;
            UP.Opposite = DOWN;
            NORTH.Opposite = SOUTH;
            SOUTH.Opposite = NORTH;
            WEST.Opposite = EAST;
            EAST.Opposite = WEST;
            UNKNOWN.Opposite = UNKNOWN;
        }

        private Direction(int index, int x, int y, int z) {
            this.Index = index;
            this.x = x;
            this.y = y;
            this.z = z;
            this.ChunkCoord = new BlockChunkCoord(x, y, z);
            this.WorldCoord = new BlockWorldCoord(x, y, z);
        }
    }
}