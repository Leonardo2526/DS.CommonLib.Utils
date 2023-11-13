using QuickGraph;
using System.ComponentModel.DataAnnotations;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// Validator for verices.
    /// </summary>
    public interface IVertexLimitsValidator : IValidatableObject
    {
        /// <summary>
        /// Instansiate an object to valid verices.
        /// </summary>
        /// <param name="graph"></param>
        void Instansiate(AdjacencyGraph<IVertex, Edge<IVertex>> graph);

        /// <summary>
        /// Set <paramref name="parentVertex"/> for validation.
        /// </summary>
        /// <param name="parentVertex"></param>
        /// <returns></returns>
        IVertexLimitsValidator SetParent(IVertex parentVertex);
    }
}
