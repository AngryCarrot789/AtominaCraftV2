using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.BlockGrid
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
            Set(x, y, z);
        }

        public BlockLocation(int x, int y, int z)
        {
            Set(x, y, z);
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
