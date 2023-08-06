using AtominaCraft.BlockGrid;
using AtominaCraft.Collision;
using AtominaCraft.Entities.Player;
using AtominaCraft.Worlds.Chunks;
using REghzy.MathsF;
using OpenTK.Graphics.OpenGL;
using REghZy.MathsF;

namespace AtominaCraft.ZResources.GFX {
    public static class DebugDraw {
        public static void Initialise() {
        }

        public static void Vertex4(Vector4f v) {
            GL.Vertex4(v.x, v.y, v.z, v.w);
        }

        public static void DrawChunk(PlayerCamera camera, Chunk chunk) {
            DrawCube(
                camera,
                GridLatch.WTMGetChunk(chunk.coords),
                GridLatch.ChunkScale,
                1.0f,
                0.1f,
                0.1f);
        }

        public static void DrawChunkCenterOutline(PlayerCamera camera, Chunk chunk, float r = 0.2f, float g = 0.8f, float b = 0.3f) {
            Vector3f position = GridLatch.WTMGetChunk(chunk.coords);
            Matrix4 mvp = camera.Matrix() * Matrix4.LocalToWorld(position, Vector3f.Zero, GridLatch.ChunkScale);
            Vector4f v1 = mvp * new Vector4f(1.0f, -1.0f, 0.0f, 1.0f);
            Vector4f v2 = mvp * new Vector4f(1.0f, 1.0f, 0.0f, 1.0f);
            Vector4f v3 = mvp * new Vector4f(0.0f, -1.0f, -1.0f, 1.0f);
            Vector4f v4 = mvp * new Vector4f(0.0f, 1.0f, -1.0f, 1.0f);
            Vector4f v5 = mvp * new Vector4f(-1.0f, -1.0f, 0.0f, 1.0f);
            Vector4f v6 = mvp * new Vector4f(-1.0f, 1.0f, 0.0f, 1.0f);
            Vector4f v7 = mvp * new Vector4f(0.0f, -1.0f, 1.0f, 1.0f);
            Vector4f v8 = mvp * new Vector4f(0.0f, 1.0f, 1.0f, 1.0f);

            GL.DepthFunc(DepthFunction.Lequal);
            GL.UseProgram(0);

            GL.LineWidth(2);

            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v1);
            Vertex4(v2);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v3);
            Vertex4(v4);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v5);
            Vertex4(v6);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v7);
            Vertex4(v8);
            GL.End();

            GL.DepthFunc(DepthFunction.Less);
            GL.LineWidth(1);
        }

        public static void DrawAABB(PlayerCamera camera, AxisAlignedBB boundingBox) {
            DrawCube(camera, boundingBox.GetCenter(), boundingBox.GetScale());
        }

        public static void DrawCube(PlayerCamera cam, Vector3f pos, Vector3f scale, float r = 0.2f, float g = 0.9f, float b = 0.7f) {
            Matrix4 mvp = cam.Matrix() * Matrix4.LocalToWorld(pos, Vector3f.Zero, scale);
            Vector4f v1 = mvp * new Vector4f(1.0f, 1.0f, -1.0f, 1.0f);
            Vector4f v2 = mvp * new Vector4f(1.0f, -1.0f, -1.0f, 1.0f);
            Vector4f v3 = mvp * new Vector4f(-1.0f, -1.0f, -1.0f, 1.0f);
            Vector4f v4 = mvp * new Vector4f(-1.0f, 1.0f, -1.0f, 1.0f);
            Vector4f v5 = mvp * new Vector4f(-1.0f, 1.0f, 1.0f, 1.0f);
            Vector4f v6 = mvp * new Vector4f(-1.0f, -1.0f, 1.0f, 1.0f);
            Vector4f v7 = mvp * new Vector4f(1.0f, -1.0f, 1.0f, 1.0f);
            Vector4f v8 = mvp * new Vector4f(1.0f, 1.0f, 1.0f, 1.0f);

            GL.DepthFunc(DepthFunction.Lequal);
            GL.UseProgram(0);

            GL.LineWidth(2);

            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v1);
            Vertex4(v2);
            Vertex4(v3);
            Vertex4(v4);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v4);
            Vertex4(v5);
            Vertex4(v6);
            Vertex4(v3);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v6);
            Vertex4(v5);
            Vertex4(v8);
            Vertex4(v7);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v8);
            Vertex4(v7);
            Vertex4(v2);
            Vertex4(v1);
            GL.End();

            GL.DepthFunc(DepthFunction.Less);
            GL.LineWidth(1);
        }

        public static void DrawXYZ(in Matrix4 proj, float rotX, float rotY) {
            Vector3f position = new Vector3f(0.0f, 0.0f, -1.0f);
            Vector3f scale = new Vector3f(0.1f);

            Quaternion y = Quaternion.AngleAxis(-rotY, Vector3f.Up);
            Quaternion x = Quaternion.AngleAxis(-rotX, Vector3f.Backward);
            Vector3f euler = (y * x).ToEuler();

            Matrix4 worldView = Matrix4.LocalToWorld(position, euler, scale);
            Matrix4 worldProjected = proj * worldView;
            Vector4f c = worldProjected * new Vector4f(0, 0, 0, 1);
            Vector4f xP = worldProjected * new Vector4f(1.5f, 0.0f, 0.0f, 1);
            Vector4f yP = worldProjected * new Vector4f(0.0f, 1.5f, 0.0f, 1);
            Vector4f zP = worldProjected * new Vector4f(0.0f, 0.0f, 1.5f, 1);
            Vector4f xN = worldProjected * new Vector4f(-0.5f, 0.0f, 0.0f, 1);
            Vector4f yN = worldProjected * new Vector4f(0.0f, -0.5f, 0.0f, 1);
            Vector4f zN = worldProjected * new Vector4f(0.0f, 0.0f, -0.5f, 1);

            GL.LineWidth(2);
            GL.DepthFunc(DepthFunction.Always);
            GL.UseProgram(0);

            // positives

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(1.0f, 0.0f, 0.0f);
            Vertex4(c);
            Vertex4(xP);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(0.0f, 1.0f, 0.0f);
            Vertex4(c);
            Vertex4(yP);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(0.0f, 0.0f, 1.0f);
            Vertex4(c);
            Vertex4(zP);
            GL.End();

            // negatives

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(0.5f, 0.0f, 0.0f);
            Vertex4(c);
            Vertex4(xN);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(0.0f, 0.5f, 0.0f);
            Vertex4(c);
            Vertex4(yN);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(0.0f, 0.0f, 0.5f);
            Vertex4(c);
            Vertex4(zN);
            GL.End();

            Vector4f v1 = worldProjected * new Vector4f(1.0f, 1.0f, -1.0f, 1.0f);
            Vector4f v2 = worldProjected * new Vector4f(1.0f, -1.0f, -1.0f, 1.0f);
            Vector4f v3 = worldProjected * new Vector4f(-1.0f, -1.0f, -1.0f, 1.0f);
            Vector4f v4 = worldProjected * new Vector4f(-1.0f, 1.0f, -1.0f, 1.0f);
            Vector4f v5 = worldProjected * new Vector4f(-1.0f, 1.0f, 1.0f, 1.0f);
            Vector4f v6 = worldProjected * new Vector4f(-1.0f, -1.0f, 1.0f, 1.0f);
            Vector4f v7 = worldProjected * new Vector4f(1.0f, -1.0f, 1.0f, 1.0f);
            Vector4f v8 = worldProjected * new Vector4f(1.0f, 1.0f, 1.0f, 1.0f);

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(0.8f, 0.3f, 0.7f);
            Vertex4(v1);
            Vertex4(v2);
            Vertex4(v3);
            Vertex4(v4);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v4);
            Vertex4(v5);
            Vertex4(v6);
            Vertex4(v3);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v6);
            Vertex4(v5);
            Vertex4(v8);
            Vertex4(v7);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            Vertex4(v8);
            Vertex4(v7);
            Vertex4(v2);
            Vertex4(v1);
            GL.End();

            GL.DepthFunc(DepthFunction.Lequal);
            GL.LineWidth(1);
        }
    }
}