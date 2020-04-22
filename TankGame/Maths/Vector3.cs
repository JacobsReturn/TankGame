using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    public class Vector3
    {
        public float x, y, z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3((lhs.m1 * rhs.x) + (lhs.m4 * rhs.y) + (lhs.m7 * rhs.z),
                (lhs.m2 * rhs.x) + (lhs.m5 * rhs.y) + (lhs.m8 * rhs.z),
                (lhs.m3 * rhs.x) + (lhs.m6 * rhs.y) + (lhs.m9 * rhs.z));
        }

        public static Vector3 operator *(Vector3 lhs, Matrix3 rhs)
        {
            return new Vector3((lhs.x * rhs.m1) + (lhs.x * rhs.m4) + (lhs.x * rhs.m7),
                (lhs.x * rhs.m2) + (lhs.x * rhs.m5) + (lhs.x * rhs.m8),
                (lhs.x * rhs.m3) + (lhs.x * rhs.m6) + (lhs.x * rhs.m9));
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3 operator *(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }

        public static Vector3 operator *(Vector3 lhs, float scale)
        {
            return new Vector3(lhs.x * scale, lhs.y * scale, lhs.z * scale);
        }

        public static Vector3 operator *(float scale, Vector3 lhs)
        {
            return new Vector3(lhs.x * scale, lhs.y * scale, lhs.z * scale);
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        public static Vector3 Clamp(Vector3 t, Vector3 a, Vector3 b)
        {
            return Max(a, Min(b, t));
        }


        public float Dot(Vector3 vec)
        {
            return x * vec.x + y * vec.y + z * vec.z;
        }

        public Vector3 Cross(Vector3 vec)
        {
            return new Vector3(y * vec.z - z * vec.y, z * vec.x - x * vec.z, x * vec.y - y * vec.x);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z); 
        }
        public

        void Normalize()
        {
            float m = Magnitude();

            this.x /= m;
            this.y /= m;
            this.z /= m;
        }
    }
}
