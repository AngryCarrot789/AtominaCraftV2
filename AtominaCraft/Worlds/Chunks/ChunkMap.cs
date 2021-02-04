using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Chunks
{
    public class ChunkMap
    {
        private List<Chunk> Chunks { get; set; }

        // Used to offset indexing chunks 
        private int OffsetX { get; set; }
        private int OffsetZ { get; set; }

        private int ChunkCount => Chunks.Count;

        public ChunkMap()
        {
            Chunks = new List<Chunk>();
        }

        /// <summary>
        /// Sets the relative center of the chunkmap offset
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        public void SetRelativeCenter(int x, int z)
        {

        }

        public bool IsChunkLoaded(int x, int z)
        {
            return ((x + 1) * (z + 1)) > ChunkCount; 
        }
    }
}
