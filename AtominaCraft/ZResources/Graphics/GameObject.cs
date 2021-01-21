using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Maths;
using System;

namespace AtominaCraft.ZResources.Graphics
{
    public class GameObject : IDisposable
    {
        public Mesh Mesh { get; set; }
        public Shader Shader { get; set; }
        public Texture Texture { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public GameObject()
        {
            Position = new Vector3(0, 0, 0);
            Scale = Vector3.Halfs;
            Rotation = Vector3.Zero;
        }

        public void Draw(PlayerCamera camera)
        {
            Matrix4 mv = WorldToLocal().Transposed();
            Matrix4 mvp = camera.Matrix() * LocalToWorld();
            if (Texture != null)
            {
                Texture.Use();
            }
            Shader.Use();
            Shader.SetMatrix(mvp, mv);
            Mesh.Draw();
        }

        public Matrix4 LocalToWorld()
        {
            return Matrix4.CreateLocalToWorld(Position, Rotation, Scale);
        }

        public Matrix4 WorldToLocal()
        {
            return Matrix4.CreateWorldToLocal(Position, Rotation, Scale);
        }

        public void DrawOutline(PlayerCamera camera)
        {
            DebugDraw.DrawCube(camera, Position, Scale);
        }

        public void Dispose()
        {
            Mesh.Dispose();
            Shader.Dispose();
            Texture.Dispose();
        }
    }
}
