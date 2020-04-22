using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;

        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        public Matrix3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            Set(m1, m2, m3, m4, m5, m6, m7, m8, m9);
        }

        public void Set(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            this.m1 = m1; this.m2 = m2; this.m3 = m3;
            this.m4 = m4; this.m5 = m5; this.m6 = m6;
            this.m7 = m7; this.m8 = m8; this.m9 = m9;
        }

        public void Set(Matrix3 m)
        {
            this.m1 = m.m1; this.m2 = m.m2; this.m3 = m.m3;
            this.m4 = m.m4; this.m5 = m.m5; this.m6 = m.m6;
            this.m7 = m.m7; this.m8 = m.m8; this.m9 = m.m9;
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {

            return new Matrix3
                (
                 lhs.m1 * rhs.m1 + lhs.m4 * rhs.m2 + lhs.m7 * rhs.m3,
                 lhs.m2 * rhs.m1 + lhs.m5 * rhs.m2 + lhs.m8 * rhs.m3,
                 lhs.m3 * rhs.m1 + lhs.m6 * rhs.m2 + lhs.m9 * rhs.m3,

                 lhs.m1 * rhs.m4 + lhs.m4 * rhs.m5 + lhs.m7 * rhs.m6,
                 lhs.m2 * rhs.m4 + lhs.m5 * rhs.m5 + lhs.m8 * rhs.m6,
                 lhs.m3 * rhs.m4 + lhs.m6 * rhs.m5 + lhs.m9 * rhs.m6,

                 lhs.m1 * rhs.m7 + lhs.m4 * rhs.m8 + lhs.m7 * rhs.m9,
                 lhs.m2 * rhs.m7 + lhs.m5 * rhs.m8 + lhs.m8 * rhs.m9,
                 lhs.m3 * rhs.m7 + lhs.m6 * rhs.m8 + lhs.m9 * rhs.m9

                );
        }

        public void SetRotateX(double radians)
        {
            Set(
                1, 0, 0, 
                0, (float)Math.Cos(radians), (float)Math.Sin(radians),
                0, (float)- Math.Sin(radians), (float)Math.Cos(radians)

                );
        }

        public void RotateX(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(radians);

            Set(this * m);
        }

        public void SetRotateY(double radians)
        {
            Set((float)Math.Cos(radians), 0, (float)-Math.Sin(radians),
                0, 1, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians));
        }

        public void RotateY(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(radians);

            Set(this * m);
        }

        public void SetRotateZ(double radians)
        {
            Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                -(float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 1);
        }

        public void RotateZ(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(radians);

            Set(this * m);
        }
    }
}