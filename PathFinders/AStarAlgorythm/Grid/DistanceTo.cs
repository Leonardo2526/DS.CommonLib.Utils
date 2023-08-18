using DS.PathSearch;
using System;

namespace AStarAlgorythm
{
    struct DistanceTo
    {
        Location Point1;
        Location Point2;

        public double Distance { get; set; }

        public DistanceTo(Location point1, Location point2)
        {
            Point1 = point1;
            Point2 = point2;

            double Xsqw = Math.Pow(Math.Abs(Point1.X - Point2.X), 2);
            double Ysqw = Math.Pow(Math.Abs(Point1.Y - Point2.Y), 2);
            double Zsqw = Math.Pow(Math.Abs(Point1.Z - Point2.Z), 2);

            Distance = Math.Sqrt(Xsqw + Ysqw + Zsqw);
        }
    }
}