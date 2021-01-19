using AtominaCraft.Resources.Strings;

namespace AtominaCraft.Resources.Maths
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2 Zero => new Vector2(0.0f);
        public static Vector2 Ones => new Vector2(1.0f);
        public static Vector2 UnitX => new Vector2(1.0f, 0.0f);
        public static Vector2 UnitY => new Vector2(0.0f, 1.0f);

        public Vector2() { Set(0.0f, 0.0f); }

        public Vector2(float a)
        {
            Set(a, a);
        }

        public Vector2(float x, float y)
        {
            Set(x, y);
        }

        public void Set(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void SetZero() => Set(0.0f, 0.0f);
        public void SetOnes() => Set(1.0f, 1.0f);
        public void SetUnitX() => Set(1.0f, 0.0f);
        public void SetUnitY() => Set(0.0f, 1.0f);

        public float MagnitudeSquare()
        {
            return X * X + Y * Y;
        }

        public float Magnitude()
        {
            return MagnitudeSquare().Sqrtf();
        }

        public void Normalise()
        {
            Vector2 v = this;
            v /= Magnitude();
            Set(v.X, v.Y);
        }

        public Vector2 Normalised()
        {
            Vector2 v = this;
            v /= Magnitude();
            return v;
        }

        public void ClipMag(float m)
        {
            if (m > 0.0f)
            {
                float r = MagnitudeSquare() / (m * m);
                if (r > 1.0f)
                {
                    Vector2 v = this;
                    v /= r.Sqrtf();
                    Set(v.X, v.Y);
                }
            }
        }

        public bool IsNDC()
        {
            return (X > -1.0f && X < 1.0f && Y > -1.0f && Y < 1.0f);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2 operator /(float a, Vector2 v)
        {
            return new Vector2(a / v.X, a / v.Y);
        }

        public static Vector2 operator -(Vector2 a, float b)
        {
            return new Vector2(a.X - b, a.Y - b);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        public static Vector2 operator +(Vector2 a, float b)
        {
            return new Vector2(a.X + b, a.Y + b);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        public Vector2 Duplicate()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            const int maxLengh = 10;
            return X.EnsureLength(maxLengh) + Y.EnsureLength(maxLengh);
        }

        public Vector2 UnitInvert()
        {
            Vector2 a = new Vector2(0, 0);
            a.X = X == 1 ? 0 : 1;
            a.Y = Y == 1 ? 0 : 1;
            return a;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }
    }
}
