using System.Collections.Generic;

namespace AtominaCraft.Client.BlockRendering.Mesh {
    public struct Vertex2 {
        public float X, Y;

        public Vertex2(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public void WriteVertices(List<float> uvs) {
            uvs.Add(this.X);
            uvs.Add(this.Y);
        }
    }
}