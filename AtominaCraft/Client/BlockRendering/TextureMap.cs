using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Graphics;
using System.Collections.Generic;
using System.IO;

namespace AtominaCraft.Client.BlockRendering
{
    public static class TextureMap
    {
        private static Dictionary<string, Texture> Map;

        private static bool IsInitialised;
        public static void Initialise()
        {
            if (!IsInitialised)
            {
                LoadTextures();

                IsInitialised = true;
            }
        }

        public static string GetTextureNameFromID(int id)
        {
            switch (id)
            {
                case 1: return "dirt";
                case 2: return "gold";
                case 3: return "white";
                case 4: return "electromagnet";
            }
            return "";
        }

        public static Texture GetTexture(string textureName)
        {
            if (Map.TryGetValue(textureName, out Texture texture))
                return texture;
            else
                return Map["white"];
        }

        public static Texture GetTextureFromID(int id)
        {
            if (Map.TryGetValue(GetTextureNameFromID(id), out Texture texture))
                return texture;
            else
                return null;
        }

        private static void LoadTextures()
        {
            Map = new Dictionary<string, Texture>();

            foreach (string textureFile in Directory.GetFiles(ResourceLocator.GetTexturesDirectory()))
            {
                Map.Add(Path.GetFileNameWithoutExtension(textureFile), new Texture(textureFile, 0, 0));
            }
        }
    }
}
