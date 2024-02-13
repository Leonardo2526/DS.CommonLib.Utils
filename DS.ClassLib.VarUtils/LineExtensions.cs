using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Extension methods for <see cref="Line"/>.
    /// </summary>
    public static class LineExtensions
    {

        /// <summary>
        /// Specifies if <paramref name="point"/> is inside <paramref name="line"/> bounds with specified <paramref name="tolerance"/>.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="point"></param>
        /// <param name="tolerance"></param>
        /// <param name="includeFinitePoints"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="point"/> lies on <paramref name="line"/> or 
        /// coinsident with <paramref name="line"/>'s end points.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool Contains(this Line line, Point3d point, 
            double tolerance = 0.001, bool includeFinitePoints = true)
        {
            if (!includeFinitePoints)
            {
                if (line.From.DistanceTo(point) < tolerance ||
                    line.To.DistanceTo(point) < tolerance)
                { return false; }
            }

            var dist = line.DistanceTo(point, true);
            return dist < tolerance;
        }

        /// <summary>
        ///  Rounds a <paramref name="line"/> coordinate values to a specified number of fractional digits.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="digits"></param>
        /// <returns> A new <see cref="Line"/> nearest to coordinate values that contains a number of fractional digits equal to digits.</returns>
        public static Line Round(this Line line, int digits = 5)
        => new(line.From.Round(digits), line.To.Round(digits));

        /// <summary>
        /// Check if <paramref name="line1"/> overlap <paramref name="line2"/> or vice versa
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns>
        /// <see langword="true"/> if overlap.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsOverlapped(this Line line1, Line line2)
        {
            var subStructionLines = LineBooleanTools.Substract(line1, line2);
            if(subStructionLines.Count == 0 || subStructionLines.Count > 1) { return true; }
            return subStructionLines.Count == 1 && subStructionLines.First().Length < line1.Length;
        }
        
    }
}
