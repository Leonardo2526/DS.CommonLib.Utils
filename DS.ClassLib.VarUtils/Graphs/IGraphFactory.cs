namespace DS.RevitLib.Utils.Graphs
{
    /// <summary>
    /// The interface is used to create graph factories.
    /// </summary>
    public interface IGraphFactory<TGraph, TDataSource>
    {
        /// <summary>
        /// 
        /// </summary>
        public TGraph Graph { get; }

        /// <summary>
        /// Create graph with <paramref name="dataSource"/>.
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns>
        /// A new <typeparamref name="TGraph"/>.
        /// </returns>
        TGraph Create(TDataSource dataSource);
    }
}
