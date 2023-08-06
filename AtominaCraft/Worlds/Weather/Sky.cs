using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.GFX;
using OpenTK.Graphics.OpenGL;
using REghZy.MathsF;

namespace AtominaCraft.Worlds.Weather {
    public class Sky {
        public Vector3f SkyColour;

        private readonly int SkyColourID;
        public Mesh SkyQuad;
        public Shader SkyShader;
        public Vector3f SunLocation;
        private int SunLocationID;

        public Sky() {
            // could probably implement a day/night system just 
            // by messing around with the shaders

            this.SkyColour = new Vector3f(0.9f, 0.9f, 0.9f);
            this.SunLocation = new Vector3f();

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

            this.SkyQuad = GraphicsLoader.Quad;
            this.SkyShader = new Shader(skyV, skyF);

            this.SkyColourID = GL.GetUniformLocation(this.SkyShader.ProgramID, "skyColour");
        }

        public void Draw(PlayerCamera camera) {
            GL.DepthMask(false);
            Matrix4 mvp = camera.projection.Inversed;
            Matrix4 mv = camera.worldView.Inversed;
            this.SkyShader.Use();
            this.SkyShader.SetMatrix(mvp, mv);
            SetSkyColour(this.SkyColour.x, this.SkyColour.y, this.SkyColour.z);
            this.SkyQuad.Draw();
            GL.DepthMask(true);
        }

        public void SetSkyColour(float r, float g, float b) {
            GL.Uniform3(this.SkyColourID, r, g, b);
        }
    }
}