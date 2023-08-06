using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.GFX;
using REghZy.MathsF;

namespace AtominaCraft.Client.BlockRendering {
    public class BlockObject {
        public CubeMesh Mesh;
        public Vector3f Position;
        public Vector3f Rotation;
        public Vector3f Scale;
        public Shader Shader;
        public Texture Texture;

        public BlockObject() {
            this.Position = new Vector3f(0, 0, 0);
            this.Scale = new Vector3f(0.5f);
            this.Rotation = Vector3f.Zero;
        }

        public void Draw(PlayerCamera camera) {
            if (this.Mesh == null)
                return;

            Matrix4 mv = WorldToLocal();
            Matrix4 mvp = camera.Matrix() * LocalToWorld();
            this.Texture?.Use();
            this.Shader.Use();
            this.Shader.SetMatrix(mvp, mv.Transposed);
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

        public void Dispose() {
            this.Shader.Dispose();
            this.Texture.Dispose();
        }
    }
}