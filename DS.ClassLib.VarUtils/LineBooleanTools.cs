using Castle.Core.Internal;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents boolean operations between <see cref="Line"/>s.
    /// </summary>
    public static class LineBooleanTools
    {
        private static Line Intersect2(Line line1, Line line2, double tolerance = 0.001)
        {
            var ad = line1.To - line1.From;
            var bc = line2.To - line2.From;

            if (ad.IsParallelTo(bc) == 0) { return default; }

            var cd = line1.To - line2.To;

            var ab = ad - (bc + cd);
            var ac = ad - cd;

            var p1 = line1.From + ab;
            var p2 = line1.From + ac;

            var c1 = line1.Contains(p1, tolerance);
            var c2 = line1.Contains(p2, tolerance);
            if (!c1 && !c2) { return default; }

            p1 = c1 ? p1 : line1.From;
            p2 = c2 ? p2 : line1.To;

            return new Line(p1, p2);
        }

        /// <summary>
        /// Find intersection between <paramref name="line1"/> ands <paramref name="line2"/>.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// A new <see cref="Line"/> that has points that <paramref name="line1"/> and <paramref name="line2"/> contains.
        /// <para>
        /// <see cref="Line"/> default value if lines are not parallel.
        /// </para>
        /// </returns>
        public static Line Intersect(Line line1, Line line2, double tolerance = 0.001)
        {
            var allPoins = new List<Point3d>();

            if (line1.Contains(line2.From, tolerance))
            { allPoins.Add(line2.From); }

            if (line1.Contains(line2.To, tolerance))
            { allPoins.Add(line2.To); }

            if (allPoins.Count != 2 && line2.Contains(line1.From, tolerance))
            { allPoins.Add(line1.From); }

            if (allPoins.Count != 2 && line2.Contains(line1.To, tolerance))
            { allPoins.Add(line1.To); }

            if (allPoins.Count < 2) { return default; }

            return new Line(allPoins[0], allPoins[1]);
        }

        /// <summary>
        /// Substact <paramref name="line2"/> from <paramref name="line1"/>.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// Substraction <see cref="Line"/>.
        /// </returns>
        public static List<Line> Substract(Line line1, Line line2, double tolerance = 0.001)
        {
            var lines = new List<Line>();

            var at = 3.DegToRad();
            if (line1.Direction.IsParallelTo(line2.Direction, at) == 0)
            { lines.Add(line1); return lines; }

            var allPoins = new List<Point3d>();
            int insidePoints = 0;
            if (!line2.Contains(line1.From, tolerance))
            { allPoins.Add(line1.From); }
            else { insidePoints++; }
            if (!line2.Contains(line1.To, tolerance))
            { allPoins.Add(line1.To); }
            else { insidePoints++; }

            if (insidePoints == 2)
            { return lines; }
            if (allPoins.Count == 0)
            { lines.Add(line1); return lines; }

            if (line1.Contains(line2.From, tolerance, false))
            { allPoins.Add(line2.From); }
            if (line1.Contains(line2.To, tolerance, false))
            { allPoins.Add(line2.To); }

            if (allPoins.Count < 2) { lines.Add(line1); return lines; }

            //create lines by points
            allPoins = allPoins.OrderBy(p => p.DistanceTo(line1.From)).ToList();
            for (int i = 0; i < allPoins.Count; i += 2)
            {
                var line = new Line(allPoins[i], allPoins[i + 1]);
                lines.Add(line);
            }

            return lines;
        }

        /// <summary>
        /// Substract <paramref name="deductionLines"/> from <paramref name="minuendLine"/>.
        /// </summary>
        /// <param name="minuendLine"></param>
        /// <param name="deductionLines"></param>
        /// <param name="toleranceDigits"></param>
        /// <returns>
        /// List of substracted <see cref="Line"/>s.
        /// </returns>
        public static List<Line> Substract(Line minuendLine, List<Line> deductionLines, int toleranceDigits = 3)
        {
            var open = new Stack<Line>();
            var close = new Stack<Line>();

            var tolerance = Math.Pow(0.1, toleranceDigits);

            var dLines = new List<Line>();
            dLines.AddRange(deductionLines);

            open.Push(minuendLine);
            while (open.Count != 0)
            {
                var current = open.Pop().Round(toleranceDigits);
                var passed = true;
                for (int i = 0; i < dLines.Count; i++)
                {
                    Line dl = dLines[i];
                    var result = Substract(current, dl, tolerance);
                    if(result.Count == 1 && result[0].Round(toleranceDigits).Equals(current))
                    { continue; }
                    else
                    {
                        result.ForEach(open.Push);
                        passed = false;
                        break;
                    }                  
                }
                if(passed) 
                { close.Push(current); }
            }

            return close.ToList();
        }
    }
}
