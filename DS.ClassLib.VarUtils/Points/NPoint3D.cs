using System;

namespace DS.ClassLib.VarUtils.Points
{
    /// <summary>
    /// Represents an x-, y-, and z- natural numbers as coordinate point in 3-D space.
    /// </summary>
    public struct NPoint3D : IFormattable
    {
        /// <summary>
        /// Instantiate a <see langword="struct"/> that represents an x-, y-, and z- natural numbers as coordinate point in 3-D space.
        /// </summary>
        /// <remarks>
        /// <see cref="ArgumentException"/> will be thrown if one of the coordinates is not a natural number.
        /// </remarks>
        /// <param name="x">X value of the point.</param>
        /// <param name="y">Y value of the point.</param>
        /// <param name="z">Z value of the point.</param>
        public NPoint3D(int x, int y, int z)
        {
            if (!x.IsNatural() || !y.IsNatural() || !z.IsNatural())
            { throw new ArgumentException("One of the coordintates is not a natural number"); }

            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets the x-coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets the y-coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets the z-coordinate.
        /// </summary>
        public int Z { get; set; }

        /// <inheritdoc/>
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return ConvertToString(format, formatProvider);
        }

        internal readonly string ConvertToString(string format, IFormatProvider provider)
        {
            char numericListSeparator = ',';
            return string.Format(provider, "({1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "})", numericListSeparator, X, Y, Z);
        }

    }
}
