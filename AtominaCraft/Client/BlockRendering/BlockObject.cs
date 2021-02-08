using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Client.BlockRendering
{
    public class BlockObject
    {
        public CubeMesh Mesh;
        public Shader Shader;
        public Texture Texture;

        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        private Matrix4 MV;
        private Matrix4 MVP;

        public BlockObject()
        {
            Position = new Vector3(0, 0, 0);
            Scale = Vector3.Halfs;
            Rotation = Vector3.Zero;

            MV = Matrix4.Identity();
            MVP = Matrix4.Identity();
        }

        public void Draw(PlayerCamera camera)
        {
            if (Texture == null)
                return;
            MV.WorldToLocal(Position, Rotation, Scale);
            MVP = camera.Matrix() * LocalToWorld();
            Texture.Use();
            Shader.Use();
            Shader.SetMatrix(MVP, MV.Transposed());
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
