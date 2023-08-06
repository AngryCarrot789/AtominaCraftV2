using AtominaCraft.Collision;
using AtominaCraft.Utils;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.GFX;

namespace AtominaCraft.Blocks {
    public class Block {
        public static Block[] Blocks = new Block[4096];

        public static Block FromID(int blockId) {
            return Blocks[blockId];
        }

        // Template blocks... these should never directly be used, they should be copied
        public static readonly Block Air = new Block(0, true);
        public static readonly Block Dirt = new Block(1);
        public static readonly Block Gold = new Block(2);
        public static readonly Block Snow = new Block(3);
        public static readonly Block Electromagnet = new Block(4);

        public AxisAlignedBB BoundingBox;

        public readonly int id;

        //public float Hardness;
        //public float Resistance;
        public bool isTransparent;

        private Block(int id = 0, bool isTransparent = false) {
            Blocks[id] = this;
            this.id = id;
            this.isTransparent = isTransparent;
            this.BoundingBox = new AxisAlignedBB();
        }

        public virtual bool IsObscured(World world, in BlockWorldCoord coord, Direction direction) {
            BlockWorldCoord otherCoord = coord + direction;
            Block other = otherCoord.GetBlock(world);
            return other != null && other.CanObscureBlock(world, otherCoord, coord);
        }

        /// <summary>
        /// Whether this block can actually render at these specific coordinates
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="chunkCoord"></param>
        /// <returns></returns>
        public virtual bool CanRender(Chunk chunk, in BlockChunkCoord chunkCoord) {
            return CanRender(chunk.world, chunkCoord.ToWorldCoord(chunk));
        }

        public virtual bool CanRender(World chunk, in BlockWorldCoord chunkCoord) {
            return true;
        }

        /// <summary>
        /// Whether this block obscures another block, therefore making that other block invisible on that specific side
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="coord">The location of this block</param>
        /// <param name="toObscure">The coordinate of the block that may or may not be obscured</param>
        /// <returns></returns>
        public virtual bool CanObscureBlock(World world, in BlockWorldCoord coord, in BlockWorldCoord toObscure) {
            if (this.isTransparent) {
                return false;
            }

            return true;
        }
    }
}