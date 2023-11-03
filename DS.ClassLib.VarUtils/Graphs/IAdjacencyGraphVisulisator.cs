using QuickGraph;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// Visualisator object to show graph.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    public interface IAdjacencyGraphVisulisator<TVertex> : IVisualisator
    {
        /// <summary>
        /// Build visualisator to show <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>
        /// Visualisator that is ready to show <paramref name="graph"/>.
        /// </returns>
        IAdjacencyGraphVisulisator<TVertex> Build(AdjacencyGraph<TVertex, Edge<TVertex>> graph);
    }
}
