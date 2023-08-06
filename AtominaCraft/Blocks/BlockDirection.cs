namespace AtominaCraft.Blocks {
    public enum BlockDirection {
        /// <summary>
        ///     Not placed
        /// </summary>
        None = -1,

        /// <summary>
        ///     Facing (0,0,-1)
        /// </summary>
        North = 0,

        /// <summary>
        ///     Facing (1,0,0)
        /// </summary>
        East = 1,

        /// <summary>
        ///     Facing (0,0,1)
        /// </summary>
        South = 2,

        /// <summary>
        ///     Facing (-1,0,0)
        /// </summary>
        West = 4,

        /// <summary>
        ///     Facing (0,0,-1)
        /// </summary>
        Up = 8,

        /// <summary>
        ///     Facing (0,0,-1)
        /// </summary>
        Down = 16,

        _Left = West,
        _Right = East,
        _Forward = North,
        _Backward = South
    }

    public static class BlockDirectionExtensions {
        public static float GetHorizontalAxisDegrees(this BlockDirection direction) {
            switch (direction) {
                case BlockDirection.North: return 0;
                case BlockDirection.East: return 90;
                case BlockDirection.South: return 180;
                case BlockDirection.West: return 270;
                default: return -1;
            }
        }

        public static float GetVerticalAxisDegrees(this BlockDirection direction) {
            switch (direction) {
                case BlockDirection.Up: return 90;
                case BlockDirection.Down: return 270;
                default: return -1;
            }
        }
    }
}