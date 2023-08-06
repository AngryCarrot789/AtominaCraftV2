using AtominaCraft.Client.BlockRendering;

namespace AtominaCraft.ZResources.GFX {
    public static class GraphicsLoader {
        public static Mesh Cube;
        public static Mesh Quad;
        public static Shader TextureShader;
        public static Shader PinkShader;

        public static void Load() {
            TextureMap.Initialise();

            string[] cubeObject = {
                "v -1.0 -1.00  1.0\n",
                "v -1.0  1.00  1.0\n",
                "v -1.0 -1.00 -1.0\n",
                "v -1.0  1.00 -1.0\n",
                "v  1.0 -1.00  1.0\n",
                "v  1.0  1.00  1.0\n",
                "v  1.0 -1.00 -1.0\n",
                "v  1.0  1.00 -1.0\n",
                "\n",
                "vt -2.0 0.0\n",
                "vt -1.0 1.0\n",
                "vt -2.0 1.0\n",
                "vt -1.0 0.0\n",
                "vt 0.0  1.0\n",
                "vt 0.0  0.0\n",
                "vt 1.0  1.0\n",
                "vt 1.0  0.0\n",
                "vt 2.0  1.0\n",
                "vt 1.0  2.0\n",
                "vt 0.0  2.0\n",
                "vt 0.0 -1.0\n",
                "vt 2.0  0.0\n",
                "vt 1.0 -1.0\n",
                "\n",
                "s off\n",
                "f 2/1 3/2 1/3\n",
                "f 4/4 7/5 3/2\n",
                "f 8/6 5/7 7/5\n",
                "f 6/8 1/9 5/7\n",
                "f 7/5 1/10 3/11\n",
                "f 4/12 6/8 8/6\n",
                "f 2/1 4/4 3/2\n",
                "f 4/4 8/6 7/5\n",
                "f 8/6 6/8 5/7\n",
                "f 6/8 2/13 1/9\n",
                "f 7/5 5/7 1/10\n",
                "f 4/12 2/14 6/8\n"
            };

            string[] quadObject = {
                "v 1 1 0\n",
                "v -1 1 0\n",
                "v -1 -1 0\n",
                "v 1 -1 0\n",
                "\n",
                "vt 1 1 0\n",
                "vt 0 1 0\n",
                "vt 0 0 0\n",
                "vt 1 0 0\n",
                "\n",
                "f 1/1 2/2 3/3\n",
                "f 1/1 3/3 4/4\n"
            };

            Cube = new Mesh(cubeObject);
            Quad = new Mesh(quadObject);

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