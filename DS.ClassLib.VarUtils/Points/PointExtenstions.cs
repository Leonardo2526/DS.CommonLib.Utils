using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace DS.ClassLib.VarUtils.Points
{
    /// <summary>
    /// An object that represents extension methods for some types of points and vectors.
    /// </summary>
    public static class PointExtenstions
    {
        public static double DistanceTo(this PointModel basePoint, PointModel point)
        {
            double xDist = Math.Pow((basePoint.X - point.X), 2);
            double yDist = Math.Pow((basePoint.Y - point.Y), 2);
            double zDist = Math.Pow((basePoint.Z - point.Z), 2);

            return Math.Sqrt(xDist + yDist + zDist);
        }

        /// <summary>
        /// Specifies if <paramref name="point1"/> is less than <paramref name="point2"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if each coordinate of <paramref name="point1"/> is less than <paramref name="point2"/>.
        /// <para>
        /// Otherwise returns <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsLess(this Point3d point1, Point3d point2)
           => point1.X < point2.X || point1.Y < point2.Y || point1.Z < point2.Z;


        /// <summary>
        /// Specifies if <paramref name="point1"/> is greater than <paramref name="point2"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if each coordinate of <paramref name="point1"/> is greater than <paramref name="point2"/>.
        /// <para>
        /// Otherwise returns <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsGreater(this Point3d point1, Point3d point2)
           => point1.X > point2.X || point1.Y > point2.Y || point1.Z > point2.Z;

        /// <summary>
        ///  Rounds a <paramref name="vector"/> coordinate values to a specified number of fractional digits.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="digits"></param>
        /// <returns> The <see cref="Vector3d"/> nearest to coordinate value that contains a number of fractional digits equal to digits.</returns>
        public static Vector3d Round(this Vector3d vector, int digits = 5)
        {
            var x = Math.Round(vector.X, digits);
            var y = Math.Round(vector.Y, digits);
            var z = Math.Round(vector.Z, digits);

            return new Vector3d(x, y, z);
        }

        /// <summary>
        ///  Rounds a <paramref name="point"/> coordinate values to a specified number of fractional digits.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="digits"></param>
        /// <returns> The <see cref="Point3d"/> nearest to coordinate value that contains a number of fractional digits equal to digits.</returns>
        public static Point3d Round(this Point3d point, int digits = 5)
        {
            var x = Math.Round(point.X, digits);
            var y = Math.Round(point.Y, digits);
            var z = Math.Round(point.Z, digits);

            return new Point3d(x, y, z);
        }       

        /// <summary>
        /// Sort <paramref name="lst"/> by distance between points.
        /// </summary>
        /// <param name="lst"></param>
        /// <returns>
        /// List of <see cref="Point3d"/> with ordered distance between them from min to max.
        /// </returns>
        public static List<Point3d> SortByDistance(this List<Point3d> lst)
        {
            List<Point3d> output = new List<Point3d>();
            output.Add(lst[NearestPoint(new Point3d(0, 0, 0), lst)]);
            lst.Remove(output[0]);
            int x = 0;
            for (int i = 0; i < lst.Count + x; i++)
            {
                output.Add(lst[NearestPoint(output[output.Count - 1], lst)]);
                lst.Remove(output[output.Count - 1]);
                x++;
            }
            return output;

            int NearestPoint(Point3d srcPt, List<Point3d> lookIn)
            {
                KeyValuePair<double, int> smallestDistance = new KeyValuePair<double, int>();
                for (int i = 0; i < lookIn.Count; i++)
                {
                    double distance = srcPt.DistanceTo(lookIn[i]);
                    if (i == 0)
                    {
                        smallestDistance = new KeyValuePair<double, int>(distance, i);
                    }
                    else
                    {
                        if (distance < smallestDistance.Key)
                        {
                            smallestDistance = new KeyValuePair<double, int>(distance, i);
                        }
                    }
                }
                return smallestDistance.Value;
            }
        }

    }
}
