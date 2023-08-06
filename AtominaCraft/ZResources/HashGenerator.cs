namespace AtominaCraft.ZResources {
    public static class HashGenerator {
        public static int GenerateHashXZ(int x, int z) {
            return (x & int.MaxValue) | ((z & int.MaxValue) << 31);
        }

        public static int GenerateHashXYZ(int x, int y, int z) {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
    }
}