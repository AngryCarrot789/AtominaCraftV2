using AtominaCraft.Blocks;
using AtominaCraft.Worlds;
using AtominaCraft.ZResources.GFX;
using REghZy.MathsF;

namespace AtominaCraft.Utils {
    /// <summary>
    /// A coordinate of a block within a world
    /// </summary>
    public readonly struct BlockWorldCoord {
        public readonly int x;
        public readonly int y;
        public readonly int z;

        /// <summary>
        /// Returns the chunk location of this block world coordinate
        /// </summary>
        public ChunkLocation ChunkLocation => new ChunkLocation(this.x >> 4, this.z >> 4);

        /// <summary>
        /// Returns a block chunk coordinate from this block world coordinate
        /// </summary>
        public BlockChunkCoord BlockChunkCoord => new BlockChunkCoord(this.x & 15, this.y, this.z & 15);

        public BlockWorldCoord(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public BlockWorldCoord Translate(Direction direction) {
            return this + direction.WorldCoord;
        }

        public static BlockWorldCoord operator +(in BlockWorldCoord a, in BlockWorldCoord b) {
            return new BlockWorldCoord(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static BlockWorldCoord operator +(in BlockWorldCoord a, Direction direction) {
            return a + direction.WorldCoord;
        }

        public static BlockWorldCoord operator -(in BlockWorldCoord a, in BlockWorldCoord b) {
            return new BlockWorldCoord(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static BlockWorldCoord operator -(in BlockWorldCoord a, Direction direction) {
            return a - direction.WorldCoord;
        }

        public static BlockWorldCoord operator *(in BlockWorldCoord a, in BlockWorldCoord b) {
            return new BlockWorldCoord(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static BlockWorldCoord operator /(in BlockWorldCoord a, in BlockWorldCoord b) {
            return new BlockWorldCoord(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public Block GetBlock(World world) {
            return Block.Blocks[world.GetBlockIdAt(this.x, this.y, this.z)];
        }

        public int GetBlockId(World world) {
            return world.GetBlockIdAt(this.x, this.y, this.z);
        }

        /// <summary>
        /// Whether the object is equal to this block coordinate
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            return obj is BlockWorldCoord coord && (coord.x == this.x && coord.y == this.y && coord.z == this.z);
        }

        public override string ToString() {
            return $"BlockWorldCoord({this.x}, {this.y}, {this.z})";
        }

        public Vector3f ToRender() {
            return new Vector3f(this.x, this.y, this.z);
        }
    }
}