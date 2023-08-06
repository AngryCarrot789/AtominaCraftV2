using System.Collections.Generic;
using AtominaCraft.Blocks;
using AtominaCraft.Utils;

namespace AtominaCraft.Worlds.Chunks {
    public class ChunkSection {
        public readonly int[][] idSection;
        public readonly int y;

        public bool isEmpty;

        public int[] this[int layer] {
            get => this[layer];
            set => this[layer] = value;
        }

        public ChunkSection(int y) {
            this.y = y;
            this.idSection = new int[16][];
            this.isEmpty = true;
        }

        public ChunkSection(int y, bool isEmpty) {
            this.y = y;
            this.idSection = new int[16][];
            this.isEmpty = isEmpty;
        }

        public int GetBlockId(int layer, int x, int z) {
            return this.idSection[layer]?[x + (z << 4)] ?? 0;
        }

        public void SetBlockId(int layer, int x, int z, int id) {
            (this.idSection[layer] ?? (this.idSection[layer] = new int[256]))[x + (z << 4)] = id;
        }

        public bool HasLayer(int layer) {
            return this.idSection[layer] != null;
        }

        public bool DoesBlockExist(int layer, int x, int z) {
            int[] arr = this.idSection[layer];
            if (arr == null) {
                return false;
            }

            return arr[x + (z << 4)] != 0;
        }

        public IEnumerable<BlockCoordDataPair> GetBlocks() {
            if (this.isEmpty) {
                yield break;
            }

            for (int i = 0; i < 16; i++) {
                int[] layer = this.idSection[i];
                if (layer != null) {
                    for (int j = 0; j < 256; j++) {
                        int id = layer[j];
                        if (id != 0) {
                            int x = j & 15;
                            int z = (j >> 4) & 15;
                            yield return new BlockCoordDataPair(new BlockChunkCoord(x, this.y + i, z), Block.Blocks[id]);
                        }
                    }
                }
            }
        }
    }
}