using AtominaCraft.Collision;
using AtominaCraft.Resources.Maths;
using AtominaCraft.Worlds;

namespace AtominaCraft.Entities
{
    public class Entity
    {
        public int ID { get; set; }

        public bool CanEntitySpawn { get; set; }

        public World World { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 PreviousPosition { get; set; }
        public Vector3 LastTickPosition { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public bool IsCollidedX { get; set; }
        public bool IsCollidedY { get; set; }
        public bool IsCollidedZ { get; set; }
        public AxisAlignedBB BoundingBox { get; set; }
        public bool IsGrounded { get; set; }
        public float DistanceFallen { get; set; }
        public bool IsNoClip { get; set; }

        public bool IsRemoved { get; set; }

        public Entity()
        {
            Position = new Vector3(0, 0, 0);
            PreviousPosition = new Vector3();
            LastTickPosition = new Vector3();
            Velocity = new Vector3();
            Rotation = new Vector3();
            Scale = new Vector3(1, 1, 1);
            BoundingBox = new AxisAlignedBB();
        }

        public virtual void Update()
        {
            PreviousPosition.Set(Position);
            Velocity *= 0.98f;
            Position += Velocity * Delta.Time;
        }
    }
}
