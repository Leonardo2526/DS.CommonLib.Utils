using QuickGraph;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// Represents builder for graph <typeparamref name="TVertex"/> vertices.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TTaggedVertex"></typeparam>
    public interface IVertexBuilder<TVertex, TTaggedVertex>
    {
        /// <summary>
        /// Tag value to add to incident edge.
        /// </summary>
        int EdgeTag { get; }

        /// <summary>
        /// Instansiate builder with <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph"></param>
        void Instansiate(AdjacencyGraph<TVertex, Edge<TVertex>> graph);

        /// <summary>
        /// Try to get <typeparamref name="TVertex"/> by <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        TVertex TryGetVertex(TTaggedVertex vertex);
    }
}