using AtominaCraft.BlockGrid;
using AtominaCraft.Collision;
using AtominaCraft.Worlds;
using System;

namespace AtominaCraft.Blocks
{
    public class Block
    {
        // Template blocks... these should never directly be used, they should be copied
        public static readonly Block Air           = new Block(0, true);
        public static readonly Block Dirt          = new Block(1, false);
        public static readonly Block Gold          = new Block(2, false);
        public static readonly Block Snow          = new Block(3, false);
        public static readonly Block Electromagnet = new Block(4, false);

        public int ID { get; set; }
        //public float Hardness { get; set; }
        //public float Resistance { get; set; }
        public bool IsTransparent { get; set; }
        public bool ShouldRender { get; set; }

        public long Metadata { get; set; }

        public AxisAlignedBB BoundingBox { get; set; }
        public BlockLocation Location { get; set; }
        public World World { get; set; }

        public Block(World world, BlockLocation location, int id = 1, bool isTransparent = false)
        {
            ShouldRender = true;
            World = world;
            Location = location;
            ID = id;
            //Hardness = hardness;
            //Resistance = resistance;
            IsTransparent = isTransparent;
            if (location.Chunk != null)
            {
                BlockLocation blockLocation = GridLatch.GetBlockOffsetByChunk(location, location.Chunk.Location);
                BoundingBox = new AxisAlignedBB(blockLocation);
            }
        }

        private Block(int id = 0, bool isTransparent = false)
        {
            ShouldRender = true;
            ID = id;
            IsTransparent = isTransparent;
            BoundingBox = new AxisAlignedBB();
        }

        /// <summary>
        /// Removes the block from the chunk it's in
        /// </summary>
        public void BreakNaturally()
        {
            if (HasLocation())
            {
                Location.Chunk.BreakBlockNaturally(Location);
            }
        }

        /// <summary>
        /// States if the location of this block isn't <see langword="null"/> and exists somewhere
        /// </summary>
        /// <returns></returns>
        public bool HasLocation()
        {
            return World != null && Location != null && Location.Chunk != null;
        }

        /// <summary>
        /// Returns true if the block ID is air
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return ID == 0;
        }

        /// <summary>
        /// Creates a copy of the block, copying all the block data over (AABB, id, etc), with the optional ability to copy metadate
        /// </summary>
        /// <param name="block"></param>
        /// <param name="world"></param>
        /// <param name="location"></param>
        /// <param name="copyMetadata"></param>
        /// <returns></returns>
        public static Block CreateCopy(Block block, World world, BlockLocation location, bool copyMetadata = true)
        {
            Block newBlock =
                new Block(
                    world,
                    location,
                    block.ID,
                    block.IsTransparent)
                {
                    BoundingBox = block.BoundingBox
                };
            if (copyMetadata)
                newBlock.Metadata = block.Metadata;
            return newBlock;
        }

        public override bool Equals(object obj)
        {
            Block block = (Block)obj;
            return block.World.Equals(World) && block.Location.Equals(Location) && block.ID.Equals(ID);
        }

        /// <summary>
        /// Used for dictionary indexing blocks in chunks
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID.GetHashCode(), Location.GetHashCode());
        }
    }
}
