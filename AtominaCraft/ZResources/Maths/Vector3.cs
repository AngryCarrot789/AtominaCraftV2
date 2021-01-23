using AtominaCraft.ZResources.Strings;

namespace AtominaCraft.ZResources.Maths
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static Vector3 Zero => new Vector3(0.0f);
        /// <summary>
        /// The size of a block
        /// </summary>
        public static Vector3 Halfs => new Vector3(0.5f);
        public static Vector3 Ones => new Vector3(1.0f);
        public static Vector3 UnitX => new Vector3(1.0f, 0.0f, 0.0f);
        public static Vector3 UnitY => new Vector3(0.0f, 1.0f, 0.0f);
        public static Vector3 UnitZ => new Vector3(0.0f, 0.0f, 1.0f);

        public static Vector3 Up        => new Vector3( 0,  1,  0);
        public static Vector3 Down      => new Vector3( 0, -1,  0);
        public static Vector3 Left      => new Vector3( 1,  0,  0);
        public static Vector3 Right     => new Vector3(-1,  0,  0);
        public static Vector3 Backward  => new Vector3( 0,  0,  1);
        public static Vector3 Forward   => new Vector3( 0,  0, -1);

        public Vector3() { Set(0.0f, 0.0f, 0.0f); }

        public Vector3(float a)
        {
            Set(a, a, a);
        }

        public Vector3(float x, float y, float z)
        {
            Set(x, y, z);
        }

        public void Set(Vector3 v)
        {
            Set(v.X, v.Y, v.Z);
        }

        public void Set(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void SetZero() => Set(0.0f, 0.0f, 0.0f);
        public void SetOnes() => Set(1.0f, 1.0f, 1.0f);
        public void SetUnitX() => Set(1.0f, 0.0f, 0.0f);
        public void SetUnitY() => Set(0.0f, 1.0f, 0.0f);
        public void SetUnitZ() => Set(0.0f, 0.0f, 1.0f);

        public float Dot(Vector3 b)
        {
            return X * b.X + Y * b.Y + Z * b.Z;
        }

        public Vector3 Cross(Vector3 b)
        {
            return new Vector3(
                Y * b.Z - Z * b.Y, 
                Z * b.X - X * b.Z, 
                X * b.Y - Y * b.X);
        }

        public float MagnitudeSquare()
        {
            return X * X + Y * Y + Z * Z;
        }

        public float Magnitude()
        {
            return MagnitudeSquare().Sqrtf();
        }

        public void Normalise()
        {
            Vector3 v = this;
            v /= Magnitude();
            Set(v.X, v.Y, v.Z);
        }

        public Vector3 Normalised()
        {
            Vector3 v = this;
            v /= Magnitude();
            return v;
        }

        public float Angle(Vector3 b)
        {
            return Normalised().Dot(b.Normalised()).ACosf();
        }

        public void ClipMag(float m)
        {
            if (m > 0.0f)
            {
                float r = MagnitudeSquare() / (m * m);
                if (r > 1.0f)
                {
                    Vector3 v = this;
                    v /= r.Sqrtf();
                    Set(v.X, v.Y, v.Z);
                }
            }
        }

        public bool IsNDC()
        {
            return (X > -1.0f && X < 1.0f && Y > -1.0f && Y < 1.0f && Z > -1.0f && Z < 1.0f);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Vector3 operator /(float a, Vector3 v)
        {
            return new Vector3(a / v.X, a / v.Y, a / v.Z);
        }

        public static Vector3 operator -(Vector3 a, float b)
        {
            return new Vector3(a.X - b, a.Y - b, a.Z - b);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator +(Vector3 a, float b)
        {
            return new Vector3(a.X + b, a.Y + b, a.Z + b);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        public Vector3 Duplicate()
        {
            return new Vector3(X, Y, Z);
        }

        public Vector3 UnitInvert()
        {
            Vector3 a = new Vector3(0.0f, 0.0f, 0.0f);
            a.X = X == 1.0f ? 0.0f : 1.0f;
            a.Y = Y == 1.0f ? 0.0f : 1.0f;
            a.Z = Z == 1.0f ? 0.0f : 1.0f;
            return a;
        }

        public override int GetHashCode()
        {
            // 20, 30, 40
            // 20 XOR (30 << 2) XOR (40 >> 2)
            // 20 XOR 120 XOR 10
            // 102
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;
        }

        public override string ToString()
        {
            int maxLengh = 10;
            return $"X: {X.EnsureLength(maxLengh)} Y: {Y.EnsureLength(maxLengh)} Z: {Z.EnsureLength(maxLengh)}";
        }
    }
}
