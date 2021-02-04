using AtominaCraft.Collision;
using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Maths;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace AtominaCraft.Entities.Player
{
    public class EntityPlayerCamera : EntityPlayer
    {
        public PlayerCamera Camera { get; private set; }

        public float CameraRotationX { get; set; }
        public float CameraRotationY { get; set; }

        public OpenTK.Mathematics.Vector2 LastMousePosition { get; set; }

        public EntityPlayerCamera()
        {
            Camera = new PlayerCamera();
            BoundingBox.SetFromCenter(Position, new Vector3(1.0f, 2.0f, 1.0f));
            BoundingBox.Move(0, -0.5f, 0);
        }

        public Matrix4 WorldToCamera()
        {
            return
                Matrix4.RotationX(-CameraRotationX) *
                Matrix4.RotationY(-CameraRotationY) *
                Matrix4.CreateWorldToLocal(Position, Rotation, Scale);

        }

        public Matrix4 CameraToWorld()
        {
            return Matrix4.CreateLocalToWorld(Position, Rotation, Scale) *
                Matrix4.RotationY(CameraRotationY) *
                Matrix4.RotationX(CameraRotationX);

        }

        public override void Update()
        {
            OpenTK.Mathematics.Vector2 difference = Inputs.Mouse.Position - LastMousePosition;
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


            Move(moveB, moveR, moveU);
            
            LastMousePosition = Inputs.Mouse.Position;
            base.Update();
        }

        public void Move(float back, float right, float up)
        {
            float aa = back;
            Matrix4 camToWorld = Matrix4.CreateLocalToWorld(Position, Rotation, Scale) * Matrix4.RotationY(CameraRotationY);
            Vector3 lookDirection = camToWorld.MultiplyDirection(new Vector3(right, up, back)).Normalised();
            lookDirection.EnsureNormal();
            Vector3 movement = lookDirection * GameSettings.DEFAULT_WALK_ACCELERATION;
            AccelerateTowards(movement);
        }

        public void Look(float x, float y)
        {
            CameraRotationX -= y * GameSettings.MOUSE_SENSITIVITY;
            if (CameraRotationX > GameSettings.PI_HALF)
                CameraRotationX = GameSettings.PI_HALF;
            else if (CameraRotationX < GameSettings.PI_NEGATIVE / 2)
                CameraRotationX = GameSettings.PI_NEGATIVE / 2;

            CameraRotationY -= x * GameSettings.MOUSE_SENSITIVITY;
            if (CameraRotationY > GameSettings.PI)
                CameraRotationY -= GameSettings.PI_DOUBLE;
            else if (CameraRotationY < GameSettings.PI_NEGATIVE)
                CameraRotationY += GameSettings.PI_DOUBLE;
        }
    }
}
