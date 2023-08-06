using System;
using AtominaCraft.BlockGrid;
using AtominaCraft.Utils;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;
using REghZy.MathsF;

namespace AtominaCraft.Blocks {
    public class Location {
        public World World;
        public int x;
        public int y;
        public int z;

        public Location(World world, int x, int y, int z) {
            this.World = world;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Location(Chunk chunk, int x, int y, int z) {
            this.World = chunk.world;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Location(int x, int y, int z) {
            this.World = null;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(Vector3f vector) {
            this.x = (int) Math.Floor(vector.x);
            this.y = (int) Math.Floor(vector.y);
            this.z = (int) Math.Floor(vector.z);
        }

        public void Set(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(World world, int x, int y, int z) {
            this.World = world;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Extract(ref Vector3f vector) {
            vector.x = this.x;
            vector.y = this.y;
            vector.z = this.z;
        }

        public Location Translated(int x, int y, int z) {
            return new Location(this.World, this.x + x, this.y + y, this.z + z);
        }

        public Location Translated(Location location) {
            return new Location(this.World, this.x + location.x, this.y + location.y, this.z + location.z);
        }

        public Location GetWorldLocation(ChunkLocation chunk) {
            return new Location(chunk.x * GridLatch.ChunkWidth + this.x, this.y, chunk.z * GridLatch.ChunkWidth + this.z);
        }

        public override bool Equals(object obj) {
            Location location = (Location) obj;
            return location.World.Equals(this.World) &&
                   location.x.Equals(this.x) &&
                   location.y.Equals(this.y) &&
                   location.z.Equals(this.z);
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.World.GetHashCode(), HashCode.Combine(this.x.GetHashCode(), this.y.GetHashCode(), this.z.GetHashCode()));
        }
    }
}