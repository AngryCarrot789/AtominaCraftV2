using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AtominaCraft.Worlds.Chunks
{
    public class ChunkLocation
    {
        public int X { get; set; }
        public int Z { get; set; }

        public ChunkLocation(int x, int z)
        {
            X = x;
            Z = z;
        }

        public override int GetHashCode()
        {
            return X + Z;
        }
    }
}
