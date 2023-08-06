using System.Collections.Generic;

namespace AtominaCraft.Client.BlockRendering.Mesh {
    public struct Vertex3 {
        public float X, Y, Z;

        public Vertex3(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public void WriteVertices(List<float> vertices) {
            vertices.Add(this.X);
            vertices.Add(this.Y);
            vertices.Add(this.Z);
        }
    }
}