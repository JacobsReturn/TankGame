using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace TankGame
{
    class AABB
    {
        Vector3 min = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); // Creating the min for the AABB.
        Vector3 max = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); // Creating the max for the AABB.

        public Color drawCol = Color.BLACK; // The colour of the drawing AABB.

        public AABB()
        {

        }

        /// <summary>
        /// This is how you make the AABB.
        /// </summary>
        /// <param name="min">the bottom left.</param>
        /// <param name="max">the top right.</param>
        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Getting the minimum of the AABB.
        /// </summary>
        /// <returns>Vector3 minimum</returns>
        public Vector3 GetMin()
        {
            return min;
        }

        /// <summary>
        /// Getting the maximum of the AABB.
        /// </summary>
        /// <returns>Vector3 maximum</returns>
        public Vector3 GetMax()
        {
            return max;
        }

        /// <summary>
        /// Setting the bounds of the AABB.
        /// </summary>
        /// <param name="min">Vector min</param>
        /// <param name="max">Vector max</param>
        public void SetBounds(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// The center of the AABB
        /// </summary>
        /// <returns>Vector3 center</returns>
        public Vector3 Center()
        {
            return (min + max) * 0.5f;
        }

        /// <summary>
        /// Drawing the bounding lines.
        /// </summary>
        public void Draw()
        {
            int w = (int)(max.x - min.x);
            int h = (int)(max.y - min.y);

            DrawRectangleLines((int)min.x, (int)min.y, w, h, drawCol);
        }

        /// <summary>
        /// Getting the extents of the AABB.
        /// </summary>
        /// <returns>The extents as a Vector3</returns>
        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(max.x - min.x) * 0.5f,
                Math.Abs(max.y - min.y) * 0.5f,
                Math.Abs(max.z - min.z) * 0.5f);
        }

        /// <summary>
        /// The list of corners.
        /// </summary>
        /// <returns>The list of corners.</returns>
        public List<Vector3> Corners()
        {
            List<Vector3> corners = new List<Vector3>(4);
            corners[0] = min;
            corners[1] = new Vector3(min.x, min.y, min.z);
            corners[2] = max;
            corners[3] = new Vector3(max.x, max.y, max.z);

            return corners;
        }

        /// <summary>
        /// Fitting a list of vector3 in an AABB.
        /// </summary>
        /// <param name="points">The points to inject.</param>
        public void Fit(List<Vector3> points)
        {
            max = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            min = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        
            foreach(Vector3 p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
        }

        /// <summary>
        /// Is the vector overlapping?
        /// </summary>
        /// <param name="p">The Vector3 position to check if its OverLapping.</param>
        /// <returns>Is overlapping</returns>
        public bool Overlaps(Vector3 p)
        {
            return !(p.x < min.x || p.y < min.y || p.x > max.x || p.y > max.y);
        }

        /// <summary>
        /// Is another AABB overlapping?
        /// </summary>
        /// <param name="other">The other AABB to check if its overlapping.</param>
        /// <returns>Is overlapping</returns>
        public bool Overlaps(AABB other)
        {
            return !(max.x < other.min.x || max.y < other.min.y || min.x > other.max.x || min.y > other.max.y);
        }

        /// <summary>
        /// Get the closest Vector3 point.
        /// </summary>
        /// <param name="p">The vector3 point.</param>
        /// <returns>The closest point in Vector3</returns>
        public Vector3 ClosestPoint(Vector3 p)
        {
            return Vector3.Clamp(p, min, max);
        }

    }
}
