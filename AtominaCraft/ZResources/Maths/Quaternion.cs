using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.ZResources.Maths
{
    public class Quaternion
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Quaternion(Quaternion q)
        {
            X = q.X;
            Y = q.Y;
            Z = q.Z;
            W = q.W;
        }

        public Quaternion(Vector3 euler)
        {
            W = 0;
            X = euler.X;
            Y = euler.Y;
            Z = euler.Z;
        }

        public Vector3 RotateVector(Vector3 coordinate)
        {
            Quaternion current = new Quaternion(this);
            Quaternion qv = new Quaternion(coordinate);
            Quaternion qr = current * qv * current.MultiplicativeInverse();

            return new Vector3(qr.X, qr.Y, qr.Z);
        }

        public Quaternion MultiplicativeInverse()
        {
            return Conjungate() * (1.0f / Normalised());
        }

        private float Normalised()
        {
            return MathF.Pow(W, 2) + MathF.Pow(X, 2) + MathF.Pow(Y, 2) + MathF.Pow(Z, 2);
        }

        public Quaternion Conjungate()
        {
            return new Quaternion(-X, -Y, -Z, W);
        }

        public static Quaternion ToQuaternion(Vector3 euler)
        {
            float yaw = euler.Y;
            float roll = euler.Z;
            float pitch = euler.X;
            float cy = MathF.Cos(yaw / 2);
            float sy = MathF.Sin(yaw / 2);
            float cp = MathF.Cos(pitch / 2);
            float sp = MathF.Sin(pitch / 2);
            float cr = MathF.Cos(roll / 2);
            float sr = MathF.Sin(roll / 2);
            return new Quaternion(
                sr * cp * cy - cr * sp * sy,
                cr * sp * cy + sr * cp * sy,
                cr * cp * sy - sr * sp * cy,
                cr * cp * cy + sr * sp * sy);
        }

        public static Vector3 ToEuler(Quaternion q)
        {
            Vector3 angles = new Vector3();

            // roll (X-axis rotation)
            float sr_cp = 2.0f * (q.W * q.X + q.Y * q.Z);
            float cr_cp = 1.0f - 2.0f * (q.X * q.X + q.Y * q.Y);
            angles.Z = MathF.Atan2(sr_cp, cr_cp);

            // pitch (Y-axis rotation)
            float sp = 2.0f * (q.W * q.Y - q.Z * q.X);
            if (MathF.Abs(sp) >= 1.0f)
                angles.X = MathF.CopySign(GameSettings.PI_HALF, sp); // use 90 degrees if out of range
            else
                angles.X = MathF.Asin(sp);

            // yaw (Z-axis rotation)
            float sy_cp = 2.0f * (q.W * q.Z + q.X * q.Y);
            float cy_cp = 1.0f - 2 * (q.Y * q.Y + q.Z * q.Z);
            angles.Y = MathF.Atan2(sy_cp, cy_cp);

            return angles;
        }

        public static Quaternion operator*(Quaternion a, Quaternion b)
        {
            return new Quaternion(
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X,
                a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W,
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z
                );
        }

        public static Quaternion operator *(Quaternion a, float b)
        {
            return new Quaternion(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        public static Quaternion AngleAxis(float rotationRads, Vector3 axis)
        {
            float s = MathF.Sin(rotationRads * 0.5f);
            Vector3 rot = axis * s;
            return new Quaternion(rot.X, rot.Y, rot.Z, MathF.Cos(rotationRads * 0.5f));
        }
    }
}
