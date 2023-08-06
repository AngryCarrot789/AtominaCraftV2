using System;
using System.Collections.Generic;
using AtominaCraft.ZResources;
using REghZy.MathsF;

namespace REghzy.MathsF {
    /// <summary>
    ///     A class containing many extension methods and extension helpers
    /// </summary>
    public static class MathAddons {
        public const float PI = 3.1415926535897931f;

        /// <summary>
        ///     Clamps a given value between 2 other values.
        ///     so Clamp(25, 0, 100) will always be between 0 and 100, never below or above
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(this float value, float min, float max) {
            // return value < min ? min : (value > max ? max : value);

            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static int Clamp(this int value, int min, int max) {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static Vector3f Clamp(this Vector3f vec, float min, float max) {
            return new Vector3f(
                Clamp(vec.x, min, max),
                Clamp(vec.y, min, max),
                Clamp(vec.z, min, max));
        }

        public static void ClampInstance(this Vector3f vector, float min, float max) {
            vector.x = Clamp(vector.x, min, max);
            vector.y = Clamp(vector.y, min, max);
            vector.z = Clamp(vector.z, min, max);
        }

        /// <summary>
        ///     Looks at both value1 and value2 and returns the smallest one. so Min(4, 6) returns 4
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Min(this float value1, float value2) {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        ///     Looks at both value1 and value2 and returns the biggest one. so Max(4, 6) returns 6
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Max(this float value1, float value2) {
            return value1 > value2 ? value1 : value2;
        }

        public static float Min3(float a, float b, float c) {
            float n = Min(a, b);
            return Min(n, c);
        }

        public static float Max3(float a, float b, float c) {
            float n = Max(a, b);
            return Max(n, c);
        }

        public static float Sinf(this float a) {
            return MathF.Sin(a);
        }

        public static float Cosf(this float a) {
            return MathF.Cos(a);
        }

        public static float ACosf(this float a) {
            return MathF.Acos(a);
        }

        public static float Tanf(this float a) {
            return MathF.Tan(a);
        }

        public static float Sqrtf(this float a) {
            return MathF.Sqrt(a);
        }

        /// <summary>
        ///     Returns a vector which has been moved by the matrix
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3f MultiplyPoint(this Matrix4 a, Vector3f b) {
            Vector3f p = new Vector3f(
                a[0] * b.x + a[1] * b.y + a[2] * b.z + a[3],
                a[4] * b.x + a[5] * b.y + a[6] * b.z + a[7],
                a[8] * b.x + a[9] * b.y + a[10] * b.z + a[11]);
            float w = a[12] * b.x + a[13] * b.y + a[14] * b.z + a[15];
            return p / w;
        }

        /// <summary>
        ///     Returns a vector which shows the direction of the matrix (euler)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3f MultiplyDirection(this Matrix4 a, Vector3f b) {
            return new Vector3f(
                a[0] * b.x + a[1] * b.y + a[2] * b.z,
                a[4] * b.x + a[5] * b.y + a[6] * b.z,
                a[8] * b.x + a[9] * b.y + a[10] * b.z);
        }

        public static Vector3f GetVertexFromList(this List<float> list, int startPoint) {
            if (list.Count >= 3 && startPoint < list.Count)
                return new Vector3f(
                    list[startPoint],
                    list[startPoint + 1],
                    list[startPoint + 2]);
            return new Vector3f(0, 0, 0);
        }

        public static Vector3f GetVertexFromList(this List<float> list, int offset, int startPoint) {
            if (list.Count >= 3 && startPoint < list.Count)
                return new Vector3f(
                    list[startPoint + offset * 3],
                    list[startPoint + offset * 3 + 1],
                    list[startPoint + offset * 3 + 2]);
            return new Vector3f(0, 0, 0);
        }

        public static Vector3f DegreesToEuler(float x, float y, float z) {
            return new Vector3f(
                x / GameSettings.DEG_TO_RAD_CONST,
                y / GameSettings.DEG_TO_RAD_CONST,
                z / GameSettings.DEG_TO_RAD_CONST);
        }

        public static float DegreesToEuler(float a) {
            return a / GameSettings.DEG_TO_RAD_CONST;
        }


        public static Vector3f DegreesToEuler(this Vector3f degrees) {
            return new Vector3f(
                degrees.x / GameSettings.DEG_TO_RAD_CONST,
                degrees.y / GameSettings.DEG_TO_RAD_CONST,
                degrees.z / GameSettings.DEG_TO_RAD_CONST);
        }

        public static int ForceParse(this string integer) {
            return int.TryParse(integer, out int value) ? value : 0;
        }

        public static float ForceParsef(this string val) {
            return float.TryParse(val, out float value) ? value : 0.0f;
        }

        public static float[] ToFloats(this string[] array) {
            float[] floats = new float[array.Length];
            for (int i = 0; i < array.Length; i++)
                floats[i] = array[i].ForceParsef();
            return floats;
        }

        public static int[] ToInts(this string[] array) {
            int[] ints = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
                ints[i] = array[i].ForceParse();
            return ints;
        }

        public static int ToInt(this float value) {
            return ForceParse(value.ToString());
        }

        /// <summary>
        ///     Creates a vector from an array of floats
        /// </summary>
        /// <param name="floats"></param>
        /// <param name="setVecAllWith1Element">
        ///     If the array's length is 1, then if this is true, the vector's X, Y and Z values
        ///     are set to that one element (in the array). otherwise only the X component is set
        /// </param>
        /// <returns></returns>
        public static Vector3f ToVector(this float[] floats, bool setVecAllWith1Element = true) {
            if (floats == null)
                return Vector3f.Zero;
            if (floats.Length >= 3)
                return new Vector3f(floats[0], floats[1], floats[2]);
            if (floats.Length == 2)
                return new Vector3f(floats[0], floats[1], 0);
            if (floats.Length == 1)
                return setVecAllWith1Element ? new Vector3f(floats[0]) : new Vector3f(floats[0], 0, 0);
            return Vector3f.Zero;
        }

        public static float MakeNegative(this float value) {
            return value < 0 ? value : -value;
        }

        public static float MakePositive(this float value) {
            return value < 0 ? -value : value;
        }

        public static float GetMinimumCorner(this float center, float scale) {
            return center - scale / 2;
        }

        public static float GetMaximumCorner(this float center, float scale) {
            return center + scale / 2;
        }

        public static Vector3f GetLineFromMinimumCorner(this Vector3f corner, float scale) {
            return new Vector3f(
                GetMinimumCorner(corner.x, scale),
                GetMinimumCorner(corner.y, scale),
                GetMinimumCorner(corner.z, scale)
            );
        }
    }
}