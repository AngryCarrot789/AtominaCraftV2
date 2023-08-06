using AtominaCraft.ZResources;
using REghzy.MathsF;
using OpenTK.Graphics.OpenGL;
using REghZy.MathsF;

namespace AtominaCraft.Entities.Player {
    /// <summary>
    ///     A class for holding information for the main camera
    /// </summary>
    public class PlayerCamera {
        /// <summary>
        ///     Represents the matrix used for projecting a view matrix
        /// </summary>
        public Matrix4 projection;

        /// <summary>
        ///     The camera's world view matrix,
        /// </summary>
        public Matrix4 worldView;

        public PlayerCamera() {
            this.ViewportWidth = 256;
            this.ViewportHeight = 256;
            this.worldView = Matrix4.Identity();
            this.projection = Matrix4.Identity();
        }

        public int ViewportWidth { get; private set; }
        public int ViewportHeight { get; private set; }
        public float Near { get; private set; }
        public float Far { get; private set; }
        public float FOV { get; private set; }

        public void SetSize(int width, int height, float near, float far, float fov = GameSettings.RENDER_FOV) {
            this.ViewportWidth = width;
            this.ViewportHeight = height;
            this.Near = near;
            this.Far = far;
            this.FOV = fov;

            float fovRads = 1.0f / (fov * GameSettings.PI / 360.0f).Tanf();
            // FEKIN INTS CAUSED LIKE 3 HOURS OF 'WTF IS GOING ON WH YWONT WORK >@(' fml
            // 16384 because why would anymore play at a higher res this 16k like cmon lol
            float aspect = ((float) this.ViewportHeight).Clamp(4, 16384) / ((float) this.ViewportWidth).Clamp(4, 16384);
            float distance = this.Near - this.Far;

            this.projection[0] = fovRads * aspect;
            this.projection[1] = 0.0f;
            this.projection[2] = 0.0f;
            this.projection[3] = 0.0f;

            this.projection[4] = 0.0f;
            this.projection[5] = fovRads;
            this.projection[6] = 0.0f;
            this.projection[7] = 0.0f;

            this.projection[8] = 0.0f;
            this.projection[9] = 0.0f;
            this.projection[10] = (this.Near + this.Far) / distance;
            this.projection[11] = 2 * this.Near * this.Far / distance;

            this.projection[12] = 0.0f;
            this.projection[13] = 0.0f;
            this.projection[14] = -1.0f;
            this.projection[15] = 0.0f;
        }

        public Matrix4 InverseProjection() {
            Matrix4 inverse = Matrix4.Zero();
            float a = this.projection[0];
            float b = this.projection[5];
            float c = this.projection[10];
            float d = this.projection[11];
            float e = this.projection[14];

            inverse[0] = 1.0f / a;
            inverse[5] = 1.0f / b;
            inverse[10] = 1.0f / c;
            inverse[11] = 1.0f / d;
            inverse[14] = -c / (d * e);

            return inverse;
        }

        /// <summary>
        ///     Returns the camera's projected world view
        /// </summary>
        /// <returns></returns>
        public Matrix4 Matrix() {
            return this.projection * this.worldView;
        }

        /// <summary>
        ///     Roates the camera's world view
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotationX"></param>
        /// <param name="rotationY"></param>
        public void SetRotation(Vector3f position, float rotationX, float rotationY) {
            this.worldView = Matrix4.RotX(rotationX) * Matrix4.RotY(rotationY) * Matrix4.Translation(-position);
        }

        public void UseViewport() {
            GL.Viewport(0, 0, this.ViewportWidth, this.ViewportHeight);
        }
    }
}