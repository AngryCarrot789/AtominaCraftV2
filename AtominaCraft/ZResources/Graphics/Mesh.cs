using AtominaCraft.ZResources.Logging;
using AtominaCraft.ZResources.Maths;
using AtominaCraft.ZResources.Strings;
using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace AtominaCraft.ZResources.Graphics
{
    public class Mesh : IDisposable
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

        public string MeshName { get; set; }

        /// <summary>
        /// This would be used for putting textures over shapes, like a texture of 
        /// earth over a Globe. or a trianGle. #earthisatrianGle
        /// And is also used for the sky cube thingy
        /// </summary>
        public bool Is3DTexture { get; set; }

        public Mesh(float[] mesh)
        {
            Vertices = new List<float>(3);
            VBOs = new int[1];
            foreach (float vertex in mesh)
            {
                Vertices.Add(vertex);
            }

            // Generate vertex array buffer
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            VBOs[0] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * sizeof(float), Vertices.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        public Mesh(List<float> vertices, List<float> uvs)
        {
            Vertices = vertices;
            UVs = uvs;
            Normals = new List<float>(vertices.Count);
            VBOs = new int[VBO_COUNT];

            // generate normals
            for (int i = 0; i < vertices.Count; i += 3)
            {
                float vertex = vertices[i];
                Vector3 vertex1 = new Vector3(vertices[i + 0]);
                Vector3 vertex2 = new Vector3(vertices[i + 1]);
                Vector3 vertex3 = new Vector3(vertices[i + 2]);
                Vector3 normals = (vertex2 - vertex1).Cross(vertex3 - vertex1).Normalised();
                Normals.Add(normals.X);
                Normals.Add(normals.Y);
                Normals.Add(normals.Z);
            }

            // Generate vertex array object
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // Generate vertex buffer objects
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
                GL.VertexAttribPointer(1, (Is3DTexture ? 3 : 2), VertexAttribPointerType.Float, false, 0, 0);
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

        public Mesh(string fileName)
        {
            LoadMesh(fileName);
        }

        /// <summary>
        /// Loads a mesh from a wavefront object file
        /// </summary>
        /// <param name="meshFilePath"></param>
        public void LoadMesh(string meshFilePath)
        {
            Vertices = new List<float>();
            UVs = new List<float>();
            Normals = new List<float>();
            VBOs = new int[VBO_COUNT];

            if (!File.Exists(meshFilePath))
            {
                LogManager.GraphicsLogger.Log($"Mesh doesnt exist: {meshFilePath}");
            }
            else
            {
                MeshName = Path.GetFileNameWithoutExtension(meshFilePath);

                List<float> vertPalette = new List<float>();
                List<float> uvPalette = new List<float>();

                string[] meshContents = File.ReadAllLines(meshFilePath);
                foreach (string line in meshContents)
                {
                    if (line.Length < 2)
                    {
                        continue;
                    }

                    // Vertex
                    if (line.Check(0, 2, "v "))
                    {
                        float[] verts = line.Extract(2).Split(' ').Tofloats();
                        foreach (float vertex in verts)
                        {
                            vertPalette.Add(vertex);
                        }
                    }

                    // Vertex texture coordinate
                    else if (line.Check(0, 3, "vt "))
                    {
                        // this should only be 2 long, the array
                        float[] uvs = line.Extract(3).Split(' ').Tofloats();
                        uvPalette.Add(uvs[0]);
                        uvPalette.Add(uvs[1]);
                        if (uvs.Length > 2)
                        {
                            uvPalette.Add(uvs[2]);
                            Is3DTexture = true;
                        }
                    }

                    // Colliders
                    else if (line.Check(0, 2, "c "))
                    {
                        //int a = 0, b = 0, c = 0;
                        //if (line[2] == '*')
                        //{
                        //    int vIx = vertPalette.Count / 3;
                        //    a = vIx - 2; b = vIx - 1; c = vIx;
                        //}
                        //else
                        //{
                        //    int[] colliders = line.Substring(3).Split(' ').ToInts();
                        //    if (colliders.Length >= 3)
                        //    {
                        //        a = colliders[0];
                        //        b = colliders[1];
                        //        c = colliders[2];
                        //    }
                        //}
                        //
                        //Vector3 v1 = vertPalette.FromList((a - 1) * 3);
                        //Vector3 v2 = vertPalette.FromList((b - 1) * 3);
                        //Vector3 v3 = vertPalette.FromList((c - 1) * 3);
                        //Colliders.Add(new Collider(v1, v2, v3));
                    }

                    // Faces
                    // aka vertex indices.
                    else if (line.Check(0, 2, "f "))
                    {
                        // 2/1/1...3/2/1...1/3/1
                        string[] faces = line.Extract(2).Split(' ');

                        bool floatSlash = false;
                        int numSlashes = line.CountElements('/');

                        int a = 0, b = 0, c = 0, d = 0;
                        int at = 0, bt = 0, ct = 0, dt = 0;
                        int temp;

                        bool wild = line[2] == '*';
                        bool wild2 = line[3] == '*';
                        bool isQuad = false;

                        if (wild)
                        {
                            if (numSlashes == 0)
                            {
                                int vertexIx = vertPalette.Count / 3;
                                int textureIx = uvPalette.Count / (Is3DTexture ? 3 : 2);

                                if (wild2)
                                {
                                    a = vertexIx - 3; b = vertexIx - 2; c = vertexIx - 1; d = vertexIx;
                                    at = textureIx - 3; bt = textureIx - 2; ct = textureIx - 1; dt = textureIx;
                                    isQuad = true;
                                }
                                else
                                {
                                    a = vertexIx - 2; b = vertexIx - 1; c = vertexIx;
                                    at = textureIx - 2; bt = textureIx - 1; ct = textureIx;
                                }
                            }
                        }
                        // 2/3/1 Vertex/Vertex/Vertex
                        else if (numSlashes == 0)
                        {
                            int[] tempFaces = faces.ToInts();
                            if (faces.Length == 3)
                            {
                                a = at = tempFaces[0];
                                b = bt = tempFaces[1];
                                c = ct = tempFaces[2];
                            }
                            else if (faces.Length == 4)
                            {
                                a = at = tempFaces[0];
                                b = bt = tempFaces[1];
                                c = ct = tempFaces[2];
                                d = dt = tempFaces[3];
                                isQuad = true;
                            }
                        }
                        // 2/2 3/3 1/1
                        // V1/T1    V2/T2    V3/T3 
                        else if (numSlashes == 3)
                        {
                            // ... = new element, 2/2...3/3...1/1 to 2/2/3/3/1/1 (trim just in case)
                            string joinedVertices = string.Join('/', faces).Trim();
                            // 2/2/3/3/1/1 to 2...2...3...3...1...1
                            int[] faceVertices = joinedVertices.Split('/').ToInts();
                            if (faceVertices.Length >= 6)
                            {
                                a = faceVertices[0];
                                at = faceVertices[1];
                                b = faceVertices[2];
                                bt = faceVertices[3];
                                c = faceVertices[4];
                                ct = faceVertices[5];
                            }
                        }
                        // is this a quad? idek
                        else if (numSlashes == 4)
                        {
                            string joined = string.Join('/', faces).Trim();
                            int[] allFaces = joined.Split('/').ToInts();
                            if (allFaces.Length >= 8)
                            {
                                a = allFaces[0];
                                at = allFaces[1];
                                b = allFaces[2];
                                bt = allFaces[3];
                                c = allFaces[4];
                                ct = allFaces[5];
                                d = allFaces[6];
                                dt = allFaces[7];
                            }
                        }
                        // 2/1/1 3/2/1 1/3/1
                        // V1/T1/N1    V2/T2/N2    V3/T3/N3 
                        else if (numSlashes == 6)
                        {
                            string joined = string.Join('/', faces).Trim();
                            int[] allFaces = joined.Split('/').ToInts();
                            // 8 just in case... should be 9 but o well
                            if (allFaces.Length >= 8)
                            {
                                if (floatSlash)
                                {
                                    at = a = allFaces[0];
                                    bt = b = allFaces[2];
                                    ct = c = allFaces[4];
                                }
                                else
                                {
                                    a = allFaces[0];
                                    at = allFaces[1];
                                    b = allFaces[3];
                                    bt = allFaces[4];
                                    c = allFaces[6];
                                    ct = allFaces[7];
                                }
                            }
                            //foreach(string number in faces)
                        }

                        else continue;

                        AddFace(vertPalette, uvPalette, a, at, b, bt, c, ct, Is3DTexture);
                        if (isQuad)
                        {
                            AddFace(vertPalette, uvPalette, c, ct, d, dt, a, at, Is3DTexture);
                        }
                    }
                }
            }

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
                GL.VertexAttribPointer(1, (Is3DTexture ? 3 : 2), VertexAttribPointerType.Float, false, 0, 0);
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
            //GL.Begin(PrimitiveType.Triangles);
            //for(int i = 0; i < Vertices.Count; i += 3)
            //{
            //    GL.Vertex3(
            //        Vertices[i + 0],
            //        Vertices[i + 1],
            //        Vertices[i + 2]);
            //}
            //GL.End();
        }

        /// <summary>
        /// Adds a face to the meshes vertices, textures and normals
        /// </summary>
        /// <param name="vertPalette">all of the face's vertices. should only be 3 long</param>
        /// <param name="uvPalette">all of the texture coordinates. should be either 2 or 3 long</param>
        /// <param name="x" >vertex x position</param>
        /// <param name="xT">texture x position</param>
        /// <param name="y" >vertex y position</param>
        /// <param name="yT">texture y position</param>
        /// <param name="z" >vertex z position</param>
        /// <param name="zT">texture z position</param>
        /// <param name="is3dTexture">says whether the face will have a 3d texture</param>
        public void AddFace(List<float> vertPalette, List<float> uvPalette, int x, int xT, int y, int yT, int z, int zT, bool is3dTexture)
        {
            if (x > 0 && y > 0 && z > 0)
            {
                if (xT > 0 && yT > 0 && zT > 0)
                {
                    x -= 1; y -= 1; z -= 1;
                    xT -= 1; yT -= 1; zT -= 1;
                    int[] vIx = new int[3] { x, y, z };
                    int[] uvIx = new int[3] { xT, yT, zT };
                    Vector3 vertex1 = vertPalette.GetVertexFromList(x * 3);
                    Vector3 vertex2 = vertPalette.GetVertexFromList(y * 3);
                    Vector3 vertex3 = vertPalette.GetVertexFromList(z * 3);
                    Vector3 normals = (vertex2 - vertex1).Cross(vertex3 - vertex1).Normalised();

                    for (int i = 0; i < 3; i++)
                    {
                        int v = vIx[i];
                        int vt = uvIx[i];
                        if (v < (vertPalette.Count / 3))
                        {
                            Vertices.Add(vertPalette[v * 3]);
                            Vertices.Add(vertPalette[v * 3 + 1]);
                            Vertices.Add(vertPalette[v * 3 + 2]);
                            if (uvPalette.Count > 0)
                            {
                                if (is3dTexture)
                                {
                                    if (vt < (uvPalette.Count / 3))
                                    {
                                        UVs.Add(uvPalette[vt * 3]);
                                        UVs.Add(uvPalette[vt * 3 + 1]);
                                        UVs.Add(uvPalette[vt * 3 + 2]);
                                    }
                                }
                                else
                                {
                                    if (vt < (uvPalette.Count / 2))
                                    {
                                        UVs.Add(uvPalette[vt * 2]);
                                        UVs.Add(uvPalette[vt * 2 + 1]);
                                    }
                                }
                            }
                            else
                            {
                                UVs.Add(0.0f);
                                UVs.Add(0.0f);
                            }
                            Normals.Add(normals.X);
                            Normals.Add(normals.Y);
                            Normals.Add(normals.Z);
                        }
                    }
                }
            }
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
