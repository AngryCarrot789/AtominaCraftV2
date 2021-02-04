using AtominaCraft.BlockGrid;
using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Maths;
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
            return HashGenerator.GenerateHashXZ(X, Z);
        }

        public override bool Equals(object obj)
        {
            ChunkLocation location = (ChunkLocation)obj;
            return 
                location.X.Equals(X) && 
                location.Z.Equals(Z);
        }
    }
}
