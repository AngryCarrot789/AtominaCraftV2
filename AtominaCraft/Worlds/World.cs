using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Entities.Player;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using System.Collections.Generic;

namespace AtominaCraft.Worlds
{
    public class World
    {
        public EntityPlayerCamera MainPlayer { get; set; }

        public List<Chunk> Chunks { get; set; }

        public World()
        {
            Chunks = new List<Chunk>();
            Chunk c = new Chunk(this, new Vector2(1, 1));
            c.Blocks.Add(new Block() { Location = new BlockLocation(0, 0, 0) });
            c.Blocks.Add(new Block() { Location = new BlockLocation(0, 2, 0) });
            c.Blocks.Add(new Block() { Location = new BlockLocation(0, 4, 0) });
            c.Blocks.Add(new Block() { Location = new BlockLocation(1, 0, 0) });
            c.Blocks.Add(new Block() { Location = new BlockLocation(2, 0, 0) });
            Chunks.Add(c);
        }

        public void Update()
        {
            MainPlayer.Update();
            foreach(Chunk chunk in Chunks)
            {
                chunk.Update();
            }
        }
    }
}
