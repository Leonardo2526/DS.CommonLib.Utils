using Rhino.Geometry;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// The interface that represents fundamental unit (node) of graphs with specified location.
    /// </summary>
    /// <typeparam name="TLocation"></typeparam>
    public interface ILocationVertex<TLocation>
    {
        /// <summary>
        /// 
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        TLocation Location { get; }
    }
}