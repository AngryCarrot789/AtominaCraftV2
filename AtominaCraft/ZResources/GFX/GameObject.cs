using System;
using AtominaCraft.Entities.Player;
using REghZy.MathsF;
using Matrix4 = REghZy.MathsF.Matrix4;

namespace AtominaCraft.ZResources.GFX {
    public class GameObject : IDisposable {
        public Mesh Mesh;

        public Vector3f Position;
        public Vector3f Rotation;
        public Vector3f Scale;
        public Shader Shader;
        public Texture Texture;

        public GameObject() {
            this.Position = new Vector3f(0, 0, 0);
            this.Scale = new Vector3f(1.0f);
            this.Rotation = Vector3f.Zero;
        }

        public void Dispose() {
            this.Mesh.Dispose();
            this.Shader.Dispose();
            this.Texture.Dispose();
        }

        public void Draw(PlayerCamera camera) {
            Matrix4 mv = WorldToLocal().Transposed;
            Matrix4 mvp = camera.Matrix() * LocalToWorld();
            this.Texture?.Use();
            this.Shader.Use();
            this.Shader.SetMatrix(mvp, mv);

            this.Mesh.Draw();
        }

        public Matrix4 LocalToWorld() {
            return Matrix4.LocalToWorld(this.Position, this.Rotation, this.Scale);
        }

        public Matrix4 WorldToLocal() {
            return Matrix4.WorldToLocal(this.Position, this.Rotation, this.Scale);
        }

        public void DrawOutline(PlayerCamera camera) {
            DebugDraw.DrawCube(camera, this.Position, this.Scale);
        }
    }
}