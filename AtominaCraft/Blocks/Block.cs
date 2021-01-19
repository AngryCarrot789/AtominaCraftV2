using AtominaCraft.BlockGrid;
using AtominaCraft.Collision;
using AtominaCraft.ZResources.Maths;

namespace AtominaCraft.Blocks
{
    public class Block
    {
        public int ID { get; set; }
        public float Hardness { get; set; }
        public float Resistance { get; set; }

        public AxisAlignedBB BoundingBox { get; set; }
        public BlockLocation Location { get; set; }
    }
}
