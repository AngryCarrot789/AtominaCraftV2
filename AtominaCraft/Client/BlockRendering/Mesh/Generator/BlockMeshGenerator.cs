using System.Collections.Generic;
using System.Linq;
using AtominaCraft.BlockGrid;
using AtominaCraft.Utils;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;

namespace AtominaCraft.Client.BlockRendering.Mesh.Generator {
    public static class BlockMeshGenerator {
        public static readonly CubeMesh MESH_TOPP = GenerateMesh(new BlockFaceVisibility(0, true));
        public static readonly CubeMesh MESH_BOTM = GenerateMesh(new BlockFaceVisibility(0, bottom: true));
        public static readonly CubeMesh MESH_LEFT = GenerateMesh(new BlockFaceVisibility(0, left: true));
        public static readonly CubeMesh MESH_RIGT = GenerateMesh(new BlockFaceVisibility(0, right: true));
        public static readonly CubeMesh MESH_FRNT = GenerateMesh(new BlockFaceVisibility(0, front: true));
        public static readonly CubeMesh MESH_BACK = GenerateMesh(new BlockFaceVisibility(0, back: true));

        public static CubeMesh GenerateMesh(BlockFaceVisibility visibility) {
            List<float> vertices = new List<float>(108);
            List<float> uvs = new List<float>(108);
            List<float> normals = new List<float>(108);
            BlockMeshBuilder meshBuilder = GenerateBlock(visibility);
            meshBuilder.WriteVertices(vertices);
            meshBuilder.WriteUVs(uvs);
            meshBuilder.WriteNormals(normals, vertices);
            return new CubeMesh(vertices, uvs, normals);
        }

        public static BlockMeshBuilder GenerateBlock(BlockFaceVisibility visibility) {
            return BlockMeshBuilder.GenerateBlockMesh(
                visibility.Top,
                visibility.Front,
                visibility.Left,
                visibility.Bottom,
                visibility.Right,
                visibility.Back);
        }

        public static BlockFaceVisibility GetVisibleFaces(World world, BlockWorldCoord pos) {
            BlockFaceVisibility visibility = new BlockFaceVisibility();
            int left = world.GetBlockIdAt(pos.x - 1, pos.y, pos.z);
            int righ = world.GetBlockIdAt(pos.x + 1, pos.y, pos.z);
            int topp = ((pos.y == 0) || (pos.y == 255)) ? 0 : world.GetBlockIdAt(pos.x, pos.y + 1, pos.z);
            int botm = ((pos.y == 0) || (pos.y == 255)) ? 0 : world.GetBlockIdAt(pos.x, pos.y - 1, pos.z);
            int back = world.GetBlockIdAt(pos.x, pos.y, pos.z - 1);
            int frnt = world.GetBlockIdAt(pos.x, pos.y, pos.z + 1);
            visibility.Left = left == 0;
            visibility.Right = righ == 0;
            visibility.Top = topp == 0;
            visibility.Bottom = botm == 0;
            visibility.Back = back == 0;
            visibility.Front = frnt == 0;
            return visibility;
        }

        public static BlockObject GenerateChunk() {

        }

        public struct BlockFaceVisibility {
            public bool Top, Front, Left, Bottom, Right, Back;

            public BlockFaceVisibility(int ignore, bool top = false, bool front = false, bool left = false, bool bottom = false, bool right = false, bool back = false) {
                this.Top = top;
                this.Front = front;
                this.Left = left;
                this.Bottom = bottom;
                this.Right = right;
                this.Back = back;
            }
        }
    }
}