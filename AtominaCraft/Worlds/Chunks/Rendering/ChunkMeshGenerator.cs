using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Chunks.Rendering
{
    public static class ChunkMeshGenerator
    {
        public static Dictionary<Chunk, ChunkMesh> ChunkMeshes { get; set; }

        public static void Initialise()
        {
            ChunkMeshes = new Dictionary<Chunk, ChunkMesh>();
        }

        public static ChunkMesh GenerateChunk(Chunk chunk)
        {
            ChunkMesh mesh = new ChunkMesh(chunk);
            mesh.GenerateMeshData();
            ChunkMeshes.Add(chunk, mesh);
            return mesh;
        }

        public static ChunkMesh GetMesh(Chunk chunk)
        {
            if (ChunkMeshes.TryGetValue(chunk, out ChunkMesh mesh))
            {
                return mesh;
            }
            return null;
        }
    }
}
