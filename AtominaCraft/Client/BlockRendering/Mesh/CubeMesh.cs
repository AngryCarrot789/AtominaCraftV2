using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using AtominaCraft.Blocks;

namespace AtominaCraft.Client.BlockRendering.Mesh
{
    public class CubeMesh : IDisposable
    {
        public const int VBO_COUNT = 3;

        private int VAO;
        private int[] VBOs;
        private List<float> Vertices;
        private List<float> UVs;
        private List<float> Normals;
        public BlockLocation Location;

        /// <summary>
        /// Generates a mesh using the given vertices, texture coordinates and normals
        /// <param name="vertices"></param>
        /// <param name="uvs"></param>
        /// <param name="normals"></param>
        public CubeMesh(List<float> vertices, List<float> uvs, List<float> normals)
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
            GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Count / 3);
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
