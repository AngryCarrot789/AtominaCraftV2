using AtominaCraft.Worlds;

namespace AtominaCraft.Blocks {
    public struct LocatableBlockCoord {
        public World World;
        public int x;
        public int y;
        public int z;

        public LocatableBlockCoord(World world, int x, int y, int z) {
            this.World = world;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public LocatableBlockCoord(int x, int y, int z) {
            this.World = null;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}