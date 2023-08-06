using System;
using System.IO;

namespace AtominaCraft.ZResources {
    public static class ResourceLocator {
        public const string GRAPHICS_BASE_PATH = "ZResources\\GFX";
        public const string SHADERS_BASE_PATH = GRAPHICS_BASE_PATH + "\\Shaders";
        public const string MESHES_BASE_PATH = GRAPHICS_BASE_PATH + "\\Meshes";
        public const string TEXTURES_BASE_PATH = GRAPHICS_BASE_PATH + "\\Textures";
        private static string APP_DIRECTORY = "";

        /// <summary>
        ///     Returns the main directory containing all the app data and files.
        /// </summary>
        /// <param name="maximumBackwards">
        ///     Maximum number of times the function can "go back in history", in terms of the file parents and stuff
        /// </param>
        /// <returns></returns>
        public static string GetApplicationDirectory(int maximumBackwards = 5) {
            if (!Directory.Exists(APP_DIRECTORY)) {
                string launchPath = Environment.CurrentDirectory;
                const string ignorableFile = "zzignore_sz47.txt";
                string directory = launchPath;

                bool Search(string path) {
                    foreach (string file in Directory.GetFiles(path))
                        if (Path.GetFileName(file) == ignorableFile)
                            return true;

                    return false;
                }

                for (int i = 0; i < maximumBackwards; i++) {
                    bool hasFound = Search(directory);
                    if (!hasFound)
                        directory = Directory.GetParent(directory).FullName;

                    if (hasFound) {
                        APP_DIRECTORY = directory;
                        return APP_DIRECTORY;
                    }
                }

                return "";
            }

            return APP_DIRECTORY;
        }

        /// <summary>
        ///     Returns the shader directory
        /// </summary>
        /// <returns></returns>
        public static string GetShadersDirectory() {
            string appFolder = GetApplicationDirectory();
            return Path.Combine(appFolder, SHADERS_BASE_PATH);
        }

        /// <summary>
        ///     Returns the mesh directory
        /// </summary>
        /// <returns></returns>
        public static string GetMeshesDirectory() {
            string appFolder = GetApplicationDirectory();
            return Path.Combine(appFolder, MESHES_BASE_PATH);
        }

        /// <summary>
        ///     Returns the textures directory
        /// </summary>
        /// <returns></returns>
        public static string GetTexturesDirectory() {
            string appFolder = GetApplicationDirectory();
            return Path.Combine(appFolder, TEXTURES_BASE_PATH);
        }

        /// <summary>
        ///     Locates a shader file in the shader directory
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string LocateShader(string fileName) {
            string shadersPath = GetShadersDirectory();
            string shaderFilePath = Path.Combine(shadersPath, fileName);
            return shaderFilePath ?? (File.Exists(shaderFilePath) ? shaderFilePath : string.Empty);
        }

        // dont need anymore xd
        // just putting the meshes in code cus why not
        ///// <summary>
        ///// Locates a shader file in the shader directory
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public static string LocateMesh(string fileName)
        //{
        //    string meshesPath = GetMeshesDirectory();
        //    string meshesFilePath = Path.Combine(meshesPath, fileName);
        //    return meshesFilePath ?? (File.Exists(meshesFilePath) ? meshesFilePath : string.Empty);
        //}

        /// <summary>
        ///     Locates a texture file in the texture directory
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string LocateTexture(string fileName) {
            return Path.Combine(GetTexturesDirectory(), fileName);
        }
    }
}