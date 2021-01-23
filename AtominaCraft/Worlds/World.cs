using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Entities.Player;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.Worlds.Weather;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using System.Collections.Generic;

namespace AtominaCraft.Worlds
{
    public class World
    {
        //public Sky Sky { get; set; }
        public EntityPlayerCamera MainPlayer { get; set; }

        public Dictionary<ChunkLocation, Chunk> Chunks { get; set; }

        public World()
        {
            //Sky = new Sky();
            Chunks = new Dictionary<ChunkLocation, Chunk>();
        }

        public void Update()
        {
            MainPlayer.Update();
            foreach(Chunk chunk in Chunks.Values)
            {
                chunk.Update();
            }
        }

        public Chunk GetChunkAt(int x, int z)
        {
            return GetChunkAt(new ChunkLocation(x, z));
        }

        public Chunk GetChunkAt(ChunkLocation location)
        {
            Chunks.TryGetValue(location, out Chunk chunk);
            return chunk;
        }

        public void SetMainPlayer(EntityPlayerCamera player)
        {
            MainPlayer = player;
            player.World = this;
        }
    }
}
