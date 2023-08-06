using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Client.BlockRendering;
using AtominaCraft.Client.BlockRendering.Mesh;
using AtominaCraft.Client.BlockRendering.Mesh.Generator;
using AtominaCraft.Entities.Player;
using AtominaCraft.Utils;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;
using OpenTK.Windowing.Common.Input;
using REghZy.MathsF;
using Image = System.Drawing.Image;

namespace AtominaCraft.ZResources.GFX {
    public static class Tesselator {
        public static readonly BlockObject OBJ;

        static Tesselator() {
            // TextureAtlas atlas = new TextureAtlas();
            // atlas.SetBlock(Bitmap.FromFile(ResourceLocator.LocateTexture("dirt.png")), 0, 0);
            // atlas.SetBlock(Bitmap.FromFile(ResourceLocator.LocateTexture("electromagnet.png")), 1, 0);
            // atlas.SetBlock(Bitmap.FromFile(ResourceLocator.LocateTexture("gold.bmp")), 2, 0);
            // atlas.SetBlock(Bitmap.FromFile(ResourceLocator.LocateTexture("white.bmp")), 3, 0);
            OBJ = new BlockObject {Shader = GraphicsLoader.TextureShader};
        }

        public static void DrawBlock(PlayerCamera camera, Chunk chunk, BlockChunkCoord pos, int id) {
            Block block = Block.Blocks[id];
            World world = chunk.world;
            BlockWorldCoord worldCoord = pos.ToWorldCoord(chunk);
            if (block != null && block.CanRender(chunk.world, worldCoord)) {
                if (pos.y == 255 || !block.IsObscured(world, worldCoord, Direction.UP)) {
                    DrawBlockMesh(camera, block, BlockMeshGenerator.MESH_TOPP, chunk, pos);
                }

                if (pos.y == 0 || !block.IsObscured(world, worldCoord, Direction.DOWN)) {
                    DrawBlockMesh(camera, block, BlockMeshGenerator.MESH_BOTM, chunk, pos);
                }

                if (!block.IsObscured(world, worldCoord, Direction.WEST)) {
                    DrawBlockMesh(camera, block, BlockMeshGenerator.MESH_LEFT, chunk, pos);
                }

                if (!block.IsObscured(world, worldCoord, Direction.EAST)) {
                    DrawBlockMesh(camera, block, BlockMeshGenerator.MESH_RIGT, chunk, pos);
                }

                if (!block.IsObscured(world, worldCoord, Direction.NORTH)) {
                    DrawBlockMesh(camera, block, BlockMeshGenerator.MESH_BACK, chunk, pos);
                }

                if (!block.IsObscured(world, worldCoord, Direction.SOUTH)) {
                    DrawBlockMesh(camera, block, BlockMeshGenerator.MESH_FRNT, chunk, pos);
                }
            }
        }

        public static void DrawBlockMesh(PlayerCamera camera, Block block, CubeMesh mesh, Chunk chunk, in BlockChunkCoord chunkCoord) {
            string textureName = TextureMap.GetTextureNameFromID(block.id);
            if (textureName == "")
                return;

            OBJ.Mesh = mesh;
            OBJ.Texture = TextureMap.GetTexture(textureName);
            OBJ.Position = GridLatch.WTMGetWorldBlock(chunk.coords, chunkCoord);
            // OBJ.Position = new Vector3f(chunkCoord.ToWorldCoord(chunk).ToRender());
            OBJ.Draw(camera);
        }

        public static void DrawChunkBlocks(PlayerCamera camera, Chunk chunk) {
            ChunkSection[] sections = chunk.sections;
            for (int i = 0; i < 16; i++) {
                ChunkSection section = sections[i];
                if (section != null && !section.isEmpty) {
                    for (int j = 0; j < 16; j++) {
                        int[] layer = section.idSection[j];
                        if (layer != null) {
                            DrawChunkLayer(camera, chunk, layer, i, j);
                        }
                    }
                }
            }
        }

        public static void DrawChunkLayer(PlayerCamera camera, Chunk chunk, int[] layer, int layerIndex, int y) {
            for (int i = 0; i < 256; i++) {
                int id = layer[i];
                if (id != 0) {
                    int x = i & 15;
                    int z = (i >> 4) & 15;
                    DrawBlock(camera, chunk, new BlockChunkCoord(x, (layerIndex << 4) + y, z), id);
                }
            }
        }
    }
}