using AtominaCraft.Entities.Player;
using AtominaCraft.Utils;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.Worlds.Weather;

namespace AtominaCraft.Worlds {
    public class World {
        public readonly LongKeyDictionary<Chunk> chunks;

        public EntityPlayerCamera MainPlayer;

        public readonly string Name;
        public Sky Sky;

        private int lastChunkX;
        private int lastChunkZ;
        private Chunk lastChunk;

        public World() {
            this.Sky = new Sky();
            this.Name = "world";
            this.chunks = new LongKeyDictionary<Chunk>();
        }

        public void Update() {
            this.MainPlayer.Update();
            foreach (LKDEntry<Chunk> entry in this.chunks) {
                entry.value.Update();
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

        public Chunk GenerateChunk(int x, int z) {
            return new Chunk(this, new ChunkLocation(x, z));
        }

        public Chunk GetChunkAt(int x, int z) {
            Chunk chunk;
            if (x == this.lastChunkX && z == this.lastChunkZ) {
                chunk = this.lastChunk;
                if (chunk != null) {
                    return chunk;
                }
            }

            this.lastChunkX = x;
            this.lastChunkZ = z;
            if (this.chunks.TryGet(LongHash.ToLong(x, z), out chunk)) {
                this.lastChunk = chunk;
                return chunk;
            }
            else {
                this.chunks.Put(LongHash.ToLong(x, z), this.lastChunk = GenerateChunk(x, z));
                return this.lastChunk;
            }
        }

        public Chunk GetChunkAt(in ChunkLocation location) {
            Chunk chunk;
            if (location.x == this.lastChunkX && location.z == this.lastChunkZ) {
                chunk = this.lastChunk;
                if (chunk != null) {
                    return chunk;
                }
            }

            this.lastChunkX = location.x;
            this.lastChunkZ = location.z;
            if (this.chunks.TryGet(location.GetLongHash(), out chunk)) {
                this.lastChunk = chunk;
                return chunk;
            }
            else {
                this.lastChunk = new Chunk(this, location);
                return this.chunks[location.GetLongHash()] = this.lastChunk;
            }
        }

        public void SetMainPlayer(EntityPlayerCamera player) {
            this.MainPlayer = player;
            player.World = this;
        }

        public override bool Equals(object obj) {
            return obj == this || obj is World world && world.Name == this.Name;
        }

        public override int GetHashCode() {
            return this.Name == null ? 0 : this.Name.GetHashCode();
        }

        public int GetBlockIdAt(int x, int y, int z) {
            Chunk chunk;
            int cX = x >> 4;
            int cZ = z >> 4;
            if (cX == this.lastChunkX && cZ == this.lastChunkZ) {
                chunk = this.lastChunk;
                if (chunk == null) {
                    return 0;
                }

                return chunk.GetBlockAt(x & 15, y, z & 15);
            }

            this.lastChunkX = cX;
            this.lastChunkZ = cZ;
            if (this.chunks.TryGet(LongHash.ToLong(cX, cZ), out chunk)) {
                this.lastChunk = chunk;
                return chunk.GetBlockAt(x & 15, y & 255, z & 15);
            }

            return 0;
        }

        public int GetBlockIdAt(in BlockWorldCoord coord) {
            return GetBlockIdAt(coord.x, coord.y, coord.z);
        }

        public bool IsChunkLoaded(int cX, int cZ) {
            if (cX == this.lastChunkX && cZ == this.lastChunkZ) {
                return this.lastChunk != null;
            }

            return this.chunks.ContainsKey(LongHash.ToLong(cX, cZ));
        }

        public bool IsChunkLoaded(in ChunkLocation location) {
            if (location.x == this.lastChunkX && location.z == this.lastChunkZ) {
                return this.lastChunk != null;
            }

            return this.chunks.ContainsKey(location.GetLongHash());
        }

        public bool TryGetChunk(in ChunkLocation location, out Chunk chunk) {
            if (location.x == this.lastChunkX && location.z == this.lastChunkZ) {
                if (this.lastChunk != null) {
                    chunk = this.lastChunk;
                    return true;
                }
            }

            this.lastChunkX = location.x;
            this.lastChunkZ = location.z;
            if (this.chunks.TryGet(location.GetLongHash(), out chunk)) {
                this.lastChunk = chunk;
                return true;
            }

            return false;
        }
    }
}