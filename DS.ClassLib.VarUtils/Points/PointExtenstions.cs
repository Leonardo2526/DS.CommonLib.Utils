using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
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
            double xDist = Math.Pow((basePoint.X - point.X) ,2);
            double yDist = Math.Pow((basePoint.Y - point.Y) ,2);
            double zDist = Math.Pow((basePoint.Z - point.Z) ,2);

            return Math.Sqrt(xDist + yDist + zDist);
        }

        /// <summary>
        /// Calculate the distance from this point to the specified point.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns>The real number equal to the distance between the two points.</returns>
        public static double DistanceTo(this Point3D point1, Point3D point2)
        {
            double xDist = Math.Pow((point1.X - point2.X), 2);
            double yDist = Math.Pow((point1.Y - point2.Y), 2);
            double zDist = Math.Pow((point1.Z - point2.Z), 2);

            return Math.Sqrt(xDist + yDist + zDist);
        }

        /// <summary>
        /// Creates a new <see cref="Point3D"/> whose coordinates are the normalized values from this vector.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>The normalized <see cref="Point3D"/> or zero if the vector is almost Zero.</returns>
        public static Point3D Normilize(this Point3D point)
        {
            var x = point.X/Math.Abs(point.X);
            var y = point.Y/Math.Abs(point.Y);
            var z = point.Z/Math.Abs(point.Z);

            return new Point3D(x, y, z);
        }

        /// <summary>
        ///  Rounds a <paramref name="point"/> coordinate values to a specified number of fractional digits.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="digits"></param>
        /// <returns> The <see cref="Point3D"/> nearest to coordinate value that contains a number of fractional digits equal to digits.</returns>
        public static Point3D Round(this Point3D point, int digits = 5)
        {
            var x = Math.Round(point.X, digits);
            var y = Math.Round(point.Y, digits);
            var z = Math.Round(point.Z, digits);

            return new Point3D(x, y, z);
        }

        /// <summary>
        ///  Rounds a <paramref name="vector"/> coordinate values to a specified number of fractional digits.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="digits"></param>
        /// <returns> The <see cref="Vector3D"/> nearest to coordinate value that contains a number of fractional digits equal to digits.</returns>
        public static Vector3D Round(this Vector3D vector, int digits = 5)
        {
            var x = Math.Round(vector.X, digits);
            var y = Math.Round(vector.Y, digits);
            var z = Math.Round(vector.Z, digits);

            return new Vector3D(x, y, z);
        }

        /// <summary>
        /// Convert <paramref name="vector"/>'s coordinates from <see cref="double"/> to <see cref="sbyte"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>New vector with coordinates convered to <see cref="sbyte"/>.</returns>
        public static Vector3D ConvertToSByte(this Vector3D vector)
        {
            return new Vector3D(Convert.ToSByte(vector.X), Convert.ToSByte(vector.Y), Convert.ToSByte(vector.Z));
        }        

        /// <summary>
        /// Determines whether 2 vectors are the same within the given tolerance.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="tolerance"></param>
        /// <returns> True if the vectors are the same; otherwise, false.</returns>
        public static bool IsAlmostEqualTo(this Vector3D vector1, Vector3D vector2, int tolerance = 5)
        {
            vector1 = vector1.Round(tolerance);
            vector2 = vector2.Round(tolerance);

            return vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
        }

        /// <summary>
        ///  Multiplies this <paramref name="point"/> by the specified value and returns the result.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="multiplicator"></param>
        /// <returns>The multiplied <paramref name="point"/> .</returns>
        public static Point3D Multiply(this Point3D point, double multiplicator)
        {
            return new Point3D(point.X * multiplicator, point.Y * multiplicator, point.Z * multiplicator);
        }

        /// <summary>
        ///  Multiplies this <paramref name="vector"/> by the specified value and returns the result.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="multiplicator"></param>
        /// <returns>The multiplied <paramref name="vector"/> .</returns>
        public static Vector3D Multiply(this Vector3D vector, double multiplicator)
        {
            return new Vector3D(vector.X * multiplicator, vector.Y * multiplicator, vector.Z * multiplicator);
        }


        public static bool IsBetweenPoints(this Point3D point, Point3D point1, Point3D point2, double tolerance = 3, bool canCoinsidence = true)
        {   
            var v1 = (point - point1);
            if(canCoinsidence && Math.Round(v1.Length, 5) == 0) { return true; }
            v1.Normalize();

            var v2 = (point - point2);
            if (canCoinsidence && Math.Round(v2.Length, 5) == 0) { return true; }
            v2.Normalize();
            v2.Negate();

            if (v1.IsAlmostEqualTo(v2, (int)tolerance))
            {
                return true;
            }
            return false;
        }
     
    }
}
