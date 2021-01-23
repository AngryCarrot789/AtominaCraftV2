using System.Collections.Generic;

namespace AtominaCraft.Blocks.Rendering
{
    public static class BlockMesh
    {
        public const int FacesCount = 6;
        public const int TrianglesPerFace = 2;
        public const int VerticesPerFace = TrianglesPerFace * 9;
        public const int VerticesCount = VerticesPerFace * FacesCount;

        private static float[] BlockVertices;
        private static float[] BlockUVs;
        // Normal facing to the sky
        private static float[] TopVertices;
        // Normal facing to the ground
        private static float[] BottomVertices;
        // Normal facing to (1, 0, 0)
        private static float[] FrontVertices;
        // Normal facing to (1, 0, 0)
        private static float[] RightVertices;
        // Normal facing to (0, 0, -1)
        private static float[] BackVertices;
        // Normal facing to (-1, 0, 0)
        private static float[] LeftVertices;
        // ... i think

        public static void Generate()
        {
            TopVertices = new float[VerticesPerFace]
            {
                -1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f, -1.0f,
            };

            BottomVertices = new float[VerticesPerFace]
            {
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
            };
            
            FrontVertices = new float[VerticesPerFace]
            {
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
            };
            
            RightVertices = new float[VerticesPerFace]
            {
                 1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
            };
            
            BackVertices = new float[VerticesPerFace]
            {
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,
            };
            
            LeftVertices = new float[VerticesPerFace]
            {
                -1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
            };

            BlockVertices = new float[VerticesCount]
            {
                // Front face
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
            
                // Top Face
                -1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f, -1.0f,
                
                // Right Face
                 1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                
                // Bottom Face
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
                
                // Back Face
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,
                
                // Left Face
                -1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
            };

            BlockUVs = new float[]
            {
                -1.0f, 0.0f,
                 0.0f, 0.0f,
                 0.0f, 1.0f,
                -1.0f, 1.0f,

                -1.0f, 0.0f,
                 0.0f, 0.0f,
                 0.0f, 1.0f,
                -1.0f, 1.0f,

                -1.0f, 0.0f,
                 0.0f, 0.0f,
                 0.0f, 1.0f,
                -1.0f, 1.0f,

                -1.0f, 0.0f,
                 0.0f, 0.0f,
                 0.0f, 1.0f,
                -1.0f, 1.0f,

                -1.0f, 0.0f,
                 0.0f, 0.0f,
                 0.0f, 1.0f,
                -1.0f, 1.0f,

                -1.0f, 0.0f,
                 0.0f, 0.0f,
                 0.0f, 1.0f,
                -1.0f, 1.0f,
            };
        }

        public static List<float> GetCulled(HashSet<BlockDirection> directionsVisible)
        {
            List<float> culled = new List<float>();
            foreach(BlockDirection direction in directionsVisible)
            {
                switch (direction)
                {
                    case BlockDirection.Up:    culled.AddRange(TopVertices);    break;
                    case BlockDirection.Down:  culled.AddRange(BottomVertices); break;
                    case BlockDirection.North: culled.AddRange(BackVertices);   break;
                    case BlockDirection.South: culled.AddRange(FrontVertices);  break;
                    case BlockDirection.East:  culled.AddRange(RightVertices);  break;
                    case BlockDirection.West:  culled.AddRange(LeftVertices);   break;
                }
            }
            return culled;
        }

        public static float[] GetUVs()
        {
            return BlockUVs;
        }
    }
}
