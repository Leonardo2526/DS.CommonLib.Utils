using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface used for trace settings.
    /// </summary>
    public interface ITraceSettings
    {
        /// <summary>
        /// Elbow angle.
        /// </summary>
        double A { get; set; }

        /// <summary>
        /// Elbow radius.
        /// </summary>
        double R { get; set; }

        /// <summary>
        /// Distance between elements.
        /// </summary>
        double B { get; set; }

        /// <summary>
        /// Distance between elements.
        /// </summary>
        double C { get; set; }

        /// <summary>
        /// Distance between elements joints.
        /// </summary>
        double D { get; set; }

        /// <summary>
        /// Distance between trace nodes.
        /// </summary>
        double F { get; set; }

        /// <summary>
        /// Distance between elements and floor.
        /// </summary>
        double H { get; set; }

        /// <summary>
        /// Available elbows angles.
        /// </summary>
        public List<int> AList { get; }

        /// <summary>
        /// PathFind step
        /// </summary>
        double Step { get; set; }
    }
}
