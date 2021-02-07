using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Collision;
using AtominaCraft.Entities.Player;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.Worlds.Weather;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using System.Collections;
using System.Collections.Generic;

namespace AtominaCraft.Worlds
{
    public class World
    {
        public Sky Sky { get; set; }

        public string Name { get; set; }

        public EntityPlayerCamera MainPlayer { get; set; }

        public Dictionary<ChunkLocation, Chunk> Chunks { get; set; }

        public World()
        {
            Sky = new Sky();
            Name = "world";
            Chunks = new Dictionary<ChunkLocation, Chunk>();
        }

        public void Update()
        {
            MainPlayer.Update();
            foreach(Chunk chunk in Chunks.Values)
            {
                chunk.Update();
            }

            // Calculate collisions

            //if (MainPlayer.Chunk == null)
            //    return;
            //
            //foreach(Block block in MainPlayer.Chunk.Blocks.Values)
            //{
            //    AxisAlignedBB blockAabb = block.BoundingBox;
            //    AxisAlignedBB playerAabb = MainPlayer.BoundingBox;
            //
            //    if (playerAabb.IntersectsAABB(blockAabb))
            //    {
            //        Vector3 intersectAmount = new Vector3()
            //        {
            //            X = playerAabb.IntersectionAmountX(blockAabb, false), // ye
            //            Y = playerAabb.IntersectionAmountY(blockAabb, false), // ye
            //            Z = playerAabb.IntersectionAmountZ(blockAabb, false), // ye
            //        };
            //
            //        Vector3 difference = MainPlayer.Position - MainPlayer.PreviousPosition;
            //
            //        MainPlayer.MoveTowards(-difference);
            //    }
            //}
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

        public override bool Equals(object obj)
        {
            World world = (World)obj;
            return world.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
