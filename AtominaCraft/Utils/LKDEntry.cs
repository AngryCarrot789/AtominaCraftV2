namespace AtominaCraft.Utils {
    public readonly struct LKDEntry<V> {
        public readonly long key;
        public readonly V value;

        public LKDEntry(long key, V value) {
            this.key = key;
            this.value = value;
        }
    }
}