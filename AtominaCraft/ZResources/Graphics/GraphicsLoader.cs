using AtominaCraft.Blocks.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtominaCraft.ZResources.Graphics
{
    public static class GraphicsLoader
    {
        public static Mesh Cube { get; set; }
        public static Mesh Quad { get; set; }
        public static Shader TextureShader { get; set; }
        public static Shader PinkShader { get; set; }

        public static void Load()
        {
            BlockTextureLinker.LoadTextures();

            Cube = new Mesh(Path.Combine(ResourceLocator.GetMeshesDirectory(), "cube.obj"));
            Quad = new Mesh(Path.Combine(ResourceLocator.GetMeshesDirectory(), "quad.obj"));

            string textureV =
                "#version 460\n" +
                "uniform mat4 mvp;\n" +
                "uniform mat4 mv;\n" +
                "in vec3 in_pos;\n" +
                "in vec2 in_uv;\n" +
                "in vec3 in_normal;\n" +
                "out vec2 ex_uv;\n" +
                "out vec3 ex_normal;\n" +
                "void main() {\n" +
                "  gl_Position = mvp * vec4(in_pos, 1.0);\n" +
                "  ex_uv = in_uv;\n" +
                "  ex_normal = normalize((mv * vec4(in_normal, 0.0)).xyz);\n" +
                "}\n";

            string textureF =
                "#version 460\n" +
                "precision highp float;\n" +
                "#define LIGHT vec3(0.36, 0.80, 0.48)\n" +
                "uniform sampler2D tex;\n" +
                "in vec2 ex_uv;\n" +
                "in vec3 ex_normal;\n" +
                "void main() {\n" +
                "  float s = dot(ex_normal, LIGHT)*0.5 + 0.5;\n" +
                "  gl_FragColor = vec4(texture(tex, ex_uv).rgb * s, 1.0);\n" +
                "}\n";


            TextureShader = new Shader(textureV, textureF);

            string vertexShader =
                "#version 330\n" +
                "uniform mat4 mvp;\n" +
                "in vec3 in_pos;\n" +
                "void main() { gl_Position = mvp * vec4(in_pos, 1.0); }\n";


            string fragmentShader =
                "#version 330\n" +
                "void main() { gl_FragColor = vec4(0.8, 0.2, 1.0, 1.0); }\n";

            PinkShader = new Shader(vertexShader, fragmentShader);
        }
    }
}
