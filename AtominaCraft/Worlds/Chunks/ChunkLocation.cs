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

        public Vector3 GetWorldSpaceCenter()
        {
            float expandX = (X << 4) + (Chunk.Width / 2);
            float expandZ = (Z << 4) + (Chunk.Width / 2);
            return new Vector3(expandX, Chunk.Height / 2, expandZ);
        }

        public override int GetHashCode()
        {
            return X + Z;
        }
    }
}
