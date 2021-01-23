using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using AtominaCraft.ZResources.Maths;

namespace AtominaCraft.Worlds.Weather
{
    public class Sky
    {
        public Mesh SkyQuad { get; set; }
        public Shader SkyShader { get; set; }

        public Sky()
        {
            SkyQuad = GraphicsLoader.Quad;
            //SkyShader = GraphicsLoader.SkyShader;
        }

        public void Draw(PlayerCamera camera)
        {
            //GL.DepthMask(false);
            //Matrix4 mvp = camera.Projection.Inverse();
            //Matrix4 mv = camera.WorldView.Inverse();
            //SkyShader.Use();
            //SkyShader.SetMatrix(mvp, mv);
            //SkyQuad.Draw();
            //GL.DepthMask(true);
        }
    }
}
