using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Client.BlockRendering.Mesh
{
    public struct Vertex3
    {
        public float X, Y, Z;
        public Vertex3(float x, float y, float z)
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
