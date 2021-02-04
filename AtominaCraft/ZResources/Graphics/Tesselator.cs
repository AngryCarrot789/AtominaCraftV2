using AtominaCraft.BlockGrid;
using AtominaCraft.Blocks;
using AtominaCraft.Worlds;
using AtominaCraft.Worlds.Chunks;
using AtominaCraft.Worlds.Chunks.Rendering;
using AtominaCraft.ZResources.Maths;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AtominaCraft.ZResources.Graphics
{
    public class Tesselator
    {
        public static void DrawChunkMesh(ChunkMesh mesh)
        {

        }

        public struct Face
        {
            public Vector3 V1, V2, V3;
            public Face(Vector3 v1, Vector3 v2, Vector3 v3)
            {
                V1 = v1;
                V2 = v2;
                V3 = v3;
            }
        }

        // V/T V/T V/T
        // 2/1    3/2     1/3
        // 4/4    7/5     3/2
        // 8/6    5/7     7/5
        // 6/8    1/9     5/7
        // 7/5    1/10    3/11
        // 4/12   6/8     8/6
        // 2/1    4/4     3/2
        // 4/4    8/6     7/5 
        // 8/6    6/8     5/7
        // 6/8    2/13    1/9
        // 7/5    5/7     1/10
        // 4/12   2/14    6/8


        // -2.0f,  0.0f                1
        // -1.0f,  1.0f                2
        // -2.0f,  1.0f                3
        // -1.0f,  0.0f                4
        //  0.0f,  1.0f                5
        //  0.0f,  0.0f                6
        //  1.0f,  1.0f                7
        //  1.0f,  0.0f                8
        //  2.0f,  1.0f                9
        //  1.0f,  2.0f                10
        //  0.0f,  2.0f                11
        //  0.0f, -1.0f                12
        //  2.0f,  0.0f                13
        //  1.0f, -1.0f                14

        // -1.0f, -1.0f,  1.0f               v1
        // -1.0f,  1.0f,  1.0f               v2
        // -1.0f, -1.0f, -1.0f               v3
        // -1.0f,  1.0f, -1.0f               v4
        //  1.0f, -1.0f,  1.0f               v5
        //  1.0f,  1.0f,  1.0f               v6
        //  1.0f, -1.0f, -1.0f               v7
        //  1.0f,  1.0f, -1.0f               v8

        /*  
         *  v2, v3, v1        
         *  v4, v7, v3        
         *  v8, v5, v7        
         *  v6, v1, v5        
         *  v7, v1, v3        
         *  v4, v6, v8        
         *  v2, v4, v3        
         *  v4, v8, v7        
         *  v8, v6, v5        
         *  v6, v2, v1        
         *  v7, v5, v1        
         *  v4, v2, v6        
         */

        private static float[] TopVertices;
        private static float[] FrontVertices;
        private static float[] BottomVertices;
        private static float[] LeftVertices;
        private static float[] BackVertices;
        private static float[] RightVertices;

        public static List<Face> Faces { get; set; }

        public static void Generate()
        {
            Vector3 v1 = new Vector3(-1.0f, -1.0f, 1.0f);
            Vector3 v2 = new Vector3(-1.0f, 1.0f, 1.0f);
            Vector3 v3 = new Vector3(-1.0f, -1.0f, -1.0f);
            Vector3 v4 = new Vector3(-1.0f, 1.0f, -1.0f);
            Vector3 v5 = new Vector3(1.0f, -1.0f, 1.0f);
            Vector3 v6 = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 v7 = new Vector3(1.0f, -1.0f, -1.0f);
            Vector3 v8 = new Vector3(1.0f, 1.0f, -1.0f);

            Faces = new List<Face>()
            {
                // left
                new Face(v2, v3, v1),
                new Face(v4, v7, v3),

                new Face(v8, v5, v7),
                new Face(v6, v1, v5),

                new Face(v7, v1, v3),
                new Face(v4, v6, v8),

                new Face(v2, v4, v3),
                new Face(v4, v8, v7),

                new Face(v8, v6, v5),
                new Face(v6, v2, v1),

                new Face(v7, v5, v1),
                new Face(v4, v2, v6)
            };

            TopVertices = new float[]
            {
                -1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f, -1.0f,
            };

            BottomVertices = new float[]
            {
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
            };

            FrontVertices = new float[]
            {
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
            };

            RightVertices = new float[]
            {
                 1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
            };

            BackVertices = new float[]
            {
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,
            };

            LeftVertices = new float[]
            {
                -1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
            };

            //BlockVertices = new float[]
            //{
            //    0.0f,  0.0f,  0.0f,
            //    1.0f,  0.0f,  0.0f,
            //    1.0f,  1.0f,  0.0f,
            //    0.0f,  1.0f,  0.0f,
            //    0.0f,  0.0f,  1.0f,
            //    1.0f,  0.0f,  1.0f,
            //    1.0f,  1.0f,  1.0f,
            //    0.0f,  1.0f,  1.0f,
            //};
            //
            //VoxelTriangles = new float[][]
            //{
            //    new float[] { 3, 7, 2, 2, 7, 6 }, // top face
            //};
            //
            //BlockUVs = new float[]
            //{
            //    -1.0f, 0.0f,
            //     0.0f, 0.0f,
            //     0.0f, 1.0f,
            //    -1.0f, 1.0f,
            //
            //    -1.0f, 0.0f,
            //     0.0f, 0.0f,
            //     0.0f, 1.0f,
            //    -1.0f, 1.0f,
            //
            //    -1.0f, 0.0f,
            //     0.0f, 0.0f,
            //     0.0f, 1.0f,
            //    -1.0f, 1.0f,
            //
            //    -1.0f, 0.0f,
            //     0.0f, 0.0f,
            //     0.0f, 1.0f,
            //    -1.0f, 1.0f,
            //
            //    -1.0f, 0.0f,
            //     0.0f, 0.0f,
            //     0.0f, 1.0f,
            //    -1.0f, 1.0f,
            //
            //    -1.0f, 0.0f,
            //     0.0f, 0.0f,
            //     0.0f, 1.0f,
            //    -1.0f, 1.0f,
            //};
        }

        public static List<float> GetVisibleVertices(bool top, bool front, bool bottom, bool left, bool back, bool right)
        {
            int count =
                (top ? 18 : 0) +
                (front ? 18 : 0) +
                (bottom ? 18 : 0) +
                (left ? 18 : 0) +
                (back ? 18 : 0) +
                (right ? 18 : 0);
            List<float> vertices = new List<float>(count);

            if (top)
                vertices.AddRange(TopVertices);
            if (front)
                vertices.AddRange(FrontVertices);
            if (bottom)
                vertices.AddRange(BottomVertices);
            if (left)
                vertices.AddRange(LeftVertices);
            if (back)
                vertices.AddRange(BackVertices);
            if (right)
                vertices.AddRange(RightVertices);

            return vertices;
        }

        public static List<BlockDirection> GetVisibleFaces(Block block)
        {
            List<BlockDirection> visible = new List<BlockDirection>();
            World world = block.World;
            Chunk chunk = block.Location.Chunk;
            BlockLocation location = block.Location;

            Block left = chunk.GetBlockAt(location.X - 1, location.Y,     location.Z    );
            Block righ = chunk.GetBlockAt(location.X + 1, location.Y,     location.Z    );
            Block topp = chunk.GetBlockAt(location.X    , location.Y + 1, location.Z    );
            Block botm = chunk.GetBlockAt(location.X    , location.Y - 1, location.Z    );
            Block back = chunk.GetBlockAt(location.X    , location.Y,     location.Z + 1);
            Block frnt = chunk.GetBlockAt(location.X    , location.Y,     location.Z - 1);

            if (left == null || left.IsEmpty() || left.IsTransparent) visible.Add(BlockDirection.West);
            if (righ == null || righ.IsEmpty() || righ.IsTransparent) visible.Add(BlockDirection.East);
            if (topp == null || topp.IsEmpty() || topp.IsTransparent) visible.Add(BlockDirection.Up);
            if (botm == null || botm.IsEmpty() || botm.IsTransparent) visible.Add(BlockDirection.Down);
            if (back == null || back.IsEmpty() || back.IsTransparent) visible.Add(BlockDirection.South);
            if (frnt == null || frnt.IsEmpty() || frnt.IsTransparent) visible.Add(BlockDirection.North);

            return visible;
        }

        //public static List<float> GetCulled(HashSet<BlockDirection> directionsVisible)
        //{
        //    List<float> culled = new List<float>();
        //    foreach(BlockDirection direction in directionsVisible)
        //    {
        //        switch (direction)
        //        {
        //            case BlockDirection.Up:    culled.AddRange(TopVertices);    break;
        //            case BlockDirection.Down:  culled.AddRange(BottomVertices); break;
        //            case BlockDirection.North: culled.AddRange(BackVertices);   break;
        //            case BlockDirection.South: culled.AddRange(FrontVertices);  break;
        //            case BlockDirection.East:  culled.AddRange(RightVertices);  break;
        //            case BlockDirection.West:  culled.AddRange(LeftVertices);   break;
        //        }
        //    }
        //    return culled;
        //}

        //public static float[] GetUVs()
        //{
        //    return BlockUVs;
        //}
    }
}
