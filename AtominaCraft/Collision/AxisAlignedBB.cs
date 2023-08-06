using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.GFX;
using REghZy.MathsF;

namespace AtominaCraft.Collision {
    /// <summary>
    ///     An Axis-Aligned bounding box, used for specifying a region in world space
    ///     that's aligned to the X/Y/Z axis (no rotation).
    ///     <para>
    ///         Will only really be used for entity-on-block or entity-on-entity collisions, not block-on-block
    ///     </para>
    /// </summary>
    public struct AxisAlignedBB {
        public float maxX;
        public float maxY;
        public float maxZ;
        public float minX;
        public float minY;
        public float minZ;

        public AxisAlignedBB(float minX, float minY, float minZ, float maxX, float maxY, float maxZ) {
            this.maxX = maxX;
            this.maxY = maxY;
            this.maxZ = maxZ;
            this.minX = minX;
            this.minY = minY;
            this.minZ = minZ;
        }

        public AxisAlignedBB(Location block) {
            this.maxX = block.x;
            this.maxY = block.y;
            this.maxZ = block.z;
            this.minX = block.x + GridLatch.BlockWidth;
            this.minY = block.y + GridLatch.BlockWidth;
            this.minZ = block.z + GridLatch.BlockWidth;
        }

        public void Set(float minX, float minY, float minZ, float maxX, float maxY, float maxZ) {
            this.minX = minX;
            this.minY = minY;
            this.minZ = minZ;
            this.maxX = maxX;
            this.maxY = maxY;
            this.maxZ = maxZ;
        }

        // scale as in, scales from the center. scale of 1 means the total length == 2
        public void SetFromCenter(Vector3f center, Vector3f scale) {
            Set(center.x - scale.x / 2,
                center.y - scale.y / 2,
                center.z - scale.z / 2,
                center.x + scale.x / 2,
                center.y + scale.y / 2,
                center.z + scale.z / 2);
        }

        public void Reset() {
            Set(0, 0, 0, 0, 0, 0);
        }

        public Vector3f GetCenter() {
            float scaleX = (this.maxX - this.minX) / 2;
            float scaleY = (this.maxY - this.minY) / 2;
            float scaleZ = (this.maxZ - this.minZ) / 2;
            return new Vector3f(this.maxX - scaleX, this.maxY - scaleY, this.maxZ - scaleZ);
        }

        public Vector3f GetMinimum() {
            return new Vector3f(this.minX, this.minY, this.minZ);
        }

        public Vector3f GetMaximum() {
            return new Vector3f(this.maxX, this.maxY, this.maxZ);
        }

        public Vector3f GetScale() {
            return new Vector3f(
                (this.maxX - this.minX) / 2,
                (this.maxY - this.minY) / 2,
                (this.maxZ - this.minZ) / 2);
        }

        public void Draw(PlayerCamera camera) {
            DebugDraw.DrawCube(camera, GetCenter(), GetScale(), 1, 0.4f, 0.2f);
        }

        /// <summary>
        ///     Moves the AABB by the given amounts in the X Y and Z axis
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Move(float x, float y, float z) {
            Set(this.minX + x, this.minY + y, this.minZ + z, this.maxX + x, this.maxY + y, this.maxZ + z);
        }

        public AxisAlignedBB ExpandFromCenter(float min, float max) {
            this.minX -= min;
            this.minY -= min;
            this.minZ -= min;
            this.maxX += max;
            this.maxY += max;
            this.maxZ += max;
            return this;
        }

        public AxisAlignedBB Expand(float minX, float minY, float minZ, float maxX, float maxY, float maxZ) {
            this.minX += minX;
            this.minY += minY;
            this.minZ += minZ;
            this.maxX -= maxX;
            this.maxY -= maxY;
            this.maxZ -= maxZ;
            return this;
        }

        public AxisAlignedBB Shrink(float minX, float minY, float minZ, float maxX, float maxY, float maxZ) {
            this.minX -= minX;
            this.minY -= minY;
            this.minZ -= minZ;
            this.maxX += maxX;
            this.maxY += maxY;
            this.maxZ += maxZ;
            return this;
        }

        public AxisAlignedBB Offset(float x, float y, float z) {
            this.minX += x;
            this.maxX += x;
            this.minY += y;
            this.maxY += y;
            this.minZ += z;
            this.maxZ += z;
            return this;
        }

        public static bool IsPointInsideAABB(Vector3f p, AxisAlignedBB b) {
            return
                p.x >= b.minX && p.x <= b.maxX &&
                p.y >= b.minY && p.y <= b.maxY &&
                p.z >= b.minZ && p.z <= b.maxZ;
        }

        public float CalculateOffsetX(AxisAlignedBB aabb, float moveOffsetX) {
            if (aabb.maxY > this.minY && aabb.minY < this.maxY) {
                if (aabb.maxZ > this.minZ && aabb.minZ < this.maxZ) {
                    float move;

                    if (moveOffsetX > 0.0D && aabb.maxX <= this.minX) {
                        move = this.minX - aabb.maxX;

                        if (move < moveOffsetX)
                            moveOffsetX = move;
                    }

                    if (moveOffsetX < 0.0D && aabb.minX >= this.maxX) {
                        move = this.maxX - aabb.minX;

                        if (move > moveOffsetX)
                            moveOffsetX = move;
                    }

                    return moveOffsetX;
                }

                return moveOffsetX;
            }

            return moveOffsetX;
        }

        public float CalculateOffsetY(AxisAlignedBB aabb, float moveOffsetY) {
            if (aabb.maxX > this.minX && aabb.minX < this.maxX) {
                if (aabb.maxZ > this.minZ && aabb.minZ < this.maxZ) {
                    float move;

                    if (moveOffsetY > 0.0D && aabb.maxY <= this.minY) {
                        move = this.minY - aabb.maxY;

                        if (move < moveOffsetY)
                            moveOffsetY = move;
                    }

                    if (moveOffsetY < 0.0D && aabb.minY >= this.maxY) {
                        move = this.maxY - aabb.minY;

                        if (move > moveOffsetY)
                            moveOffsetY = move;
                    }

                    return moveOffsetY;
                }

                return moveOffsetY;
            }

            return moveOffsetY;
        }

        public float CalculateOffsetZ(AxisAlignedBB aabb, float moveOffsetZ) {
            if (aabb.maxX > this.minX && aabb.minX < this.maxX) {
                if (aabb.maxY > this.minY && aabb.minY < this.maxY) {
                    float move;

                    if (moveOffsetZ > 0.0D && aabb.maxZ <= this.minZ) {
                        move = this.minZ - aabb.maxZ;

                        if (move < moveOffsetZ)
                            moveOffsetZ = move;
                    }

                    if (moveOffsetZ < 0.0D && aabb.minZ >= this.maxZ) {
                        move = this.maxZ - aabb.minZ;

                        if (move > moveOffsetZ)
                            moveOffsetZ = move;
                    }

                    return moveOffsetZ;
                }

                return moveOffsetZ;
            }

            return moveOffsetZ;
        }

        public bool IntersectsAABBX(AxisAlignedBB aabb) {
            return this.minX <= aabb.maxX && this.maxX >= aabb.minX;
        }

        public bool IntersectsAABBY(AxisAlignedBB aabb) {
            return this.minY <= aabb.maxY && this.maxY >= aabb.minY;
        }

        public bool IntersectsAABBZ(AxisAlignedBB aabb) {
            return this.minZ <= aabb.maxZ && this.maxZ >= aabb.minZ;
        }

        public bool IntersectsAABB(AxisAlignedBB b) {
            return IntersectsAABBX(b) && IntersectsAABBY(b) && IntersectsAABBZ(b);
        }

        public float IntersectionAmountX(AxisAlignedBB aabb, bool checkIntersection) {
            if (checkIntersection)
                if (!IntersectsAABBY(aabb) && !IntersectsAABBZ(aabb))
                    return 0;
            if (this.minX < aabb.maxX && aabb.minX < this.minX)
                return aabb.maxX - this.minX;
            if (this.maxX > aabb.minX && aabb.minX > this.minX)
                return this.maxX - aabb.minX;
            return 0;
        }

        public float IntersectionAmountY(AxisAlignedBB aabb, bool checkIntersection) {
            if (checkIntersection)
                if (!IntersectsAABBX(aabb) && !IntersectsAABBZ(aabb))
                    return 0;
            if (this.minY < aabb.maxY && aabb.minY < this.minY)
                return aabb.maxY - this.minY;
            if (this.maxY > aabb.minY && aabb.minY > this.minY)
                return this.maxY - aabb.minY;
            return 0;
        }

        public float IntersectionAmountZ(AxisAlignedBB aabb, bool checkIntersection) {
            if (checkIntersection)
                if (!IntersectsAABBX(aabb) && !IntersectsAABBY(aabb))
                    return 0;
            if (this.minZ < aabb.maxZ && aabb.minZ < this.minZ)
                return aabb.maxZ - this.minZ;
            if (this.maxZ > aabb.minZ && aabb.minZ > this.minZ)
                return this.maxZ - aabb.minZ;
            return 0;
        }

        public AxisAlignedBB Copy() {
            return new AxisAlignedBB(this.minX, this.minY, this.minZ, this.maxX, this.maxY, this.maxZ);
        }
    }
}