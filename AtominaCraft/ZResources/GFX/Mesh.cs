using System;
using System.Collections.Generic;
using System.IO;
using AtominaCraft.ZResources.Logging;
using REghzy.MathsF;
using AtominaCraft.ZResources.Strings;
using OpenTK.Graphics.OpenGL;
using REghZy.MathsF;

namespace AtominaCraft.ZResources.GFX {
    public class Mesh : IDisposable {
        /// <summary>
        ///     The number of vertex buffer objects. Vertex, UV/Textures, and Normals
        /// </summary>
        public const int VBO_COUNT = 3;

        /// <summary>
        ///     This would be used for putting textures over shapes, like a texture of
        ///     earth over a Globe. or a trianGle. #earthisatrianGle
        ///     And is also used for the sky cube thingy
        /// </summary>
        public bool Is3DTexture;

        public string MeshName;

        /// <summary>
        ///     Stores all of the meshes normals
        /// </summary>
        public List<float> Normals;

        /// <summary>
        ///     Stores all of the meshes texture coordinates
        /// </summary>
        public List<float> UVs;

        /// <summary>
        ///     Stores an ID to the vertex arrays object (the object contains all the buffers)
        /// </summary>
        public int VAO;

        /// <summary>
        ///     Contains IDs to the vertex buffers within the VAO
        /// </summary>
        public int[] VBOs;

        /// <summary>
        ///     Stores all of the meshes vertices
        /// </summary>
        public List<float> Vertices;

        /// <summary>
        ///     Generates a mesh using the given vertices and texture coordinates, and then auto-generates the normals
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="uvs"></param>
        public Mesh(List<float> vertices, List<float> uvs) {
            this.Vertices = vertices;
            this.UVs = uvs;
            this.Normals = new List<float>(vertices.Count);
            this.VBOs = new int[VBO_COUNT];

            // generate normals
            for (int i = 0; i < vertices.Count; i += 9) {
                Vector3f vertex1 = new Vector3f(
                    vertices[i + 0],
                    vertices[i + 1],
                    vertices[i + 2]
                );
                Vector3f vertex2 = new Vector3f(
                    vertices[i + 3],
                    vertices[i + 4],
                    vertices[i + 5]
                );
                Vector3f vertex3 = new Vector3f(
                    vertices[i + 6],
                    vertices[i + 7],
                    vertices[i + 8]
                );
                Vector3f normal = (vertex2 - vertex1).Cross(vertex3 - vertex1).Normalise();
                this.Normals.Add(normal.x);
                this.Normals.Add(normal.y);
                this.Normals.Add(normal.z);
                this.Normals.Add(normal.x);
                this.Normals.Add(normal.y);
                this.Normals.Add(normal.z);
                this.Normals.Add(normal.x);
                this.Normals.Add(normal.y);
                this.Normals.Add(normal.z);
            }

            // Generate vertex array buffer
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // Generate vertex buffer
            this.VBOs[0] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.Vertices.Count * sizeof(float), this.Vertices.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate texture buffer
            this.VBOs[1] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[1]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.UVs.Count * sizeof(float), this.UVs.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, this.Is3DTexture ? 3 : 2, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate normals buffer
            this.VBOs[2] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[2]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.Normals.Count * sizeof(float), this.Normals.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        /// <summary>
        ///     Generates a mesh using the given vertices, texture coordinates and normals
        ///     <param name="vertices"></param>
        ///     <param name="uvs"></param>
        ///     <param name="normals"></param>
        public Mesh(List<float> vertices, List<float> uvs, List<float> normals) {
            this.Vertices = vertices;
            this.UVs = uvs;
            this.Normals = normals;
            this.VBOs = new int[VBO_COUNT];

            // Generate vertex array buffer
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // Generate vertex buffer
            this.VBOs[0] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.Vertices.Count * sizeof(float), this.Vertices.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate texture buffer
            this.VBOs[1] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[1]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.UVs.Count * sizeof(float), this.UVs.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, this.Is3DTexture ? 3 : 2, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate normals buffer
            this.VBOs[2] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[2]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.Normals.Count * sizeof(float), this.Normals.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        public Mesh(string fileName) {
            if (!File.Exists(fileName)) {
                LogManager.GraphicsLogger.Log($"Mesh doesnt exist: {fileName}");
                return;
            }

            string[] meshContents = File.ReadAllLines(fileName);
            LoadMesh(meshContents);
        }

        public Mesh(string[] meshData) {
            LoadMesh(meshData);
        }

        /// <summary>
        ///     Get rid of the vertex buffers and the vertex arrays object
        /// </summary>
        public void Dispose() {
            GL.DeleteBuffers(this.VBOs.Length, this.VBOs);
            GL.DeleteVertexArray(this.VAO);
        }

        /// <summary>
        ///     Loads a mesh from a wavefront object file
        /// </summary>
        /// <param name="meshFilePath"></param>
        public void LoadMesh(string[] meshFilePath) {
            this.Vertices = new List<float>();
            this.UVs = new List<float>();
            this.Normals = new List<float>();
            this.VBOs = new int[VBO_COUNT];
            List<float> vertPalette = new List<float>();
            List<float> uvPalette = new List<float>();

            foreach (string line in meshFilePath) {
                if (line.Length < 2)
                    continue;

                // Vertex
                if (line.Check(0, 2, "v ")) {
                    float[] verts = line.Extract(2).Split(' ').ToFloats();
                    foreach (float vertex in verts)
                        vertPalette.Add(vertex);
                }

                // Vertex texture coordinate
                else if (line.Check(0, 3, "vt ")) {
                    // this should only be 2 long, the array
                    float[] uvs = line.Extract(3).Split(' ').ToFloats();
                    uvPalette.Add(uvs[0]);
                    uvPalette.Add(uvs[1]);
                    if (uvs.Length > 2) {
                        uvPalette.Add(uvs[2]);
                        this.Is3DTexture = true;
                    }
                }

                // Colliders
                else if (line.Check(0, 2, "c ")) {
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
                else if (line.Check(0, 2, "f ")) {
                    // 2/1/1..\n..3/2/1..\n..1/3/1
                    string[] faces = line.Extract(2).Split(' ');

                    bool floatSlash = false;
                    int numSlashes = line.CountElements('/');

                    int a = 0, b = 0, c = 0, d = 0;
                    int at = 0, bt = 0, ct = 0, dt = 0;

                    bool wild = line[2] == '*';
                    bool wild2 = line[3] == '*';
                    bool isQuad = false;

                    if (wild) {
                        if (numSlashes == 0) {
                            int vertexIx = vertPalette.Count / 3;
                            int textureIx = uvPalette.Count / (this.Is3DTexture ? 3 : 2);

                            if (wild2) {
                                a = vertexIx - 3;
                                b = vertexIx - 2;
                                c = vertexIx - 1;
                                d = vertexIx;
                                at = textureIx - 3;
                                bt = textureIx - 2;
                                ct = textureIx - 1;
                                dt = textureIx;
                                isQuad = true;
                            }
                            else {
                                a = vertexIx - 2;
                                b = vertexIx - 1;
                                c = vertexIx;
                                at = textureIx - 2;
                                bt = textureIx - 1;
                                ct = textureIx;
                            }
                        }
                    }
                    // 2/3/1 Vertex/Vertex/Vertex
                    else if (numSlashes == 0) {
                        int[] tempFaces = faces.ToInts();
                        if (faces.Length == 3) {
                            a = at = tempFaces[0];
                            b = bt = tempFaces[1];
                            c = ct = tempFaces[2];
                        }
                        else if (faces.Length == 4) {
                            a = at = tempFaces[0];
                            b = bt = tempFaces[1];
                            c = ct = tempFaces[2];
                            d = dt = tempFaces[3];
                            isQuad = true;
                        }
                    }
                    // 2/2 3/3 1/1
                    // V1/T1    V2/T2    V3/T3 
                    else if (numSlashes == 3) {
                        // ... = new element, 2/2...3/3...1/1 to 2/2/3/3/1/1 (trim just in case)
                        string joinedParts = string.Join('/', faces).Trim();
                        // 2/2/3/3/1/1 to 2...2...3...3...1...1
                        int[] faceVertices = joinedParts.Split('/').ToInts();
                        if (faceVertices.Length >= 6) {
                            a = faceVertices[0];
                            at = faceVertices[1];
                            b = faceVertices[2];
                            bt = faceVertices[3];
                            c = faceVertices[4];
                            ct = faceVertices[5];
                        }
                    }
                    // is this a quad? idek
                    else if (numSlashes == 4) {
                        string joined = string.Join('/', faces).Trim();
                        int[] allFaces = joined.Split('/').ToInts();
                        if (allFaces.Length >= 8) {
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
                    else if (numSlashes == 6) {
                        string joined = string.Join('/', faces).Trim();
                        int[] allFaces = joined.Split('/').ToInts();
                        // 8 just in case... should be 9 but o well
                        if (allFaces.Length >= 8) {
                            if (floatSlash) {
                                at = a = allFaces[0];
                                bt = b = allFaces[2];
                                ct = c = allFaces[4];
                            }
                            else {
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

                    else {
                        continue;
                    }

                    AddFace(vertPalette, uvPalette, a, at, b, bt, c, ct, this.Is3DTexture);
                    if (isQuad)
                        AddFace(vertPalette, uvPalette, c, ct, d, dt, a, at, this.Is3DTexture);
                }
            }

            // Generate vertex array buffer
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);


            // Generate vertex buffer
            this.VBOs[0] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.Vertices.Count * sizeof(float), this.Vertices.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate texture buffer
            this.VBOs[1] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[1]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.UVs.Count * sizeof(float), this.UVs.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, this.Is3DTexture ? 3 : 2, VertexAttribPointerType.Float, false, 0, 0);
            }
            // Generate normals buffer
            this.VBOs[2] = GL.GenBuffer();
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBOs[2]);
                GL.BufferData(BufferTarget.ArrayBuffer, this.Normals.Count * sizeof(float), this.Normals.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        /// <summary>
        ///     Binds the vertex arrays and draws them
        /// </summary>
        public void Draw() {
            GL.BindVertexArray(this.VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, this.Vertices.Count);
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
        ///     Adds a face to the meshes vertices, textures and normals
        /// </summary>
        /// <param name="vertPalette">all of the vertices usable. for a cube it might be 24 long</param>
        /// <param name="uvPalette">all of the texture coordinates</param>
        /// <param name="a">vertex x index</param>
        /// <param name="aT">texture x index (u)</param>
        /// <param name="b">vertex y index</param>
        /// <param name="bT">texture y index (v)</param>
        /// <param name="c">vertex z index</param>
        /// <param name="cT">texture z index (w)</param>
        /// <param name="is3dTexture">says whether the face will have a 3d texture</param>
        public void AddFace(
            List<float> vertPalette,
            List<float> uvPalette,
            int a,
            int aT,
            int b,
            int bT,
            int c,
            int cT,
            bool is3dTexture) {
            if (a > 0 && b > 0 && c > 0)
                if (aT > 0 && bT > 0 && cT > 0) {
                    // say...
                    /*          a = 5 
                     *          b = 3 
                     *          c = 1
                     *          
                     *          aT = 1
                     *          bT = 2
                     *          cT = 3
                     *          
                     *          now they're:
                     *          
                     *          a = 4
                     *          b = 2 
                     *          c = 0
                     *          aT = 0
                     *          bT = 1
                     *          cT = 2
                     */
                    a -= 1;
                    b -= 1;
                    c -= 1;
                    aT -= 1;
                    bT -= 1;
                    cT -= 1;
                    int[] vertexIndices = new int[3] {a, b, c};
                    int[] textureIndices = new int[3] {aT, bT, cT};
                    Vector3f vertex1 = vertPalette.GetVertexFromList(a * 3);
                    Vector3f vertex2 = vertPalette.GetVertexFromList(b * 3);
                    Vector3f vertex3 = vertPalette.GetVertexFromList(c * 3);
                    Vector3f normals = (vertex2 - vertex1).Cross(vertex3 - vertex1).Normalise();

                    for (int i = 0; i < 3; i++) {
                        int vertIndex = vertexIndices[i];
                        int textIndex = textureIndices[i];
                        if (vertIndex < vertPalette.Count / 3) {
                            this.Vertices.Add(vertPalette[vertIndex * 3]);
                            this.Vertices.Add(vertPalette[vertIndex * 3 + 1]);
                            this.Vertices.Add(vertPalette[vertIndex * 3 + 2]);
                            if (uvPalette.Count > 0) {
                                if (is3dTexture) {
                                    if (textIndex < uvPalette.Count / 3) {
                                        this.UVs.Add(uvPalette[textIndex * 3]);
                                        this.UVs.Add(uvPalette[textIndex * 3 + 1]);
                                        this.UVs.Add(uvPalette[textIndex * 3 + 2]);
                                    }
                                }
                                else {
                                    if (textIndex < uvPalette.Count / 2) {
                                        this.UVs.Add(uvPalette[textIndex * 2]);
                                        this.UVs.Add(uvPalette[textIndex * 2 + 1]);
                                    }
                                }
                            }
                            else {
                                this.UVs.Add(0.0f);
                                this.UVs.Add(0.0f);
                            }

                            this.Normals.Add(normals.x);
                            this.Normals.Add(normals.y);
                            this.Normals.Add(normals.z);
                        }
                    }
                }
        }
    }
}