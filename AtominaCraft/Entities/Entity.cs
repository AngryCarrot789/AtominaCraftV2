using AtominaCraft.Collision;
using AtominaCraft.ZResources.Maths;
using AtominaCraft.Worlds;
using System.ComponentModel.DataAnnotations;
using AtominaCraft.Worlds.Chunks;
using System;

namespace AtominaCraft.Entities
{
    public class Entity
    {
        public int ID { get; set; }

        public bool CanEntitySpawn { get; set; }

        public World World { get; set; }
        public Chunk Chunk { get; set; }
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
            PreviousPosition = new Vector3();
            Position = new Vector3(0, 0, 0);
            LastTickPosition = new Vector3();
            Velocity = new Vector3();
            Rotation = new Vector3();
            Scale = new Vector3(1, 1, 1);
            BoundingBox = new AxisAlignedBB(0, 0, 0, 0, 0, 0);
        }

        public virtual void Update()
        {
            Velocity *= 0.98f;
            MoveTo(Position + (Velocity * Delta.Time));
            Chunk = World.GetChunkAt((int)Math.Floor(Position.X) >> 4, (int)Math.Floor(Position.Z) >> 4);
        }

        public void UpdateAABBPosition()
        {
            float differenceX = Position.X - PreviousPosition.X;
            float differenceY = Position.Y - PreviousPosition.Y;
            float differenceZ = Position.Z - PreviousPosition.Z;
            BoundingBox.Move(differenceX, differenceY, differenceZ);
        }

        public void MoveTo(Vector3 newPos)
        {
            PreviousPosition.Set(Position);
            Position = newPos;
            UpdateAABBPosition();
        }
    }
}
