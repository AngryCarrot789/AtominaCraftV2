using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks.Rendering;
using AtominaCraft.Entities.Player;
using AtominaCraft.ZResources.Graphics;
using AtominaCraft.ZResources.Maths;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace AtominaCraft.Blocks
{
    public static class BlockRenderer
    {
        private static GameObject Cube { get; set; }

        static BlockRenderer()
        {
            Cube = new GameObject();
            Cube.Shader = BlockTextureLinker.TextureShader;
            Cube.Mesh = BlockTextureLinker.Cube;
        }

        public static void DrawBlock(Block block, PlayerCamera camera)
        {
            string textureName = BlockTextureLinker.GetTextureNameFromID(block.ID);

            BlockTextureLinker.TextureMap.TryGetValue(textureName, out Texture texture);
            Cube.Texture = texture;
            Cube.Position = GridLatch.GetBlockWorldSpace(block.Location);
            Cube.Draw(camera);
        }
    }
}
