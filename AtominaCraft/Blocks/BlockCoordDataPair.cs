using AtominaCraft.Utils;

namespace AtominaCraft.Blocks {
    public readonly struct BlockCoordDataPair {
        public readonly BlockChunkCoord ChunkCoord;
        public readonly Block block;

        public BlockCoordDataPair(BlockChunkCoord chunkCoord, Block block) {
            this.ChunkCoord = chunkCoord;
            this.block = block;
        }
    }
}