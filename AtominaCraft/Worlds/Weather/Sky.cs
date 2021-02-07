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

        private int SkyColourID { get; set; }
        private int SunLocationID { get; set; }

        public Vector3 SkyColour { get; set; }
        public Vector3 SunLocation { get; set; }

        public Sky()
        {
            // could probably implement a day/night system just 
            // by messing around with the shaders

            SkyColour = new Vector3(0.9f, 0.9f, 0.9f);
            SunLocation = new Vector3();

            string skyV =
                "#version 150\n" +
                "uniform mat4 mvp;\n" +
                "uniform mat4 mv;\n" +
                "in vec3 in_pos;\n" +
                "in vec2 in_uv;\n" +
                "out vec3 ex_normal;\n" +
                "void main(void) {\n" +
                "   gl_Position = vec4(in_pos.xy, 0.0, 1.0);\n" +
                "   vec3 eye_normal = normalize((mvp * gl_Position).xyz);\n" +
                "   ex_normal = normalize((mv * vec4(eye_normal, 0.0)).xyz);\n" +
                "}\n";

            string skyF =
                "#version 150\n" +
                "precision highp float;\n" +
                "#define LIGHT vec3(0.36, 0.80, 0.48)\n" +
                "#define SUN_SIZE 0.002\n" +
                "#define SUN_SHARPNESS 0.9\n" +
                "uniform vec3 skyColour;\n" +
                "in vec3 ex_normal;\n" +
                "void main(void) {\n" +
                "    vec3 n = normalize(ex_normal);\n" +
                "    float lowBrightArea = (1.0 - n.y) * (1.0 - n.y) * 0.35;\n" +
                "    vec3 sky_colour = vec3(0.2 + lowBrightArea, 0.5 + lowBrightArea, 1.0) * skyColour;\n" +
                "    float s = dot(n, LIGHT) - 1.0 + SUN_SIZE;\n" +
                "    float sun = min(exp(s * SUN_SHARPNESS / SUN_SIZE), 1.0);\n" +
                "    gl_FragColor = vec4(max(sky_colour, sun), 1.0);\n" +
                "}\n";

            SkyQuad = GraphicsLoader.Quad;
            SkyShader = new Shader(skyV, skyF);

            SkyColourID = GL.GetUniformLocation(SkyShader.ProgramID, "skyColour");
        }

        public void Draw(PlayerCamera camera)
        {
            GL.DepthMask(false);
            Matrix4 mvp = camera.Projection.Inverse();
            Matrix4 mv = camera.WorldView.Inverse();
            SkyShader.Use();
            SkyShader.SetMatrix(mvp, mv);
            SetSkyColour(SkyColour.X, SkyColour.Y, SkyColour.Z);
            SkyQuad.Draw();
            GL.DepthMask(true);
        }

        public void SetSkyColour(float r, float g, float b)
        {
            GL.Uniform3(SkyColourID, r, g, b);
        }
    }
}
