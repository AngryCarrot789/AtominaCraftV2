using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Client.BlockRendering.Mesh.Generator
{
    public struct BlockMeshBuilder
    {
        public BlockFace
            Top,
            Front,
            Left,
            Bottom,
            Right,
            Back;

        public bool VisibleTop => Top.IsGenerated;
        public bool VisibleFront => Front.IsGenerated;
        public bool VisibleLeft => Left.IsGenerated;
        public bool VisibleBottom => Bottom.IsGenerated;
        public bool VisibleRight => Right.IsGenerated;
        public bool VisibleBack => Back.IsGenerated;

        public void WriteVertices(List<float> vertices)
        {
            if (VisibleTop) Top.WriteVertices(vertices);
            if (VisibleFront) Front.WriteVertices(vertices);
            if (VisibleLeft) Left.WriteVertices(vertices);
            if (VisibleBottom) Bottom.WriteVertices(vertices);
            if (VisibleRight) Right.WriteVertices(vertices);
            if (VisibleBack) Back.WriteVertices(vertices);
        }

        public void WriteUVs(List<float> uvs)
        {
            if (VisibleTop) Top.WriteTextureCoordinates(uvs);
            if (VisibleFront) Front.WriteTextureCoordinates(uvs);
            if (VisibleLeft) Left.WriteTextureCoordinates(uvs);
            if (VisibleBottom) Bottom.WriteTextureCoordinates(uvs);
            if (VisibleRight) Right.WriteTextureCoordinates(uvs);
            if (VisibleBack) Back.WriteTextureCoordinates(uvs);
        }

        public void WriteNormals(List<float> normals, List<float> vertices)
        {
            for (int i = 0; i < vertices.Count; i += 9)
            {
                Vector3 vertex1 = new Vector3(vertices[i + 0], vertices[i + 1], vertices[i + 2]);
                Vector3 vertex2 = new Vector3(vertices[i + 3], vertices[i + 4], vertices[i + 5]);
                Vector3 vertex3 = new Vector3(vertices[i + 6], vertices[i + 7], vertices[i + 8]);
                Vector3 normal = (vertex2 - vertex1).Cross(vertex3 - vertex1).Normalised();
                normals.Add(normal.X);
                normals.Add(normal.Y);
                normals.Add(normal.Z);
                normals.Add(normal.X);
                normals.Add(normal.Y);
                normals.Add(normal.Z);
                normals.Add(normal.X);
                normals.Add(normal.Y);
                normals.Add(normal.Z);
            }
        }

        public static BlockMeshBuilder GenerateBlockMesh(bool top, bool front, bool left, bool bottom, bool right, bool back)
        {
            BlockMeshBuilder mesh = new BlockMeshBuilder();
            if (top) mesh.Top = BlockFace.GenerateFace(FaceDirection.Top);
            if (front) mesh.Front = BlockFace.GenerateFace(FaceDirection.Front);
            if (left) mesh.Left = BlockFace.GenerateFace(FaceDirection.Left);
            if (bottom) mesh.Bottom = BlockFace.GenerateFace(FaceDirection.Bottom);
            if (right) mesh.Right = BlockFace.GenerateFace(FaceDirection.Right);
            if (back) mesh.Back = BlockFace.GenerateFace(FaceDirection.Back);
            return mesh;
        }
    }
}
