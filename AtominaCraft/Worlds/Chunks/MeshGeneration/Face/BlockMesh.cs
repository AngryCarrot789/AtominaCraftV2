using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Worlds.Chunks.MeshGeneration.Face
{
    public struct BlockMesh
    {
        public BlockFace Top, Front, Left, Bottom, Right, Back;

        public void WriteVertices(List<float> vertices)
        {
            if (Top    != null) Top.WriteVertices(vertices);
            if (Front  != null) Front.WriteVertices(vertices);
            if (Left   != null) Left.WriteVertices(vertices);
            if (Bottom != null) Bottom.WriteVertices(vertices);
            if (Right  != null) Right.WriteVertices(vertices);
            if (Back   != null) Back.WriteVertices(vertices);
        }

        public void WriteUVs(List<float> uvs)
        {
            if (Top    != null) Top.WriteTextureCoordinates(uvs);
            if (Front  != null) Front.WriteTextureCoordinates(uvs);
            if (Left   != null) Left.WriteTextureCoordinates(uvs);
            if (Bottom != null) Bottom.WriteTextureCoordinates(uvs);
            if (Right  != null) Right.WriteTextureCoordinates(uvs);
            if (Back   != null) Back.WriteTextureCoordinates(uvs);
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

        public static BlockMesh GenerateBlockMesh(bool top, bool front, bool left, bool bottom, bool right, bool back)
        {
            BlockMesh mesh = new BlockMesh();
            if (top)    mesh.Top    = BlockFace.GenerateFace(FaceDirection.Top);
            if (front)  mesh.Front  = BlockFace.GenerateFace(FaceDirection.Front);
            if (left)   mesh.Left   = BlockFace.GenerateFace(FaceDirection.Left);
            if (bottom) mesh.Bottom = BlockFace.GenerateFace(FaceDirection.Bottom);
            if (right)  mesh.Right  = BlockFace.GenerateFace(FaceDirection.Right);
            if (back)   mesh.Back   = BlockFace.GenerateFace(FaceDirection.Back);
            return mesh;
        }
    }
}
