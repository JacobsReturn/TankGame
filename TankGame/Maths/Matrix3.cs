using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    public class Matrix3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9; // The matrice3 matrix.

        /// <summary>
        /// Initializing the matrix.
        /// </summary>
        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        /// <summary>
        /// Initializing the matrix with all its values.
        /// </summary>
        public Matrix3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            Set(m1, m2, m3, m4, m5, m6, m7, m8, m9);
        }

        /// <summary>
        /// Setting the matrix values.
        /// </summary>
        public void Set(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            this.m1 = m1; this.m2 = m2; this.m3 = m3;
            this.m4 = m4; this.m5 = m5; this.m6 = m6;
            this.m7 = m7; this.m8 = m8; this.m9 = m9;
        }

        /// <summary>
        /// Setting the matrix values to another matrices.
        /// </summary>
        public void Set(Matrix3 m)
        {
            this.m1 = m.m1; this.m2 = m.m2; this.m3 = m.m3;
            this.m4 = m.m4; this.m5 = m.m5; this.m6 = m.m6;
            this.m7 = m.m7; this.m8 = m.m8; this.m9 = m.m9;
        }

        /// <summary>
        /// Merging matrices.
        /// </summary>
        /// <param name="lhs">Left hand Matrix.</param>
        /// <param name="rhs">Right hand Matrix.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Set the matrices rotation X.
        /// </summary>
        /// <param name="radians">Rotation in radians.</param>
        public void SetRotateX(double radians)
        {
            Set(
                1, 0, 0, 
                0, (float)Math.Cos(radians), (float)Math.Sin(radians),
                0, (float)- Math.Sin(radians), (float)Math.Cos(radians)

                );
        }

        /// <summary>
        /// Set the matrices rotation X.
        /// </summary>
        /// <param name="radians">Rotation in radians.</param>
        public void RotateX(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(radians);

            Set(this * m);
        }

        /// <summary>
        /// Set the matrices rotation Y.
        /// </summary>
        /// <param name="radians">Rotation in radians.</param>
        public void SetRotateY(double radians)
        {
            Set((float)Math.Cos(radians), 0, (float)-Math.Sin(radians),
                0, 1, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians));
        }

        /// <summary>
        /// Set the matrices rotation Y.
        /// </summary>
        /// <param name="radians">Rotation in radians.</param>
        public void RotateY(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(radians);

            Set(this * m);
        }

        /// <summary>
        /// Set the matrices rotation Z.
        /// </summary>
        /// <param name="radians">Rotation in radians.</param>
        public void SetRotateZ(double radians)
        {
            Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                -(float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 1);
        }

        /// <summary>
        /// Set the matrices rotation Z.
        /// </summary>
        /// <param name="radians">Rotation in radians.</param>
        public void RotateZ(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(radians);

            Set(this * m);
        }

        /// <summary>
        /// Setting the Scale.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="z">Z position (uneeded)</param>
        public void SetScaled(float x, float y, float z)
        {
            m1 = x; m2 = 0; m3 = 0;
            m4 = 0; m5 = y; m6 = 0;
            m7 = 0; m8 = 0; m9 = z;
        }

        /// <summary>
        /// Calling SetScaled, but a simplifier function for Vector3s.
        /// </summary>
        /// <param name="v">A vector3 to scale with.</param>
        public void SetScaled(Vector3 v)
        {
            SetScaled(v.x, v.y, v.z);
        }

        /// <summary>
        /// Calling SetScaled, but scaling with a matrix.
        /// </summary>
        public void Scale(float x, float y, float z)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(x, y, z);

            Set(this * m);
        }

        /// <summary>
        /// Scale the Vector3.
        /// </summary>
        /// <param name="v">The vector to scale.</param>
        public void Scale(Vector3 v)
        {
            Scale(v.x, v.y, v.z);
        }

        /// <summary>
        /// Setting the Euler of the matrix.
        /// </summary>
        /// <param name="pitch">Angular pitch.</param>
        /// <param name="yaw">Angular yaw.</param>
        /// <param name="roll">Angular roll.</param>
        public void SetEuler(float pitch, float yaw, float roll)
        {
            Matrix3 x = new Matrix3();
            Matrix3 y = new Matrix3();
            Matrix3 z = new Matrix3();
            x.SetRotateX(pitch);
            y.SetRotateY(yaw);
            z.SetRotateZ(roll);

            Set(z * y * x);
        }

        /// <summary>
        /// Setting the position translation.
        /// </summary>
        /// <param name="x">X to move to.</param>
        /// <param name="y">Y to move to.</param>
        public void SetTranslation(float x, float y)
        {
            m6 = y; m7 = x; m8 = y; m9 = 1;
        }

        /// <summary>
        /// Setting the position translation. (Same as SetTranslation)
        /// </summary>
        /// <param name="x">X to move to.</param>
        /// <param name="y">Y to move to.</param>
        public void Translate(float x, float y)
        {
            m6 += y; m7 += x; m8 += y;
        }

        /// <summary>
        /// Transforming the Matrix to a Vector.
        /// </summary>
        /// <returns>A Vector3 transformed matrix.</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(1, 1, 1) * this; // Loved how easy it was :0
        }

        /// <summary>
        /// AbsoluteRotation (ignores current rotation)
        /// </summary>
        /// <param name="radians">Rotation radians to apply.</param>
        public void AbsoluteRotate(double radians)
        {
            Matrix3 newMatrix = new Matrix3();
            newMatrix.SetRotateZ(radians);

            Set(newMatrix);
        }

        /// <summary>
        /// Getting the forward of the GameObject.
        /// </summary>
        /// <returns>Matrix forward</returns>
        public Vector3 GetForward()
        {
            return new Vector3(m1, m2, 1);
        }

        /// <summary>
        /// Getting the right of the GameObject.
        /// </summary>
        /// <returns>Matrix right</returns>
        public Vector3 GetRight()
        {
            return new Vector3(m3, m4, 1);
        }

        /// <summary>
        /// Getting the rotation of the GameObject.
        /// </summary>
        /// <returns>Matrix rotation</returns>
        public float GetRotation()
        {
            return (float)Math.Atan2(m2, m1);
        }
    }
}