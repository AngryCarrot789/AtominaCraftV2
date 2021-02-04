using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;

namespace AtominaCraft.Collision
{
    /// <summary>
    ///     An Axis-Aligned bounding box, used for specifying a region in world space 
    ///     that's aligned to the X/Y/Z axis (no rotation). 
    /// <para>
    ///     Will only really be used for entity-on-block or entity-on-entity collisions, not block-on-block
    /// </para>
    /// </summary>
    public class AxisAlignedBB
    {
        public float MinX { get; set; }
        public float MinY { get; set; }
        public float MinZ { get; set; }
        public float MaxX { get; set; }
        public float MaxY { get; set; }
        public float MaxZ { get; set; }

        public AxisAlignedBB() { }

        public AxisAlignedBB(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            Set(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public AxisAlignedBB(BlockLocation block)
        {
            Set(block.X,
                block.Y,
                block.Z,
                block.X + GridLatch.BLOCK_SIZE,
                block.Y + GridLatch.BLOCK_SIZE,
                block.Z + GridLatch.BLOCK_SIZE);
        }

        public void Set(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }

        // scale as in, scales from the center. scale of 1 means the total length == 2
        public void SetFromCenter(Vector3 center, Vector3 scale)
        {
            Set(center.X - (scale.X / 2),
                center.Y - (scale.Y / 2),
                center.Z - (scale.Z / 2),
                center.X + (scale.X / 2),
                center.Y + (scale.Y / 2),
                center.Z + (scale.Z / 2));
        }

        public void Reset()
        {
            Set(0, 0, 0, 0, 0, 0);
        }

        public Vector3 GetCenter()
        {
            float scaleX = (MaxX - MinX) / 2;
            float scaleY = (MaxY - MinY) / 2;
            float scaleZ = (MaxZ - MinZ) / 2;
            return new Vector3(MaxX - scaleX, MaxY - scaleY, MaxZ - scaleZ);
        }

        public Vector3 GetMinimum()
        {
            return new Vector3(MinX, MinY, MinZ);
        }

        public Vector3 GetMaximum()
        {
            return new Vector3(MaxX, MaxY, MaxZ);
        }

        public Vector3 GetScale()
        {
            return new Vector3(
                (MaxX - MinX) / 2,
                (MaxY - MinY) / 2,
                (MaxZ - MinZ) / 2);
        }

        public void Draw(PlayerCamera camera)
        {
            DebugDraw.DrawCube(camera, GetCenter(), GetScale(), 1, 0.4f, 0.2f);
        }

        /// <summary>
        /// Moves the AABB by the given amounts in the X Y and Z axis
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Move(float x, float y, float z)
        {
            Set(MinX + x, MinY + y, MinZ + z, MaxX + x, MaxY + y, MaxZ + z);
        }

        public void ExpandFromCenter(float min, float max)
        {
            MinX -= min;
            MinY -= min;
            MinZ -= min;
            MaxX += max;
            MaxY += max;
            MaxZ += max;
        }

        public void Expand(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX += minX;
            MinY += minY;
            MinZ += minZ;
            MaxX -= maxX;
            MaxY -= maxY;
            MaxZ -= maxZ;
        }

        public void Shrink(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX -= minX;
            MinY -= minY;
            MinZ -= minZ;
            MaxX += maxX;
            MaxY += maxY;
            MaxZ += maxZ;
        }

        public void Offset(float x, float y, float z)
        {
            MinX += x;
            MaxX += x;
            MinY += y;
            MaxY += y;
            MinZ += z;
            MaxZ += z;
        }

        public static bool IsPointInsideAABB(Vector3 p, AxisAlignedBB b)
        {
            return
                p.X >= b.MinX && p.X <= b.MaxX &&
                p.Y >= b.MinY && p.Y <= b.MaxY &&
                p.Z >= b.MinZ && p.Z <= b.MaxZ;
        }

        public bool IntersectsAABBX(AxisAlignedBB aabb)
        {
            return MinX <= aabb.MaxX && MaxX >= aabb.MinX;
        }

        public bool IntersectsAABBY(AxisAlignedBB aabb)
        {
            return MinY <= aabb.MaxY && MaxY >= aabb.MinY;
        }

        public bool IntersectsAABBZ(AxisAlignedBB aabb)
        {
            return MinZ <= aabb.MaxZ && MaxZ >= aabb.MinZ;
        }

        public bool IntersectsAABB(AxisAlignedBB b)
        {
            return IntersectsAABBX(b) && IntersectsAABBY(b) && IntersectsAABBZ(b);
        }

        public float IntersectionAmountX(AxisAlignedBB aabb, bool checkIntersection)
        {
            if (checkIntersection)
            {
                if (!IntersectsAABBY(aabb) && !IntersectsAABBZ(aabb))
                {
                    return 0;
                }
            }
            if (MinX < aabb.MaxX && aabb.MinX < MinX)
            {
                return aabb.MaxX - MinX;
            }
            if (MaxX > aabb.MinX && aabb.MinX > MinX)
            {
                return MaxX - aabb.MinX;
            }
            return 0;
        }

        public float IntersectionAmountY(AxisAlignedBB aabb, bool checkIntersection)
        {
            if (checkIntersection)
            {
                if (!IntersectsAABBX(aabb) && !IntersectsAABBZ(aabb))
                {
                    return 0;
                }
            }
            if (MinY < aabb.MaxY && aabb.MinY < MinY)
            {
                return aabb.MaxY - MinY;
            }
            if (MaxY > aabb.MinY && aabb.MinY > MinY)
            {
                return MaxY - aabb.MinY;
            }
            return 0;
        }

        public float IntersectionAmountZ(AxisAlignedBB aabb, bool checkIntersection)
        {
            if (checkIntersection)
            {
                if (!IntersectsAABBX(aabb) && !IntersectsAABBY(aabb))
                {
                    return 0;
                }
            }
            if (MinZ < aabb.MaxZ && aabb.MinZ < MinZ)
            {
                return aabb.MaxZ - MinZ;
            }
            if (MaxZ > aabb.MinZ && aabb.MinZ > MinZ)
            {
                return MaxZ - aabb.MinZ;
            }
            return 0;
        }
    }
}