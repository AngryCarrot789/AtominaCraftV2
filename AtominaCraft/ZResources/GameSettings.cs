namespace AtominaCraft.ZResources {
    /// <summary>
    ///     A class containing the game's information, like name, width/height, etc.
    /// </summary>
    public static class GameSettings {
        // App/Version
        public const string APPLICATION_CLASS_NAME = GAME_NAME;
        public const string GAME_NAME = "AtominaCraft";
        public const string GAME_VERSION = "1.0.1.129";
        public const int VERSION_MAJOR = 1;
        public const int VERSION_MINOR = 0;
        public const int VERSION_REVISION = 1;
        public const int VERSION_BUILD = 129;

        // General
        public const float PI = 3.141592653589f;
        public const float PI_NEGATIVE = -3.141592653589f;
        public const float PI_DOUBLE = 6.283185307178f;
        public const float PI_DOUBLE_NEGATIVE = -6.283185307178f;
        public const float PI_HALF = 1.5707963267945f;
        public const float PI_HALF_NEGATIVE = -1.5707963267945f;
        public const float DEG_TO_RAD_CONST = 57.29577951309679f;
        public const string WINDOW_TITLE = GAME_NAME + " - " + GAME_VERSION;
        public const bool START_FULLSCREEN = false;
        public const bool HIDE_MOUSE = false;
        public const bool RENDER_USE_SKY = false;
        public const float RENDER_FOV = 75.0f;
        public const float RENDER_NEAR_MIN = 0.003f;
        public const float RENDER_NEAR_MAX = 0.1f;
        public const float RENDER_FAR = 5000.0f;
        public const int RENDER_FBO_SIZE = 2048;

        // Graphics
        public static int WINDOW_WIDTH = 1280;
        public static int WINDOW_HEIGHT = 720;
        public static int WINDOW_X_POS = 500;
        public static int WINDOW_Y_POS = 100;

        // Gameplay
        public static float MOUSE_SENSITIVITY = 0.0013f;
        public static float MOUSE_SMOOTHING = 0.1f;
        public static float DEFAULT_FLY_SPEED = 4.7f;
        public static float DEFAULT_WALK_SPEED = 2.6f;
        public static float DEFAULT_SPRINT_SPEED = 3.9f;
        public static float GRAVITY_Y = -9.8f;
        public static bool USE_BOBBING = false;
        public static bool CAN_CONFINE_CURSOR = true;

        /// <summary>
        ///     This is a high value just because it is due to how movement works :(
        /// </summary>
        public static float DEFAULT_WALK_ACCELERATION => 70.0f;
    }
}