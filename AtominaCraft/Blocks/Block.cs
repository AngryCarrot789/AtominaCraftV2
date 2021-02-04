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
        public int ID { get; set; }
        public float Hardness { get; set; }
        public float Resistance { get; set; }
        public bool IsTransparent { get; set; }

        public AxisAlignedBB BoundingBox { get; set; }
        public BlockLocation Location { get; set; }
        public World World { get; set; }

        public Block(World world, BlockLocation location, int id = (int)TextureTypes.Dirt)
        {
            World = world;
            Location = location;
            ID = id;
            IsTransparent = ID == ((int)TextureTypes.Air) || ID == ((int)TextureTypes.Glass);
            BoundingBox = new AxisAlignedBB(Location);
        }

        public bool IsEmpty()
        {
            return ID == 0;
        }

        public override bool Equals(object obj)
        {
            Block block = (Block)obj;
            return block.World.Equals(World) && block.Location.Equals(Location) && block.ID.Equals(ID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID.GetHashCode(), Location.GetHashCode());
        }
    }
}
