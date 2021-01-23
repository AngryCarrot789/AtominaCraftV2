using AtominaCraft.Worlds.Chunks;
using AtominaCraft.Worlds.Chunks.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Rendering
{
    public class WorldMesh
    {
        public Dictionary<Chunk, ChunkMesh> ChunkMeshes { get; set; }

        public WorldMesh()
        {
            ChunkMeshes = new Dictionary<Chunk, ChunkMesh>();
        }
    }
}
