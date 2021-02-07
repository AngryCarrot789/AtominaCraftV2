using AtominaCraft.Collision;
using AtominaCraft.ZResources.Maths;
using AtominaCraft.Worlds;
using System.ComponentModel.DataAnnotations;
using AtominaCraft.Worlds.Chunks;
using System;
using AtominaCraft.BlockGrid;

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

        public float Height { get; set; }
        public float Width { get; set; }
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
            Height = Scale.Y;
            Width = Scale.X;
            BoundingBox = new AxisAlignedBB();
            BoundingBox.SetFromCenter(Position, Scale);
            BoundingBox.Offset(0, (-Height) / 8, 0);
        }

        public virtual void Update()
        {
            PreviousPosition = Position;
            Velocity *= (1.0f - 0.05f);
            Position += (Velocity * Delta.Time);
            UpdateAABBPosition();

            Chunk = World.GetChunkAt(GridLatch.MTWGetChunk(Position));
        }

        public void UpdateAABBPosition()
        {
            float differenceX = Position.X - PreviousPosition.X;
            float differenceY = Position.Y - PreviousPosition.Y;
            float differenceZ = Position.Z - PreviousPosition.Z;
            BoundingBox.Move(differenceX, differenceY, differenceZ);
        }

        public void MoveTo(Vector3 newPosition)
        {
            PreviousPosition = Position;
            Position = newPosition;
            UpdateAABBPosition();
        }

        public void MoveTowards(Vector3 position)
        {
            MoveTo(position + position);
        }

        public void AccelerateTowards(Vector3 position)
        {
            Velocity += position * Delta.Time;
        }
    }
}
