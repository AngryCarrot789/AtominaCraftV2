using AtominaCraft.Blocks;
using AtominaCraft.ZResources.Maths;
using System.Collections.Generic;

namespace AtominaCraft.Worlds.Chunks
{
    public class Chunk
    {
        public Vector2 Location { get; set; }

        public World World { get; set; }

        public List<Block> Blocks { get; set; }

        public Chunk(World world, Vector2 location)
        {
            Blocks = new List<Block>();
            Location = location;
            World = world;
        }

        public void Update()
        {

        }
    }
}
