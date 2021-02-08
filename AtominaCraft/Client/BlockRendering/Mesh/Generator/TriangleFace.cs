using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Client.BlockRendering.Mesh.Generator
{
    public struct TriangleFace
    {
        public Vertex3 V1;
        public Vertex3 V2;
        public Vertex3 V3;
        public Vertex2 VT1;
        public Vertex2 VT2;
        public Vertex2 VT3;

        public TriangleFace(Vertex3 v1, Vertex3 v2, Vertex3 v3, Vertex2 vt1, Vertex2 vt2, Vertex2 vt3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
            VT1 = vt1;
            VT2 = vt2;
            VT3 = vt3;
        }

        public void WriteVertices(List<float> vertices)
        {
            V1.WriteVertices(vertices);
            V2.WriteVertices(vertices);
            V3.WriteVertices(vertices);
        }

        public void WriteTextureCoordinates(List<float> uvs)
        {
            VT1.WriteVertices(uvs);
            VT2.WriteVertices(uvs);
            VT3.WriteVertices(uvs);
        }
    }
}
