using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Generator.Mesh
{
    public struct Vertex2
    {
        public float X, Y;
        public Vertex2(float x, float y)
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
