using AtominaCraft.BlockGrid;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Maths;
using System;

namespace AtominaCraft.Blocks
{
    public class BlockLocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Chunk Chunk { get; set; }

        public BlockLocation(Chunk chunk, int x, int y, int z)
        {
            Chunk = chunk;
            X = x;
            Y = y;
            Z = z;
        }

        public BlockLocation(int x, int y, int z)
        {
            Chunk = null;
            X = x;
            Y = y;
            Z = z;
        }

        public void Set(Vector3 vector)
        {
            X = (int)Math.Floor(vector.X);
            Y = (int)Math.Floor(vector.Y);
            Z = (int)Math.Floor(vector.Z);
        }

        public void Set(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Set(Chunk chunk, int x, int y, int z)
        {
            Chunk = chunk;
            X = x;
            Y = y;
            Z = z;
        }

        public void Extract(Vector3 vector)
        {
            vector.X = X;
            vector.Y = Y;
            vector.Z = Z;
        }

        public BlockLocation Translated(int x, int y, int z)
        {
            return new BlockLocation(Chunk, X + x, Y + y, Z + z);
        }

        public BlockLocation Translated(BlockLocation location)
        {
            return new BlockLocation(Chunk, X + location.X, Y + location.Y, Z + location.Z);
        }

        public BlockLocation GetWorldLocation(ChunkLocation chunk)
        {
            return new BlockLocation((chunk.X * GridLatch.ChunkWidth) + X, Y, (chunk.Z * GridLatch.ChunkWidth) + Z);
        }

        public override bool Equals(object obj)
        {
            BlockLocation location = (BlockLocation)obj;
            return
                location.Chunk.Equals(Chunk) &&
                location.X.Equals(X) &&
                location.Y.Equals(Y) &&
                location.Z.Equals(Z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Chunk.GetHashCode(), HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode()));
        }
    }
}
