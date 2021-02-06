using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System;

namespace AtominaCraft.Worlds.Chunks.MeshGeneration
{
    public class ChunkMesh : IDisposable
    {
        /// <summary>
        /// The number of vertex buffer objects. Vertex, UV/Textures, and Normals
        /// </summary>
        public const int VBO_COUNT = 3;

        /// <summary>
        /// Stores an ID to the vertex arrays object (the object contains all the buffers)
        /// </summary>
        public int VAO { get; set; }

        /// <summary>
        /// Contains IDs to the vertex buffers within the VAO
        /// </summary>
        public int[] VBOs { get; set; }

        /// <summary>
        /// Stores all of the meshes vertices
        /// </summary>
        public List<float> Vertices { get; set; }
        /// <summary>
        /// Stores all of the meshes texture coordinates
        /// </summary>
        public List<float> UVs { get; set; }
        /// <summary>
        /// Stores all of the meshes normals
        /// </summary>
        public List<float> Normals { get; set; }

        /// <summary>
        /// Generates a mesh using the given vertices, texture coordinates and normals
        /// <param name="vertices"></param>
        /// <param name="uvs"></param>
        /// <param name="normals"></param>
        public ChunkMesh(List<float> vertices, List<float> uvs, List<float> normals)
        {
            Vertices = vertices;
            UVs = uvs;
            Normals = normals;
            VBOs = new int[VBO_COUNT];

            // Generate vertex array buffer
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // Generate vertex buffer
            VBOs[0] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * sizeof(float), Vertices.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate texture buffer
            VBOs[1] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[1]);
                GL.BufferData(BufferTarget.ArrayBuffer, UVs.Count * sizeof(float), UVs.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate normals buffer
            VBOs[2] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[2]);
                GL.BufferData(BufferTarget.ArrayBuffer, Normals.Count * sizeof(float), Normals.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        /// <summary>
        /// Binds the vertex arrays and draws them
        /// </summary>
        public void Draw()
        {
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Count);
        }

        /// <summary>
        /// Get rid of the vertex buffers and the vertex arrays object
        /// </summary>
        public void Dispose()
        {
            GL.DeleteBuffers(VBOs.Length, VBOs);
            GL.DeleteVertexArray(VAO);
        }
    }
}
