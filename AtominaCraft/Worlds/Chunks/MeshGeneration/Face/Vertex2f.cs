using System.Collections.Generic;

namespace AtominaCraft.Worlds.Chunks.MeshGeneration.Face
{
    public struct Vertex2f
    {
        public float X, Y;
        public Vertex2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void WriteVertices(List<float> uvs)
        {
            uvs.Add(X);
            uvs.Add(Y);
        }
    }
}
