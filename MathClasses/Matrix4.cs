using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MathClasses
{
    public class Matrix4
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16;

        public Matrix4()
        {
            m1 = 1; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = 1; m7 = 0; m8 = 0;
            m9 = 0; m10= 0; m11= 1; m12= 0;
            m13= 0; m14= 0; m15= 0; m16= 1;
        }
        public Matrix4(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m13, float m14, float m15, float m16)
        {
            Set(m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16);
        }

        public void Set(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m13, float m14, float m15, float m16)
        {
            this.m1 = m1; this.m2 = m2; this.m3 = m3; this.m4 = m4; 
            this.m5 = m5; this.m6 = m6; this.m7 = m7; this.m8 = m8; 
            this.m9 = m9; this.m10 = m10; this.m11 = m11; this.m12 = m12;
            this.m13 = m13; this.m14 = m14; this.m15 = m15; this.m16 = m16;
        }

        public void Set(Matrix4 m)
        {
            this.m1 = m.m1; this.m2 = m.m2; this.m3 = m.m3; this.m4 = m.m4;
            this.m5 = m.m5; this.m6 = m.m6; this.m7 = m.m7; this.m8 = m.m8;
            this.m9 = m.m9; this.m10 = m.m10; this.m11 = m.m11; this.m12 = m.m12;
            this.m13 = m.m13; this.m14 = m.m14; this.m15 = m.m15; this.m16 = m.m16;
        }

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {

            return new Matrix4
                (
                 rhs.m1 * lhs.m1 + rhs.m5 * lhs.m2 + rhs.m9 * lhs.m4,
                 rhs.m2 * lhs.m1 + rhs.m6 * lhs.m2 + rhs.m10 * lhs.m4,
                 rhs.m3 * lhs.m1 + rhs.m7 * lhs.m2 + rhs.m11 * lhs.m4,
                 rhs.m4 * lhs.m1 + rhs.m8 * lhs.m5 + rhs.m12 * lhs.m4,

                 rhs.m1 * lhs.m5 + rhs.m5 * lhs.m2 + rhs.m9 * lhs.m8,
                 rhs.m2 * lhs.m5 + rhs.m6 * lhs.m2 + rhs.m10 * lhs.m8,
                 rhs.m3 * lhs.m5 + rhs.m7 * lhs.m2 + rhs.m11 * lhs.m8,
                 rhs.m4 * lhs.m5 + rhs.m8 * lhs.m5 + rhs.m12 * lhs.m8,

                 rhs.m1 * lhs.m9 + rhs.m5 * lhs.m2 + rhs.m9 * lhs.m12,
                 rhs.m2 * lhs.m9 + rhs.m6 * lhs.m2 + rhs.m10 * lhs.m12,
                 rhs.m3 * lhs.m9 + rhs.m7 * lhs.m2 + rhs.m11 * lhs.m12,
                 rhs.m4 * lhs.m9 + rhs.m8 * lhs.m5 + rhs.m12 * lhs.m12,

                 rhs.m1 * lhs.m13 + rhs.m5 * lhs.m2 + rhs.m9 * lhs.m16,
                 rhs.m2 * lhs.m13 + rhs.m6 * lhs.m2 + rhs.m10 * lhs.m16,
                 rhs.m3 * lhs.m13 + rhs.m7 * lhs.m2 + rhs.m11 * lhs.m16,
                 rhs.m4 * lhs.m13 + rhs.m8 * lhs.m5 + rhs.m12 * lhs.m16
                );
        }

        public void SetRotateX(double radians)
        {
            Set(
                1, 0, 0, 0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                0, (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 0, 1
                );
        }

        public void RotateX(double radians)
        {
            Matrix4 m = new Matrix4();
            m.SetRotateX(radians);

            Set(this * m);
        }

        public void SetRotateY(double radians)
        {
            Set(
                (float)Math.Cos(radians), 0, (float)-Math.Sin(radians), 0,
                0, 1, 0, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
                0, 0, 0, 1
                );
        }

        public void RotateY(double radians)
        {
            Matrix4 m = new Matrix4();
            m.SetRotateY(radians);

            Set(this * m);
        }

        public void SetRotateZ(double radians)
        {
            Set(
                (float)Math.Cos(radians), (float)Math.Sin(radians), 0, 0,
                (float)-Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
        }

        public void RotateZ(double radians)
        {
            Matrix4 m = new Matrix4();
            m.SetRotateZ(radians);

            Set(this * m);
        }
    }
}
