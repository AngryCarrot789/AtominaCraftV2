using System;

namespace AtominaCraft.ZResources.Strings {
    public static class StringHelper {
        /// <summary>
        ///     An extension method of string.IsNullOrEmpty(), but shortened for ease
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value) {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Repeats a given string a set number of times and returns it
        /// </summary>
        /// <param name="text">The text to be repeated</param>
        /// <param name="numberOfTimes">The number of times to repeat the text</param>
        /// <returns></returns>
        public static string Repeat(this string text, int numberOfTimes) {
            string newText = "";
            for (int i = 0; i < numberOfTimes; i++)
                newText += text;
            return newText;
        }

        /// <summary>
        ///     Repeats a given character a set number of times and returns it as a string
        /// </summary>
        /// <param name="text">The text to be repeated</param>
        /// <param name="numberOfTimes">The number of times to repeat the text</param>
        /// <returns></returns>
        public static string Repeat(this char text, int numberOfTimes) {
            string newText = "";
            for (int i = 0; i < numberOfTimes; i++)
                newText += text;
            return newText;
        }

        /// <summary>
        ///     Forces a given string to be a certain length by filling it with excess whitespaces
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="maxLength">the length the text should be including whitespaces</param>
        /// <returns></returns>
        public static string EnsureLength(this string text, int maxLength) {
            int textLength = text.Length;
            int repeatLength = maxLength - textLength;
            if (repeatLength < 0)
                return text;

            return text + Repeat(' ', repeatLength);
        }

        /// <summary>
        ///     Same as <see cref="EnsureLength(string, int)" /> but can be used for floats for ease
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string EnsureLength(this float value, int maxLength) {
            return EnsureLength(MathF.Round(value, 5).ToString(), maxLength);
        }

        public static bool IsLongEnough(this string str, int minLength) {
            return str.Length >= minLength;
        }

        /// <summary>
        ///     Checks if the text in value at the specified index and length is equal to checkEqual
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="checkEqual"></param>
        /// <returns></returns>
        public static bool Check(this string value, int startIndex, int length, string checkEqual) {
            return value.IsLongEnough(startIndex + 1 + length) && value.Substring(startIndex, length) == checkEqual;
        }

        /// <summary>
        ///     Extracts text out of a string at the specified start index and length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Extract(this string value, int startIndex, int length = 0) {
            if (startIndex == -1 || length == -1)
                return "";
            if (value.IsLongEnough(startIndex + 1 + length)) {
                if (length == 0)
                    return value.Substring(startIndex);
                return value.Substring(startIndex, length);
            }

            if (value.IsLongEnough(startIndex + 1))
                return value.Substring(startIndex);
            return "";
        }

        public static int CountElements(this string value, char character) {
            int counts = 0;
            for (int i = 0; i < value.Length; i++)
                if (value[i] == character)
                    counts++;
            return counts;
        }
    }
}