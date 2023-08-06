namespace AtominaCraft.Utils {
    public static class LongHash {
        /// <summary>
        /// Joins the MSW and LSW into 1
        /// </summary>
        /// <param name="msw"></param>
        /// <param name="lsw"></param>
        /// <returns></returns>
        public static long ToLong(int msw, int lsw) {
            return ((long)msw << 32) + (long)lsw - -2147483648L;
        }

        /// <summary>
        /// Most significant word; high 4 bytes
        /// </summary>
        public static int MSW(long l) {
            return (int)(l >> 32);
        }

        /// <summary>
        /// Least significant word; lower 4 bytes
        /// </summary>
        public static int LSW(long l) {
            return (int)l + -2147483648;
        }
    }
}