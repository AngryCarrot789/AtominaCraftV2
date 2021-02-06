using System.Collections.Generic;

namespace AtominaCraft.Worlds.Chunks.MeshGeneration.Face
{
    public struct Vertex3f
    {
        public float X, Y, Z;
        public Vertex3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void WriteVertices(List<float> vertices)
        {
            vertices.Add(X);
            vertices.Add(Y);
            vertices.Add(Z);
        }
    }
}
