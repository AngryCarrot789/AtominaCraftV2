using AtominaCraft.Collision;
using AtominaCraft.Resources.Maths;

namespace AtominaCraft.Blocks
{
    public class Block
    {
        public int ID { get; set; }
        public float Hardness { get; set; }
        public float Resistance { get; set; }

        public AxisAlignedBB BoundingBox { get; set; }

        public Vector3 Location { get; set; }
    }
}
