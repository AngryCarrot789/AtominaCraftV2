using System;
using AtominaCraft.BlockGrid;
using AtominaCraft.Collision;
using AtominaCraft.Utils;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;
using REghZy.MathsF;

namespace AtominaCraft.Entities {
    public class Entity {
        public AxisAlignedBB BoundingBox;

        public bool CanEntitySpawn;
        public Chunk Chunk;
        public float DistanceFallen;

        public float Height;
        public int ID;
        public bool IsCollidedX;
        public bool IsCollidedY;
        public bool IsCollidedZ;
        public bool IsGrounded;
        public bool IsNoClip;

        public bool IsRemoved;
        public Vector3f LastTickPosition;
        public Vector3f Position;
        public Vector3f PreviousPosition;
        public Vector3f Rotation;
        public Vector3f Scale;
        public Vector3f Velocity;

        public float Width;

        public World World;

        public BlockWorldCoord BlockPositon {
            get => new BlockWorldCoord((int) Math.Floor(this.Position.x), (int) Math.Floor(this.Position.z), (int) Math.Floor(this.Position.z));
            set => this.Position = new Vector3f(value.x, value.y, value.z);
        }

        public Entity() {
            this.PreviousPosition = new Vector3f(0.0f);
            this.Position = new Vector3f(0, 0, 0);
            this.LastTickPosition = new Vector3f(0.0f);
            this.Velocity = new Vector3f(0.0f);
            this.Rotation = new Vector3f(0.0f);
            this.Scale = new Vector3f(1.0f);
            this.Height = this.Scale.y;
            this.Width = this.Scale.x;
            this.BoundingBox = new AxisAlignedBB();
            this.BoundingBox.SetFromCenter(this.Position, this.Scale);
            this.BoundingBox.Offset(0, -this.Height / 8, 0);
        }

        public virtual void Update() {
            this.PreviousPosition = this.Position;
            this.Velocity *= 1.0f - 0.05f;
            this.Position += this.Velocity * Delta.Time;
            UpdateAABBPosition();

            this.Chunk = this.World.GetChunkAt(GridLatch.MTWGetChunk(this.Position));
        }

        public void UpdateAABBPosition() {
            float differenceX = this.Position.x - this.PreviousPosition.x;
            float differenceY = this.Position.y - this.PreviousPosition.y;
            float differenceZ = this.Position.z - this.PreviousPosition.z;
            this.BoundingBox.Move(differenceX, differenceY, differenceZ);
        }

        public void MoveTo(Vector3f newPosition) {
            this.PreviousPosition = this.Position;
            this.Position = newPosition;
            UpdateAABBPosition();
        }

        public void AccelerateTowards(Vector3f position) {
            this.Velocity += position * Delta.Time;
        }
    }
}