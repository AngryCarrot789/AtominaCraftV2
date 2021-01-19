using AtominaCraft.Resources;
using AtominaCraft.Resources.Maths;
using OpenTK.Graphics.OpenGL;

namespace AtominaCraft.Entities.Player
{
    /// <summary>
    /// A class for holding information for the main camera
    /// </summary>
    public class PlayerCamera
    {
        public int ViewportWidth { get; private set; }
        public int ViewportHeight { get; private set; }
        public float Near { get; private set; }
        public float Far { get; private set; }
        public float FOV { get; private set; }

        /// <summary>
        /// Represents the matrix used for projecting a view matrix
        /// </summary>
        public Matrix4 Projection { get; set; }
        /// <summary>
        /// The camera's world view matrix, 
        /// </summary>
        public Matrix4 WorldView { get; set; }

        public PlayerCamera()
        {
            ViewportWidth = 256;
            ViewportHeight = 256;
            WorldView = Matrix4.Identity();
            Projection = Matrix4.Identity();
        }

        public void SetSize(int width, int height, float near, float far, float fov = GameSettings.RENDER_FOV)
        {
            ViewportWidth = width;
            ViewportHeight = height;
            Near = near;
            Far = far;
            FOV = fov;

            float fovRads = 1.0f / MathAddons.Tanf(fov * GameSettings.PI / 360.0f);
            //FEKIN INTS CAUSED LIKE 3 HOURS OF 'WTF IS GOING ON WH YWONT WORK >@(' fml
            // 16384 because why would anymore play at a higher res this 16k like cmon lol
            float aspect = ((float)ViewportHeight).Clamp(4, 16384) / ((float)ViewportWidth).Clamp(4, 16384);
            float distance = Near - Far;

            Projection.M[0] = fovRads * aspect;
            Projection.M[1] = 0.0f;
            Projection.M[2] = 0.0f;
            Projection.M[3] = 0.0f;

            Projection.M[4] = 0.0f;
            Projection.M[5] = fovRads;
            Projection.M[6] = 0.0f;
            Projection.M[7] = 0.0f;

            Projection.M[8] = 0.0f;
            Projection.M[9] = 0.0f;
            Projection.M[10] = (Near + Far) / distance;
            Projection.M[11] = (2 * Near * Far) / distance;

            Projection.M[12] = 0.0f;
            Projection.M[13] = 0.0f;
            Projection.M[14] = -1.0f;
            Projection.M[15] = 0.0f;
        }

        public Matrix4 InverseProjection()
        {
            Matrix4 inverse = Matrix4.Zero();
            float a = Projection.M[0];
            float b = Projection.M[5];
            float c = Projection.M[10];
            float d = Projection.M[11];
            float e = Projection.M[14];

            inverse.M[0] = 1.0f / a;
            inverse.M[5] = 1.0f / b;
            inverse.M[10] = 1.0f / c;
            inverse.M[11] = 1.0f / d;
            inverse.M[14] = -c / (d * e);

            return inverse;
        }

        /// <summary>
        /// Returns the camera's projected world view 
        /// </summary>
        /// <returns></returns>
        public Matrix4 Matrix()
        {
            return Projection * WorldView;
        }

        /// <summary>
        /// Roates the camera's world view
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotationX"></param>
        /// <param name="rotationY"></param>
        public void SetRotation(Vector3 position, float rotationX, float rotationY)
        {
            WorldView = Matrix4.RotationX(rotationX) * Matrix4.RotationY(rotationY) * Matrix4.Translation(-position);
        }

        public void UseViewport()
        {
            GL.Viewport(0, 0, ViewportWidth, ViewportHeight);
        }
    }
}
