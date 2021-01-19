using AtominaCraft.ZResources.Strings;

namespace AtominaCraft.ZResources.Maths
{
    // only created my own matrix class cus nothing was working before with the OpenTK one,
    // except it wouldve if i knew how to opengl. dont really need this matrix class but eh
    // ima keep.
    public class Matrix4
    {
        public float[] M { get; set; }

        public Matrix4()
        {
            MakeZero();
        }

        public Matrix4(float[] _m)
        {
            M = new float[16];
            for (int i = 0; i < MathAddons.Min(_m.Length, 16).ToInt(); i++)
            {
                M[i] = _m[i];
            }
        }

        /// <summary>
        /// Fills the entire matrix with a given value
        /// </summary>
        /// <param name="b"></param>
        public void Fill(float b)
        {
            M = new float[16];
            for (int i = 0; i < 16; i++)
            {
                M[i] = b;
            }
        }

        public void MakeZero()
        {
            Fill(0.0f);
        }

        public static Matrix4 CreateWorldToLocal(Vector3 position, Vector3 euler, Vector3 scale)
        {
            return
                Scale(1.0f / scale) *
                RotationZ(-euler.Z) *
                RotationX(-euler.X) *
                RotationY(-euler.Y) *
                Translation(-position);
        }

        public static Matrix4 CreateLocalToWorld(Vector3 position, Vector3 euler, Vector3 scale)
        {
            return
                Translation(position) *
                RotationY(euler.Y) *
                RotationX(euler.X) *
                RotationZ(euler.Z) *
                Scale(scale);
        }

        /// <summary>
        /// Creates an identity matrix, containing a diagonal line of 1s, rest 0s.
        /// </summary>
        public void MakeIdentity()
        {
            M[0] = 1.0f;  M[1] = 0.0f;  M[2] = 0.0f;  M[3] = 0.0f;
            M[4] = 0.0f;  M[5] = 1.0f;  M[6] = 0.0f;  M[7] = 0.0f;
            M[8] = 0.0f;  M[9] = 0.0f;  M[10] = 1.0f; M[11] = 0.0f;
            M[12] = 0.0f; M[13] = 0.0f; M[14] = 0.0f; M[15] = 1.0f;
        }

        /// <summary>
        /// Makes a rotation in the X axis
        /// </summary>
        /// <param name="a">The angle, in radians</param>
        public void MakeRotationX(float a)
        {
            M[0] = 1.0f;  M[1] = 0.0f;     M[2] = 0.0f;      M[3] = 0.0f;
            M[4] = 0.0f;  M[5] = a.Cosf(); M[6] = -a.Sinf(); M[7] = 0.0f;
            M[8] = 0.0f;  M[9] = a.Sinf(); M[10] = a.Cosf(); M[11] = 0.0f;
            M[12] = 0.0f; M[13] = 0.0f;    M[14] = 0.0f;     M[15] = 1.0f;
        }

        /// <summary>
        /// Makes a rotation in the Y axis
        /// </summary>
        /// <param name="a">The angle, in radians</param>
        public void MakeRotationY(float a)
        {
            M[0] = a.Cosf();  M[1] = 0.0f;  M[2] = a.Sinf();  M[3] = 0.0f;
            M[4] = 0.0f;      M[5] = 1.0f;  M[6] = 0.0f;      M[7] = 0.0f;
            M[8] = -a.Sinf(); M[9] = 0.0f;  M[10] = a.Cosf(); M[11] = 0.0f;
            M[12] = 0.0f;     M[13] = 0.0f; M[14] = 0.0f;     M[15] = 1.0f;
        }

        /// <summary>
        /// Makes a rotation in the Z axis
        /// </summary>
        /// <param name="a">The angle, in radians</param>
        public void MakeRotationZ(float a)
        {
            M[0] = a.Cosf(); M[1] = -a.Sinf(); M[2] = 0.0f;  M[3] = 0.0f;
            M[4] = a.Sinf(); M[5] = a.Cosf();  M[6] = 0.0f;  M[7] = 0.0f;
            M[8] = 0.0f;     M[9] = 0.0f;      M[10] = 1.0f; M[11] = 0.0f;
            M[12] = 0.0f;    M[13] = 0.0f;     M[14] = 0.0f; M[15] = 1.0f;
        }

        /// <summary>
        /// Makes a translation with the given vector
        /// </summary>
        /// <param name="s"></param>
        public void MakeTranslation(Vector3 t)
        {
            M[0] = 1.0f;  M[1] = 0.0f;  M[2] = 0.0f;  M[3] = t.X;
            M[4] = 0.0f;  M[5] = 1.0f;  M[6] = 0.0f;  M[7] = t.Y;
            M[8] = 0.0f;  M[9] = 0.0f;  M[10] = 1.0f; M[11] = t.Z;
            M[12] = 0.0f; M[13] = 0.0f; M[14] = 0.0f; M[15] = 1.0f;
        }

        /// <summary>
        /// Makes a scale with the given vector
        /// </summary>
        /// <param name="s"></param>
        public void MakeScale(Vector3 s)
        {
            M[0] = s.X;   M[1] = 0.0f;  M[2] = 0.0f;  M[3] = 0.0f;
            M[4] = 0.0f;  M[5] = s.Y;   M[6] = 0.0f;  M[7] = 0.0f;
            M[8] = 0.0f;  M[9] = 0.0f;  M[10] = s.Z;  M[11] = 0.0f;
            M[12] = 0.0f; M[13] = 0.0f; M[14] = 0.0f; M[15] = 1.0f;
        }

        /// <summary>
        /// Sets the X axis without modifying anything else
        /// </summary>
        /// <param name="t"></param>
        public void SetXAxis(Vector3 t)
        {
            M[0] = t.X;
            M[4] = t.Y;
            M[8] = t.Z;
        }

        /// <summary>
        /// Sets the Y axis without modifying anything else
        /// </summary>
        /// <param name="t"></param>
        public void SetYAxis(Vector3 t)
        {
            M[1] = t.X;
            M[5] = t.Y;
            M[9] = t.Z;
        }

        /// <summary>
        /// Sets the Z axis without modifying anything else
        /// </summary>
        /// <param name="t"></param>
        public void SetZAxis(Vector3 t)
        {
            M[2] = t.X;
            M[6] = t.Y;
            M[10] = t.Z;
        }

        /// <summary>
        /// Sets the translation of the matrix without modifying anything else
        /// </summary>
        /// <param name="t"></param>
        public void SetTranslation(Vector3 t)
        {
            M[3] = t.X;
            M[7] = t.Y;
            M[11] = t.Z;
        }

        /// <summary>
        /// Sets the scale of the matrix without modifying anything else
        /// </summary>
        /// <param name="t"></param>
        public void SetScale(Vector3 t)
        {
            M[0] = t.X;
            M[5] = t.Y;
            M[10] = t.Z;
        }

        /// <summary>
        /// Flips the matrix along a diagonal line
        /// </summary>
        /// <param name="t"></param>
        public Matrix4 Transposed()
        {
            Matrix4 o = new Matrix4();
            o.M[0]  = M[0]; o.M[1]  = M[4]; o.M[2]  = M[8];  o.M[3]  = M[12];
            o.M[4]  = M[1]; o.M[5]  = M[5]; o.M[6]  = M[9];  o.M[7]  = M[13];
            o.M[8]  = M[2]; o.M[9]  = M[6]; o.M[10] = M[10]; o.M[11] = M[14];
            o.M[12] = M[3]; o.M[13] = M[7]; o.M[14] = M[11]; o.M[15] = M[15];
            return o;
        }

        public void Translate(Vector3 t)
        {
            M[3] += t.X;
            M[7] += t.Y;
            M[11] += t.Z;
        }

        public void Stretch(Vector3 s)
        {
            M[0] *= s.X;
            M[5] *= s.Y;
            M[10] *= s.Z;
        }

        #region Operations

        public static Matrix4 operator *(Matrix4 a, float b)
        {
            Matrix4 o = new Matrix4();
            for (int i = 0; i < 16; ++i)
            {
                o.M[i] = a.M[i] *= b;
            }
            return o;
        }

        public static Matrix4 operator /(Matrix4 a, float b)
        {
            Matrix4 o = new Matrix4();
            for (int i = 0; i < 16; ++i)
            {
                o.M[i] = a.M[i] /= b;
            }
            return o;
        }

        /// <summary>
        /// Multiplies 2 given matrixes.
        /// <para>
        /// Performs the Dot product. every column in b is multiplied by a row in a, to return an output row. then
        /// every other row in a is multiplied by every column in b to return the full matrix.
        /// </para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 m = new Matrix4();
            m.M[0] =  b.M[0] * a.M[0]  + b.M[4] * a.M[1]  + b.M[8]  * a.M[2] + b.M[12]  * a.M[3];
            m.M[1] =  b.M[1] * a.M[0]  + b.M[5] * a.M[1]  + b.M[9]  * a.M[2] + b.M[13]  * a.M[3];
            m.M[2] =  b.M[2] * a.M[0]  + b.M[6] * a.M[1]  + b.M[10] * a.M[2] + b.M[14]  * a.M[3];
            m.M[3] =  b.M[3] * a.M[0]  + b.M[7] * a.M[1]  + b.M[11] * a.M[2] + b.M[15]  * a.M[3];
                                                                                       
            m.M[4] =  b.M[0] * a.M[4]  + b.M[4] * a.M[5]  + b.M[8]  * a.M[6] + b.M[12]  * a.M[7];
            m.M[5] =  b.M[1] * a.M[4]  + b.M[5] * a.M[5]  + b.M[9]  * a.M[6] + b.M[13]  * a.M[7];
            m.M[6] =  b.M[2] * a.M[4]  + b.M[6] * a.M[5]  + b.M[10] * a.M[6] + b.M[14]  * a.M[7];
            m.M[7] =  b.M[3] * a.M[4]  + b.M[7] * a.M[5]  + b.M[11] * a.M[6] + b.M[15]  * a.M[7];
                                                          
            m.M[8] =  b.M[0] * a.M[8]  + b.M[4] * a.M[9]  + b.M[8]  * a.M[10] + b.M[12] * a.M[11];
            m.M[9] =  b.M[1] * a.M[8]  + b.M[5] * a.M[9]  + b.M[9]  * a.M[10] + b.M[13] * a.M[11];
            m.M[10] = b.M[2] * a.M[8]  + b.M[6] * a.M[9]  + b.M[10] * a.M[10] + b.M[14] * a.M[11];
            m.M[11] = b.M[3] * a.M[8]  + b.M[7] * a.M[9]  + b.M[11] * a.M[10] + b.M[15] * a.M[11];

            m.M[12] = b.M[0] * a.M[12] + b.M[4] * a.M[13] + b.M[8]  * a.M[14] + b.M[12] * a.M[15];
            m.M[13] = b.M[1] * a.M[12] + b.M[5] * a.M[13] + b.M[9]  * a.M[14] + b.M[13] * a.M[15];
            m.M[14] = b.M[2] * a.M[12] + b.M[6] * a.M[13] + b.M[10] * a.M[14] + b.M[14] * a.M[15];
            m.M[15] = b.M[3] * a.M[12] + b.M[7] * a.M[13] + b.M[11] * a.M[14] + b.M[15] * a.M[15];
            return m;
        }

        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            return new Vector4(
                a.M[0]  * b.X + a.M[1]  * b.Y + a.M[2]  * b.Z + a.M[3]  * b.W,
                a.M[4]  * b.X + a.M[5]  * b.Y + a.M[6]  * b.Z + a.M[7]  * b.W,
                a.M[8]  * b.X + a.M[9]  * b.Y + a.M[10] * b.Z + a.M[11] * b.W,
                a.M[12] * b.X + a.M[13] * b.Y + a.M[14] * b.Z + a.M[15] * b.W);
        }

        // Use extension methods
        //public static Vector3 MultiplyPoint(Matrix4 a, Vector4 b)
        //{
        //    Vector3 p = new Vector3(
        //        a.M[0] * b.X + a.M[1] * b.Y + a.M[2] * b.Z + a.M[3],
        //        a.M[4] * b.X + a.M[5] * b.Y + a.M[6] * b.Z + a.M[7],
        //        a.M[8] * b.X + a.M[9] * b.Y + a.M[10] * b.Z + a.M[11]);
        //    float w = a.M[12] * b.X + a.M[13] * b.Y + a.M[14] * b.Z + a.M[15];
        //    return p / w;
        //}
        //
        //public static Vector3 MultiplyDirection(Matrix4 a, Vector4 b)
        //{
        //    return new Vector3(
        //        a.M[0] * b.X + a.M[1] * b.Y + a.M[2] * b.Z,
        //        a.M[4] * b.X + a.M[5] * b.Y + a.M[6] * b.Z,
        //        a.M[8] * b.X + a.M[9] * b.Y + a.M[10] * b.Z);
        //}

        public Matrix4 Inverse()
        {
            Matrix4 inv = new Matrix4();
            inv.M[0] = M[5] * M[10] * M[15] -
                M[5] * M[11] * M[14] -
                M[9] * M[6] * M[15] +
                M[9] * M[7] * M[14] +
                M[13] * M[6] * M[11] -
                M[13] * M[7] * M[10];

            inv.M[4] = -M[4] * M[10] * M[15] +
                M[4] * M[11] * M[14] +
                M[8] * M[6] * M[15] -
                M[8] * M[7] * M[14] -
                M[12] * M[6] * M[11] +
                M[12] * M[7] * M[10];

            inv.M[8] = M[4] * M[9] * M[15] -
                M[4] * M[11] * M[13] -
                M[8] * M[5] * M[15] +
                M[8] * M[7] * M[13] +
                M[12] * M[5] * M[11] -
                M[12] * M[7] * M[9];

            inv.M[12] = -M[4] * M[9] * M[14] +
                M[4] * M[10] * M[13] +
                M[8] * M[5] * M[14] -
                M[8] * M[6] * M[13] -
                M[12] * M[5] * M[10] +
                M[12] * M[6] * M[9];

            inv.M[1] = -M[1] * M[10] * M[15] +
                M[1] * M[11] * M[14] +
                M[9] * M[2] * M[15] -
                M[9] * M[3] * M[14] -
                M[13] * M[2] * M[11] +
                M[13] * M[3] * M[10];

            inv.M[5] = M[0] * M[10] * M[15] -
                M[0] * M[11] * M[14] -
                M[8] * M[2] * M[15] +
                M[8] * M[3] * M[14] +
                M[12] * M[2] * M[11] -
                M[12] * M[3] * M[10];

            inv.M[9] = -M[0] * M[9] * M[15] +
                M[0] * M[11] * M[13] +
                M[8] * M[1] * M[15] -
                M[8] * M[3] * M[13] -
                M[12] * M[1] * M[11] +
                M[12] * M[3] * M[9];

            inv.M[13] = M[0] * M[9] * M[14] -
                M[0] * M[10] * M[13] -
                M[8] * M[1] * M[14] +
                M[8] * M[2] * M[13] +
                M[12] * M[1] * M[10] -
                M[12] * M[2] * M[9];

            inv.M[2] = M[1] * M[6] * M[15] -
                M[1] * M[7] * M[14] -
                M[5] * M[2] * M[15] +
                M[5] * M[3] * M[14] +
                M[13] * M[2] * M[7] -
                M[13] * M[3] * M[6];

            inv.M[6] = -M[0] * M[6] * M[15] +
                M[0] * M[7] * M[14] +
                M[4] * M[2] * M[15] -
                M[4] * M[3] * M[14] -
                M[12] * M[2] * M[7] +
                M[12] * M[3] * M[6];

            inv.M[10] = M[0] * M[5] * M[15] -
                M[0] * M[7] * M[13] -
                M[4] * M[1] * M[15] +
                M[4] * M[3] * M[13] +
                M[12] * M[1] * M[7] -
                M[12] * M[3] * M[5];

            inv.M[14] = -M[0] * M[5] * M[14] +
                M[0] * M[6] * M[13] +
                M[4] * M[1] * M[14] -
                M[4] * M[2] * M[13] -
                M[12] * M[1] * M[6] +
                M[12] * M[2] * M[5];

            inv.M[3] = -M[1] * M[6] * M[11] +
                M[1] * M[7] * M[10] +
                M[5] * M[2] * M[11] -
                M[5] * M[3] * M[10] -
                M[9] * M[2] * M[7] +
                M[9] * M[3] * M[6];

            inv.M[7] = M[0] * M[6] * M[11] -
                M[0] * M[7] * M[10] -
                M[4] * M[2] * M[11] +
                M[4] * M[3] * M[10] +
                M[8] * M[2] * M[7] -
                M[8] * M[3] * M[6];

            inv.M[11] = -M[0] * M[5] * M[11] +
                M[0] * M[7] * M[9] +
                M[4] * M[1] * M[11] -
                M[4] * M[3] * M[9] -
                M[8] * M[1] * M[7] +
                M[8] * M[3] * M[5];

            inv.M[15] = M[0] * M[5] * M[10] -
                M[0] * M[6] * M[9] -
                M[4] * M[1] * M[10] +
                M[4] * M[2] * M[9] +
                M[8] * M[1] * M[6] -
                M[8] * M[2] * M[5];

            float det = M[0] * inv.M[0] + M[1] * inv.M[4] + M[2] * inv.M[8] + M[3] * inv.M[12];
            inv /= det;
            return inv;
        }

        #endregion

        #region Vectors

        public Vector3 GetXAxis()
        {
            return new Vector3(M[0], M[4], M[8]);
        }
        public Vector3 GetYAxis()
        {
            return new Vector3(M[1], M[5], M[9]);
        }
        public Vector3 GetZAxis()
        {
            return new Vector3(M[2], M[6], M[10]);
        }
        public Vector3 GetXYZAxis()
        {
            return GetXAxis() * GetYAxis() * GetZAxis();
        }
        public Vector3 GetTranslation()
        {
            return new Vector3(M[3], M[7], M[11]);
        }
        public Vector3 GetScale()
        {
            return new Vector3(M[0], M[5], M[10]);
        }

        #endregion

        #region Static methods

        public static Matrix4 Zero()
        {
            Matrix4 m = new Matrix4();
            m.MakeZero();
            return m;
        }

        public static Matrix4 Identity()
        {
            Matrix4 m = new Matrix4();
            m.MakeIdentity();
            return m;
        }

        public static Matrix4 RotationX(float a)
        {
            Matrix4 m = new Matrix4();
            m.MakeRotationX(a);
            return m;
        }

        public static Matrix4 RotationY(float a)
        {
            Matrix4 m = new Matrix4();
            m.MakeRotationY(a);
            return m;
        }

        public static Matrix4 RotationZ(float a)
        {
            Matrix4 m = new Matrix4();
            m.MakeRotationZ(a);
            return m;
        }

        public static Matrix4 Translation(Vector3 t)
        {
            Matrix4 m = new Matrix4();
            m.MakeTranslation(t);
            return m;
        }

        public static Matrix4 Scale(float s)
        {
            Matrix4 m = new Matrix4();
            m.MakeScale(new Vector3(s));
            return m;
        }

        public static Matrix4 Scale(Vector3 s)
        {
            Matrix4 m = new Matrix4();
            m.MakeScale(s);
            return m;
        }

        #endregion

        public override string ToString()
        {
            int maxLen = 10;
            string row1 = M[0].EnsureLength(maxLen) + M[1].EnsureLength(maxLen) + M[2].EnsureLength(maxLen) + M[3].EnsureLength(maxLen);
            string row2 = M[4].EnsureLength(maxLen) + M[5].EnsureLength(maxLen) + M[6].EnsureLength(maxLen) + M[7].EnsureLength(maxLen);
            string row3 = M[8].EnsureLength(maxLen) + M[9].EnsureLength(maxLen) + M[10].EnsureLength(maxLen) + M[11].EnsureLength(maxLen);
            string row4 = M[12].EnsureLength(maxLen) + M[13].EnsureLength(maxLen) + M[14].EnsureLength(maxLen) + M[15].EnsureLength(maxLen);
            return $"{row1}\n{row2}\n{row3}\n{row4}";
        }
    }
}
