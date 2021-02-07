using AtominaCraft.BlockGrid;
using AtominaCraft.Collision;
using AtominaCraft.Entities.Player;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.ZResources.Maths;
using OpenTK.Graphics.OpenGL;
using System;

namespace AtominaCraft.ZResources.Graphics
{
    public static class DebugDraw
    {
		public static void Initialise()
        {

        }

        public static void Vertex4(Vector4 v)
        {
            GL.Vertex4(v.X, v.Y, v.Z, v.W);
        }

		public static void DrawChunk(PlayerCamera camera, Chunk chunk)
        {
			DrawCube(
				camera, 
				GridLatch.WTMGetChunk(chunk.Location), 
				GridLatch.ChunkScale, 
				1.0f, 
				0.1f, 
				0.1f);
        }

		public static void DrawChunkCenterOutline(PlayerCamera camera, Chunk chunk, float r = 0.2f, float g = 0.8f, float b = 0.3f)
        {
			Vector3 position = GridLatch.WTMGetChunk(chunk.Location);
			Matrix4 mvp = camera.Matrix() * Matrix4.CreateLocalToWorld(position, Vector3.Zero, GridLatch.ChunkScale);
			Vector4 v1 = mvp * new Vector4( 1.0f, -1.0f,  0.0f, 1.0f);
			Vector4 v2 = mvp * new Vector4( 1.0f,  1.0f,  0.0f, 1.0f);
			Vector4 v3 = mvp * new Vector4( 0.0f, -1.0f, -1.0f, 1.0f);
			Vector4 v4 = mvp * new Vector4( 0.0f,  1.0f, -1.0f, 1.0f);
			Vector4 v5 = mvp * new Vector4(-1.0f, -1.0f,  0.0f, 1.0f);
			Vector4 v6 = mvp * new Vector4(-1.0f,  1.0f,  0.0f, 1.0f);
			Vector4 v7 = mvp * new Vector4( 0.0f, -1.0f,  1.0f, 1.0f);
			Vector4 v8 = mvp * new Vector4( 0.0f,  1.0f,  1.0f, 1.0f);

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

		public static void DrawAABB(PlayerCamera camera, AxisAlignedBB boundingBox)
		{
			DrawCube(camera, boundingBox.GetCenter(), boundingBox.GetScale());
		}

		public static void DrawCube(PlayerCamera cam, Vector3 pos, Vector3 scale, float r = 0.2f, float g = 0.9f, float b = 0.7f)
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

		public static void DrawXYZ(Matrix4 proj, float rotX, float rotY)
        {
			Vector3 position = new Vector3(0.0f, 0.0f, -1.0f);
			Vector3 scale = new Vector3(0.1f);

			Quaternion y = Quaternion.AngleAxis(-rotY, Vector3.Up);
			Quaternion x = Quaternion.AngleAxis(-rotX, Vector3.Backward);
			Quaternion q = y * x;
			Vector3 euler = Quaternion.ToEuler(q);

			Matrix4 worldView = Matrix4.CreateLocalToWorld(position, euler, scale);
			Matrix4 worldProjected = proj * worldView;
			Vector4 c = worldProjected * new Vector4(0, 0, 0, 1);
			Vector4 xP = worldProjected * new Vector4(1.5f, 0.0f, 0.0f, 1);
			Vector4 yP = worldProjected * new Vector4(0.0f, 1.5f, 0.0f, 1);
			Vector4 zP = worldProjected * new Vector4(0.0f, 0.0f, 1.5f, 1);
			Vector4 xN = worldProjected * new Vector4(-0.5f, 0.0f, 0.0f, 1);
			Vector4 yN = worldProjected * new Vector4(0.0f, -0.5f, 0.0f, 1);
			Vector4 zN = worldProjected * new Vector4(0.0f, 0.0f, -0.5f, 1);

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

			Vector4 v1 = worldProjected * new Vector4(1.0f, 1.0f, -1.0f, 1.0f);
			Vector4 v2 = worldProjected * new Vector4(1.0f, -1.0f, -1.0f, 1.0f);
			Vector4 v3 = worldProjected * new Vector4(-1.0f, -1.0f, -1.0f, 1.0f);
			Vector4 v4 = worldProjected * new Vector4(-1.0f, 1.0f, -1.0f, 1.0f);
			Vector4 v5 = worldProjected * new Vector4(-1.0f, 1.0f, 1.0f, 1.0f);
			Vector4 v6 = worldProjected * new Vector4(-1.0f, -1.0f, 1.0f, 1.0f);
			Vector4 v7 = worldProjected * new Vector4(1.0f, -1.0f, 1.0f, 1.0f);
			Vector4 v8 = worldProjected * new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

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
