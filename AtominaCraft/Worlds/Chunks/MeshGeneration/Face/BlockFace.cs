using AtominaCraft.Worlds.Generator.Mesh;
using System.Collections.Generic;

namespace AtominaCraft.Worlds.Chunks.MeshGeneration.Face
{
    public class BlockFace
    {
        /*                          
         *            ignore this XDDDDDDDDDDDDDDDDD              
         *                               VT2
         *                               
         *                               
         *                               
         *                               
         *       
         *   0,0                            1,0         
         *          ____________________ VT3                 VT1            
         *         |                    |                     
         *         |                    |
         *         |                    | 
         *         |                    |
         *         |                    |
         *         |                    |
         *         |                    |         
         *         |____________________|                                                 
         *       VT5                     VT2                                         
         *   0,1                            1,1                                               
         *        
         *        
         *        
         *        
         *       VT4 
         *                                                      
         *                    
         *                +Y    
         *                 |     -Z
         *                 |    /
         *                 |  /  
         *       -X _______|/________ +X  
         *                /|   
         *              /  |   
         *            /    |    
         *          +Z    -Y
         *          
         *           V5 ____________________ V1                                       
         *             /|                  /|             
         *            / |             1   / |                      
         *           /  |                /  |                            
         *          /   | 2             /   |                            
         *      V7 /___________________/ V3 |                                              
         *         |                   |    |                            
         *         |    |           1  |    |                            
         *         | V6 | _ _ _ _ _ _  | _  | V2                             
         *         |    /              |    /                              
         *         |   /   1           |   /                               
         *         |  /                |  /                                
         *         | /  2          2   | /                                 
         *         |/__________________|/                                                     
         *        V8                   V4                                    
         *                               
         *        
         */



        public static Vertex3 V1 = new Vertex3( 1.0f,  1.0f, -1.0f);
        public static Vertex3 V2 = new Vertex3( 1.0f, -1.0f, -1.0f);
        public static Vertex3 V3 = new Vertex3( 1.0f,  1.0f,  1.0f);
        public static Vertex3 V4 = new Vertex3( 1.0f, -1.0f,  1.0f);
        public static Vertex3 V5 = new Vertex3(-1.0f,  1.0f, -1.0f);
        public static Vertex3 V6 = new Vertex3(-1.0f, -1.0f, -1.0f);
        public static Vertex3 V7 = new Vertex3(-1.0f,  1.0f,  1.0f);
        public static Vertex3 V8 = new Vertex3(-1.0f, -1.0f,  1.0f);

        public static Vertex2f VT1  = new Vertex2f( 2.0f,  0.0f);
        public static Vertex2f VT2  = new Vertex2f( 1.0f,  1.0f);
        public static Vertex2f VT3  = new Vertex2f( 1.0f,  0.0f);
        public static Vertex2f VT4  = new Vertex2f( 0.0f,  2.0f);
        public static Vertex2f VT5  = new Vertex2f( 0.0f,  1.0f);
        public static Vertex2f VT6  = new Vertex2f( 1.0f, -2.0f);
        public static Vertex2f VT7  = new Vertex2f( 0.0f, -1.0f);
        public static Vertex2f VT8  = new Vertex2f( 0.0f, -2.0f);
        public static Vertex2f VT9  = new Vertex2f( 0.0f,  0.0f);
        public static Vertex2f VT10 = new Vertex2f(-1.0f,  1.0f);
        public static Vertex2f VT11 = new Vertex2f(-1.0f,  0.0f);
        public static Vertex2f VT12 = new Vertex2f( 1.0f, -1.0f);
        public static Vertex2f VT13 = new Vertex2f( 2.0f,  1.0f);
        public static Vertex2f VT14 = new Vertex2f( 1.0f,  2.0f);

        // ---------------------------------------------------------------------------------------------|
        //                                                                                              |    
        // Triangle Faces. there's 12 on each cube, and 2 faces per cube face, and each face contains   |    
        //       3 vector3s making 9 floats, total of 18 verts per cube face, 108 total per cube        |
        //                                                                                              |
        // ---------------------------------------------------------------------------------------------|
        public static TriangleFace T1  = new TriangleFace(V5, V3, V1, VT1,  VT2,  VT3);  // Top    1    |
        public static TriangleFace T2  = new TriangleFace(V5, V7, V3, VT1,  VT13, VT2);  // Top    2    |
        // ---------------------------------------------------------------------------------------------|
        public static TriangleFace F1  = new TriangleFace(V3, V8, V4, VT2,  VT4,  VT5);  // Front  1    |
        public static TriangleFace F2  = new TriangleFace(V3, V7, V8, VT2,  VT14, VT4);  // Front  2    |
        // ---------------------------------------------------------------------------------------------|
        public static TriangleFace L1  = new TriangleFace(V7, V6, V8, VT6,  VT7,  VT8);  // Left   1    |
        public static TriangleFace L2  = new TriangleFace(V7, V5, V6, VT6,  VT12, VT7);  // Left   2    |
        // ---------------------------------------------------------------------------------------------|
        public static TriangleFace B1  = new TriangleFace(V2, V8, V6, VT9,  VT10, VT11); // Bottom 1    |
        public static TriangleFace B2  = new TriangleFace(V2, V4, V8, VT9,  VT5,  VT10); // Bottom 2    |
        // ---------------------------------------------------------------------------------------------|
        public static TriangleFace R1  = new TriangleFace(V1, V4, V2, VT3,  VT5,  VT9);  // Right  1    |
        public static TriangleFace R2  = new TriangleFace(V1, V3, V4, VT3,  VT2,  VT5);  // Right  2    |
        // ---------------------------------------------------------------------------------------------|
        public static TriangleFace BK1 = new TriangleFace(V5, V2, V6, VT12, VT9,  VT7);  // Back   1    |
        public static TriangleFace BK2 = new TriangleFace(V5, V1, V2, VT12, VT3,  VT9);  // Back   2    |
        // ---------------------------------------------------------------------------------------------|
        public TriangleFace Face1, Face2;

        private BlockFace(TriangleFace f1, TriangleFace f2)
        {
            Face1 = f1;
            Face2 = f2;
        }

        public void WriteVertices(List<float> vertices)
        {
            Face1.WriteVertices(vertices);
            Face2.WriteVertices(vertices);
        }

        public void WriteTextureCoordinates(List<float> uvs)
        {
            Face1.WriteTextureCoordinates(uvs);
            Face2.WriteTextureCoordinates(uvs);
        }

        public static BlockFace GenerateFace(FaceDirection direction)
        {
            switch (direction)
            {
                case FaceDirection.Front:
                    return new BlockFace(F1, F2);
                case FaceDirection.Back:
                    return new BlockFace(BK1, BK2);
                case FaceDirection.Top:
                    return new BlockFace(T1, T2);
                case FaceDirection.Bottom:
                    return new BlockFace(B1, B2);
                case FaceDirection.Left:
                    return new BlockFace(L1, L2);
                case FaceDirection.Right:
                    return new BlockFace(R1, R2);
                default:
                    return null;
            }
        }
    }
}
