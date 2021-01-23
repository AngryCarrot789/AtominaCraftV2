using AtominaCraft.ZResources;
using AtominaCraft.ZResources.Graphics;
using OpenTK.Graphics.ES11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtominaCraft.Blocks.Rendering
{
    public static class BlockTextureLinker
    {
        public static Dictionary<string, Texture> TextureMap { get; set; }

        public static string GetTextureNameFromID(int id)
        {
            switch (id)
            {
                case (int)TextureTypes.Air: return "white";
                case (int)TextureTypes.Gold: return "gold";
                case (int)TextureTypes.Electromagnet: return "electromagnet";
                case (int)TextureTypes.Dirt: return "dirt";
            }

            return "";
        }

        public static void LoadTextures()
        {
            TextureMap = new Dictionary<string, Texture>();

            foreach (string textureFile in Directory.GetFiles(ResourceLocator.GetTexturesDirectory()))
            {
                TextureMap.Add(Path.GetFileNameWithoutExtension(textureFile), new Texture(textureFile, 0, 0));
            }
        }
    }
}
