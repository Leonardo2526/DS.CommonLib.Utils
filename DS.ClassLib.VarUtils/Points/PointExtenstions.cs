using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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
            double xDist = Math.Pow((basePoint.X - point.X), 2);
            double yDist = Math.Pow((basePoint.Y - point.Y), 2);
            double zDist = Math.Pow((basePoint.Z - point.Z), 2);

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
            var x = point.X / Math.Abs(point.X);
            var y = point.Y / Math.Abs(point.Y);
            var z = point.Z / Math.Abs(point.Z);

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
        /// Convert <paramref name="point"/> to <see cref="Point3D"/>.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>
        /// A new <see cref="Point3D"/>.
        /// </returns>
        public static Point3D Convert(this Point3d point)
        {
            return new Point3D(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Convert <paramref name="point"/> to <see cref="Point3d"/>.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>
        /// A new <see cref="Point3d"/>.
        /// </returns>
        public static Point3d Convert(this Point3D point)
        {
            return new Point3d(point.X, point.Y, point.Z);
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
        public static bool IsLess(this Point3D point1, Point3D point2)
           => point1.X < point2.X || point1.Y < point2.Y || point1.Z < point2.Z;

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
        public static bool IsGreater(this Point3D point1, Point3D point2)
           => point1.X > point2.X || point1.Y > point2.Y || point1.Z > point2.Z;

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
        /// Convert <paramref name="vector"/> to <see cref="Vector3D"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>
        /// A new <see cref="Vector3D"/>.
        /// </returns>
        public static Vector3D Convert(this Vector3d vector)
        {
            return new Vector3D(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Convert <paramref name="vector"/> to <see cref="Vector3d"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>
        /// A new <see cref="Vector3d"/>.
        /// </returns>
        public static Vector3d Convert(this Vector3D vector)
        {
            return new Vector3d(vector.X, vector.Y, vector.Z);
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
        /// Convert <paramref name="vector"/>'s coordinates from <see cref="double"/> to <see cref="sbyte"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>New vector with coordinates convered to <see cref="sbyte"/>.</returns>
        public static Vector3D ConvertToSByte(this Vector3D vector)
        {
            return new Vector3D(System.Convert.ToSByte(vector.X), System.Convert.ToSByte(vector.Y), System.Convert.ToSByte(vector.Z));
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
        ///  Multiplies <paramref name="vectors"/> on <paramref name="multiplicator"/>.
        /// </summary>
        /// <param name="vectors"></param>
        /// <param name="multiplicator"></param>
        /// <returns>The multiplied list of <paramref name="vectors"/>.</returns>
        public static List<Vector3D> Multiply(this List<Vector3D> vectors, double multiplicator)
        {
            var multiDirections = new List<Vector3D>();
            vectors.ForEach(dir => multiDirections.Add(Vector3D.Multiply(dir, multiplicator)));
            return multiDirections;
        }

        /// <summary>
        ///  Multiplies <paramref name="vectors"/> on <paramref name="orthoBasis"/>.
        ///  <para>
        ///  If <paramref name="vectors"/> have any vector that is codirectional with any <paramref name="orthoBasis"/>,
        ///  its value will be multiplicated on codirectional <paramref name="orthoBasis"/> length.
        /// </para>
        /// <para>
        /// If vector from <paramref name="vectors"/> isn't codirectional with <paramref name="orthoBasis"/>, 
        /// its value will be multiplicated on <paramref name="orthoBasis"/> length that has mimimum angle with this vector. 
        /// </para>
        /// </summary>
        /// <param name="vectors"></param>
        /// <param name="orthoBasis"></param>
        /// <returns>The multiplied list of <paramref name="vectors"/>.</returns>
        public static List<Vector3D> Multiply(this List<Vector3D> vectors, OrthoBasis orthoBasis)
        {
            var multiDirections = new List<Vector3D>();

            var orthoVectors = new List<Vector3D>() { orthoBasis.X, orthoBasis.Y, orthoBasis.Z };
            foreach (var v in vectors)
            {
                (Vector3D minOrthovector, double angle) = GetWithMinAngle(v, orthoVectors);
                double multiplicator = minOrthovector.Length / Math.Cos(angle.DegToRad());
                multiDirections.Add(Vector3D.Multiply(v, multiplicator));
            }

            return multiDirections;
        }

        /// <summary>
        /// Get angles between <paramref name="vector"/> and each one from <paramref name="vectors"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="vectors"></param>
        /// <returns>
        /// List of angles.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public static List<double> GetAngles(this Vector3D vector, List<Vector3D> vectors)
        {
            var angles = new List<double>();
            vectors.ForEach(v => angles.Add(Vector3D.AngleBetween(vector, v)));
            return angles;
        }

        /// <summary>
        /// Get vector from <paramref name="vectors"/> with minimum abs angle value between it and <paramref name="vectors"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="vectors"></param>
        /// <returns>
        /// <see cref="Vector3D"/> with mimimum angle and angle value in degrees.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public static (Vector3D vector, double angle) GetWithMinAngle(this Vector3D vector, List<Vector3D> vectors)
        {
            Vector3D minVector = vectors.FirstOrDefault();
            double minAngle = 360;
            foreach (var v in vectors)
            {
                var angle = Math.Abs(Vector3D.AngleBetween(vector, v));

                //var negate = vector.DeepCopy();
                //negate.Negate();
                //var negateangle = Math.Abs(Vector3D.AngleBetween(negate, v));

                //angle = Math.Min(angle, negateangle);

                if (angle < minAngle) { minAngle = angle; minVector = v; }
            }
            return (minVector, minAngle);
        }

        public static bool IsBetweenPoints(this Point3D point, Point3D point1, Point3D point2, double tolerance = 3, bool canCoinsidence = true)
        {
            var v1 = (point - point1);
            if (canCoinsidence && Math.Round(v1.Length, 5) == 0) { return true; }
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

        /// <summary>
        /// Project <paramref name="v1"/> on <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>
        /// A new <see cref="Vector3D"/> that is projection of <paramref name="v1"/> on <paramref name="v2"/>.      
        /// </returns>
        public static Vector3D ProjectOn(this Vector3D v1, Vector3D v2)
        {
            var projVector = v2.DeepCopy();
            projVector.Normalize();

            var angle = Math.Abs(Vector3D.AngleBetween(v1, v2));
            if (angle > 90)
            {
                angle = 180 - angle;
                projVector.Negate();
            }
            var mult = v1.Length * Math.Cos(angle.DegToRad());
            projVector = Vector3D.Multiply(projVector, mult);

            return projVector;
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
