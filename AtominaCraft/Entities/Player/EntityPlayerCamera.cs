using AtominaCraft.ZResources;
using OpenTK.Windowing.GraphicsLibraryFramework;
using REghZy.MathsF;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace AtominaCraft.Entities.Player {
    public class EntityPlayerCamera : EntityPlayer {
        public float camRotX;
        public float camRotY;

        public Vector2 LastMousePosition;

        public EntityPlayerCamera() {
            this.Camera = new PlayerCamera();
            this.BoundingBox.SetFromCenter(this.Position, new Vector3f(1.0f, 2.0f, 1.0f));
            this.BoundingBox.Move(0, -0.5f, 0);
        }

        public PlayerCamera Camera { get; }

        public Matrix4 WorldToCamera() {
            return Matrix4.RotX(-this.camRotX) * Matrix4.RotY(-this.camRotY) * Matrix4.WorldToLocal(this.Position, this.Rotation, this.Scale);
        }

        public Matrix4 CameraToWorld() {
            return Matrix4.LocalToWorld(this.Position, this.Rotation, this.Scale) * Matrix4.RotY(this.camRotY) * Matrix4.RotX(this.camRotX);
        }

        public override void Update() {
            Vector2 difference = Inputs.Mouse.Position - this.LastMousePosition;
            Look(difference.X, difference.Y);

            float moveB = 0.0f, moveR = 0.0f, moveU = 0.0f;
            if (Inputs.Keyboard.IsKeyDown(Keys.W))
                moveB -= 1.0f;
            if (Inputs.Keyboard.IsKeyDown(Keys.S))
                moveB += 1.0f;
            if (Inputs.Keyboard.IsKeyDown(Keys.A))
                moveR -= 1.0f;
            if (Inputs.Keyboard.IsKeyDown(Keys.D))
                moveR += 1.0f;
            if (Inputs.Keyboard.IsKeyDown(Keys.Space))
                moveU += 1.0f;
            if (Inputs.Mouse.IsButtonDown(MouseButton.Button5))
                moveU -= 1.0f;

            // if (moveB != 0 || moveR != 0 || moveU != 0) {
            //     List<AxisAlignedBB> boundingBoxes = this.World.GetBoundingBoxesBetween(this.BoundingBox.Copy().ExpandFromCenter(1.5f, 1.5f));
            //     if (moveB != 0)
            //         foreach (AxisAlignedBB aabb in boundingBoxes)
            //             moveB = aabb.CalculateOffsetZ(this.BoundingBox, moveB);
            //     if (moveR != 0)
            //         foreach (AxisAlignedBB aabb in boundingBoxes)
            //             moveR = aabb.CalculateOffsetX(this.BoundingBox, moveR);
            //     if (moveU != 0)
            //         foreach (AxisAlignedBB aabb in boundingBoxes)
            //             moveU = aabb.CalculateOffsetY(this.BoundingBox, moveU);
            // }

            Move(moveB, moveR, moveU);

            this.LastMousePosition = Inputs.Mouse.Position;
            base.Update();
        }

        public void Move(float back, float right, float up) {
            Matrix4 camToWorld = Matrix4.LocalToWorld(this.Position, this.Rotation, this.Scale) * Matrix4.RotY(this.camRotY);
            Vector3f lookDirection = camToWorld.MultiplyDirection(new Vector3f(right, up, back)).Normalise();
            Vector3f movement = lookDirection.GetNonNAN() * (GameSettings.DEFAULT_WALK_ACCELERATION * Delta.Time);
            this.Velocity += movement;
            //AccelerateTowards(movement);
        }

        public void Look(float x, float y) {
            this.camRotX -= y * GameSettings.MOUSE_SENSITIVITY;
            if (this.camRotX > GameSettings.PI_HALF)
                this.camRotX = GameSettings.PI_HALF;
            else if (this.camRotX < GameSettings.PI_NEGATIVE / 2)
                this.camRotX = GameSettings.PI_NEGATIVE / 2;

            this.camRotY -= x * GameSettings.MOUSE_SENSITIVITY;
            if (this.camRotY > GameSettings.PI)
                this.camRotY -= GameSettings.PI_DOUBLE;
            else if (this.camRotY < GameSettings.PI_NEGATIVE)
                this.camRotY += GameSettings.PI_DOUBLE;
        }
    }
}