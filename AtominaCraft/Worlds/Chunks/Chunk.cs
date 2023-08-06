using System;
using System.Collections.Generic;
using AtominaCraft.Blocks;
using AtominaCraft.Utils;

namespace AtominaCraft.Worlds.Chunks {
    public class Chunk {
        public ChunkSection[] sections;

        public bool HaltMeshRegeneration;

        public readonly ChunkLocation coords;
        public readonly World world;

        public Chunk(World world, ChunkLocation coords) {
            this.sections = new ChunkSection[16];
            this.coords = coords;
            this.world = world;
        }

        public void Update() {

        }

        public int GetBlockAt(in BlockChunkCoord pos) {
            return GetBlockAt(pos.x, pos.y, pos.z);
        }

        public int GetBlockAt(int x, int y, int z) {
            return this.sections[y >> 4]?.GetBlockId(y & 15, x, z) ?? 0;
        }

        public void SetBlockAt(in BlockChunkCoord pos, int id) {
            SetBlockAt(pos.x, pos.y, pos.z, id);
        }

        public void SetBlockAt(int x, int y, int z, int id) {
            (this.sections[y >> 4] ?? (this.sections[y >> 4] = new ChunkSection(y >> 4, false))).SetBlockId(y & 15, x & 15, z & 15, id);
        }

        /// <summary>
        ///     Checks if a block at the given location exists
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool DoesBlockExist(int x, int y, int z) {
            ChunkSection section = this.sections[y >> 4];
            return section != null && !section.isEmpty && section.DoesBlockExist(y & 15, x, z);
        }

        /// <summary>
        ///     Removes the block at the given location from the chunk (if it exists in there... that is)
        /// </summary>
        /// <param name="location"></param>
        public void BreakBlockNaturally(int x, int y, int z) {
            if (DoesBlockExist(x, y, z)) {
                SetBlockAt(x, y, z, 0);
            }
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.world.GetHashCode(), this.coords.GetHashCode());
        }

        public override bool Equals(object obj) {
            return obj == this || obj is Chunk chunk && chunk.coords.Equals(this.coords);
        }

        public IEnumerable<BlockCoordDataPair> GetBlocks() {
            for (int i = 0; i < 16; i++) {
                ChunkSection section = this.sections[i];
                if (section != null) {
                    foreach (BlockCoordDataPair pair in section.GetBlocks()) {
                        yield return pair;
                    }
                }
            }
        }

        public override string ToString() {
            return $"Chunk({this.coords.x}, {this.coords.z})";
        }
    }
}