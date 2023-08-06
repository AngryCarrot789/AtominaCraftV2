using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace AtominaCraft.Client.BlockRendering.Mesh {
    public class CubeMesh : IDisposable {
        public const int VBO_COUNT = 3;
        public readonly int VAO;
        public readonly int[] VBOs;
        public readonly int VertexCount;
        // private readonly float[] Vertices;
        // private readonly float[] Normals;
        // private readonly float[] UVs;

        /// <summary>
        /// Generates a mesh using the given vertices, texture coordinates and normals
        /// <param name="vertices"></param>
        /// <param name="uvs"></param>
        /// <param name="normals"></param>
        public CubeMesh(List<float> vertices, List<float> uvs, List<float> normals) {
            this.VertexCount = vertices.Count / 3;
            this.VBOs = new int[VBO_COUNT];

            // Generate vertex array buffer
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // Generate vertex buffer
            this.VBOs[0] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate texture buffer
            this.VBOs[1] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[1]);
                GL.BufferData(BufferTarget.ArrayBuffer, uvs.Count * sizeof(float), uvs.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate normals buffer
            this.VBOs[2] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[2]);
                GL.BufferData(BufferTarget.ArrayBuffer, normals.Count * sizeof(float), normals.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        /// <summary>
        ///     Get rid of the vertex buffers and the vertex arrays object
        /// </summary>
        public void Dispose() {
            GL.DeleteBuffers(this.VBOs.Length, this.VBOs);
            GL.DeleteVertexArray(this.VAO);
        }

        /// <summary>
        ///     Binds the vertex arrays and draws them
        /// </summary>
        public void Draw() {
            GL.BindVertexArray(this.VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, VertexCount);
        }
    }
}