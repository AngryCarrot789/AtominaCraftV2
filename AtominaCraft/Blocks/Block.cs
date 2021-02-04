using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks.Rendering;
using AtominaCraft.Collision;
using AtominaCraft.Worlds;
using AtominaCraft.ZResources.Maths;
using System;
using System.Runtime.CompilerServices;

namespace AtominaCraft.Blocks
{
    public class Block
    {
        //public static readonly Block Empty = new Block(null, new BlockLocation(0, 0, 0), 0);
        //public static readonly Block Gold = new Block(null, new BlockLocation(0, 0, 0), 3);
        //public static readonly Block Electromagnet = new Block(null, new BlockLocation(0, 0, 0), 6);
        //public static readonly Block Dirt = new Block(null, new BlockLocation(0, 0, 0), 4);

        public int ID { get; set; }
        public float Hardness { get; set; }
        public float Resistance { get; set; }
        public bool IsTransparent { get; set; }
        public bool ShouldRender { get; set; }

        public long Metadata { get; set; }

        public AxisAlignedBB BoundingBox { get; set; }
        public BlockLocation Location { get; set; }
        public World World { get; set; }

        public Block(World world, BlockLocation location, int id = (int)TextureTypes.Dirt, float hardness = 0, float resistance = 0, bool isTransparent = false)
        {
            ShouldRender = true;
            World = world;
            Location = location;
            ID = id;
            Hardness = hardness;
            Resistance = resistance;
            IsTransparent = isTransparent;
            if (location != null && location.Chunk != null)
            {
                BlockLocation blockLocation = GridLatch.GetBlockOffsetByChunk(location, location.Chunk.Location);
                BoundingBox = new AxisAlignedBB(blockLocation);
            }
        }

        public bool IsEmpty()
        {
            return ID == 0;
        }

        public static Block CreateCopy(Block block, bool copyMetadata)
        {
            Block newBlock =
                new Block(
                    block.World,
                    new BlockLocation(
                        block.Location.X,
                        block.Location.Y,
                        block.Location.Z),
                    block.ID,
                    block.Hardness,
                    block.Resistance,
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
