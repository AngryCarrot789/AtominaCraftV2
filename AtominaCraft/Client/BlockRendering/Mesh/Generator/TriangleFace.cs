using System.Collections.Generic;

namespace AtominaCraft.Client.BlockRendering.Mesh.Generator {
    public struct TriangleFace {
        public Vertex3 V1;
        public Vertex3 V2;
        public Vertex3 V3;
        public Vertex2 VT1;
        public Vertex2 VT2;
        public Vertex2 VT3;

        public TriangleFace(Vertex3 v1, Vertex3 v2, Vertex3 v3, Vertex2 vt1, Vertex2 vt2, Vertex2 vt3) {
            this.V1 = v1;
            this.V2 = v2;
            this.V3 = v3;
            this.VT1 = vt1;
            this.VT2 = vt2;
            this.VT3 = vt3;
        }

        public void WriteVertices(List<float> vertices) {
            this.V1.WriteVertices(vertices);
            this.V2.WriteVertices(vertices);
            this.V3.WriteVertices(vertices);
        }

        public void WriteTextureCoordinates(List<float> uvs) {
            this.VT1.WriteVertices(uvs);
            this.VT2.WriteVertices(uvs);
            this.VT3.WriteVertices(uvs);
        }
    }
}