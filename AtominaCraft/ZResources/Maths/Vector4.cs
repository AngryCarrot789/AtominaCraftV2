using System;

namespace AtominaCraft.ZResources.Maths
{
    public class Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Vector4(float a)
        {
            X = a;
            Y = a;
            Z = a;
            W = a;
        }

        public Vector4(Vector3 xyz, float w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector3 XYZ()
        {
            return new Vector3(X, Y, Z);
        }

        public Vector3 XYZNormalised()
        {
            return new Vector3(X, Y, Z).Normalised();
        }

        public Vector3 Homogenised()
        {
            return new Vector3(X / W, Y / W, Z / W);
        }

        public float Dot(Vector4 b)
        {
            return X * b.X + Y * b.Y + Z * b.Z + W * b.W;
        }

        public static Vector4 operator *(Vector4 a, float b)
        {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W / b);
        }

        public static Vector4 operator /(Vector4 a, float b)
        {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        public static Vector4 operator -(Vector4 l, Vector4 r)
        {
            return new Vector4(l.X - r.X, l.Y - r.Y, l.Z - r.Z, l.W - r.W);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());
        }
    }
}
