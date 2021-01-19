using AtominaCraft.Entities.Player;
using AtominaCraft.Resources.Maths;
using OpenTK.Graphics.OpenGL;

namespace AtominaCraft.Resources.Graphics
{
    public static class DebugDraw
    {
        public static void Initialise()
        {

        }

        public static void DrawVertex(Vector4 v)
        {
            GL.Vertex4(v.X, v.Y, v.Z, v.W);
        }

        public static void DrawCube(PlayerCamera cam, Vector3 pos, Vector3 scale)
        {
            Matrix4 mvp = cam.Matrix() * Matrix4.CreateLocalToWorld(pos, Vector3.Zero, scale);

            Vector4 v1 = mvp * new Vector4( 1.0f,  1.0f, -1.0f, 1.0f);
            Vector4 v2 = mvp * new Vector4( 1.0f, -1.0f, -1.0f, 1.0f);
            Vector4 v3 = mvp * new Vector4(-1.0f, -1.0f, -1.0f, 1.0f);
            Vector4 v4 = mvp * new Vector4(-1.0f,  1.0f, -1.0f, 1.0f);
            Vector4 v5 = mvp * new Vector4(-1.0f,  1.0f,  1.0f, 1.0f);
            Vector4 v6 = mvp * new Vector4(-1.0f, -1.0f,  1.0f, 1.0f);
            Vector4 v7 = mvp * new Vector4( 1.0f, -1.0f,  1.0f, 1.0f);
            Vector4 v8 = mvp * new Vector4( 1.0f,  1.0f,  1.0f, 1.0f);

            GL.DepthFunc(DepthFunction.Lequal);
            GL.UseProgram(0);

			GL.LineWidth(5);

			GL.Color3(0.2f, 0.9f, 0.7f);
			GL.Begin(PrimitiveType.LineLoop);
			DrawVertex(v1); 
			DrawVertex(v2); 
			DrawVertex(v3);
			DrawVertex(v4); 
			GL.End();

			GL.Begin(PrimitiveType.LineLoop);
			DrawVertex(v4);
			DrawVertex(v5);
			DrawVertex(v6);
			DrawVertex(v3);
			GL.End();

			GL.Begin(PrimitiveType.LineLoop);
			DrawVertex(v6);
			DrawVertex(v5);
			DrawVertex(v8);
			DrawVertex(v7);
			GL.End();

			GL.Begin(PrimitiveType.LineLoop);
			DrawVertex(v8);
			DrawVertex(v7);
			DrawVertex(v2);
			DrawVertex(v1);
			GL.End();

			GL.DepthFunc(DepthFunction.Less);
		}
	}
}
