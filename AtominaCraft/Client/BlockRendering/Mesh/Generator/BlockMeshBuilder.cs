using System.Collections.Generic;
using REghZy.MathsF;

namespace AtominaCraft.Client.BlockRendering.Mesh.Generator {
    public struct BlockMeshBuilder {
        public BlockFace Top;
        public BlockFace Front;
        public BlockFace Left;
        public BlockFace Bottom;
        public BlockFace Right;
        public BlockFace Back;

        public bool VisibleTop => this.Top.IsGenerated;
        public bool VisibleFront => this.Front.IsGenerated;
        public bool VisibleLeft => this.Left.IsGenerated;
        public bool VisibleBottom => this.Bottom.IsGenerated;
        public bool VisibleRight => this.Right.IsGenerated;
        public bool VisibleBack => this.Back.IsGenerated;

        public void WriteVertices(List<float> vertices) {
            if (this.VisibleTop)
                this.Top.WriteVertices(vertices);
            if (this.VisibleFront)
                this.Front.WriteVertices(vertices);
            if (this.VisibleLeft)
                this.Left.WriteVertices(vertices);
            if (this.VisibleBottom)
                this.Bottom.WriteVertices(vertices);
            if (this.VisibleRight)
                this.Right.WriteVertices(vertices);
            if (this.VisibleBack)
                this.Back.WriteVertices(vertices);
        }

        public void WriteUVs(List<float> uvs) {
            if (this.VisibleTop)
                this.Top.WriteTextureCoordinates(uvs);
            if (this.VisibleFront)
                this.Front.WriteTextureCoordinates(uvs);
            if (this.VisibleLeft)
                this.Left.WriteTextureCoordinates(uvs);
            if (this.VisibleBottom)
                this.Bottom.WriteTextureCoordinates(uvs);
            if (this.VisibleRight)
                this.Right.WriteTextureCoordinates(uvs);
            if (this.VisibleBack)
                this.Back.WriteTextureCoordinates(uvs);
        }

        public void WriteNormals(List<float> normals, List<float> vertices) {
            for (int i = 0; i < vertices.Count; i += 9) {
                Vector3f v1 = new Vector3f(vertices[i + 0], vertices[i + 1], vertices[i + 2]);
                Vector3f v2 = new Vector3f(vertices[i + 3], vertices[i + 4], vertices[i + 5]);
                Vector3f v3 = new Vector3f(vertices[i + 6], vertices[i + 7], vertices[i + 8]);
                Vector3f normal = (v2 - v1).Cross(v3 - v1).Normalise();
                normals.Add(normal.x);
                normals.Add(normal.y);
                normals.Add(normal.z);
                normals.Add(normal.x);
                normals.Add(normal.y);
                normals.Add(normal.z);
                normals.Add(normal.x);
                normals.Add(normal.y);
                normals.Add(normal.z);
            }
        }

        public static BlockMeshBuilder GenerateBlockMesh(bool top, bool front, bool left, bool bottom, bool right, bool back) {
            BlockMeshBuilder mesh = new BlockMeshBuilder();
            if (top)
                mesh.Top = BlockFace.GenerateFace(FaceDirection.Top);
            if (front)
                mesh.Front = BlockFace.GenerateFace(FaceDirection.Front);
            if (left)
                mesh.Left = BlockFace.GenerateFace(FaceDirection.Left);
            if (bottom)
                mesh.Bottom = BlockFace.GenerateFace(FaceDirection.Bottom);
            if (right)
                mesh.Right = BlockFace.GenerateFace(FaceDirection.Right);
            if (back)
                mesh.Back = BlockFace.GenerateFace(FaceDirection.Back);
            return mesh;
        }
    }
}