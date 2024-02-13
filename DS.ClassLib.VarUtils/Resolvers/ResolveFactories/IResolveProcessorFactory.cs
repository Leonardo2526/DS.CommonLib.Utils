namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object that represents factory to create <see cref="IResolveProcessor{TResult}"/> by <typeparamref name="TItem"/>.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IResolveProcessorFactory<TItem, TResult>
    {
        /// <summary>
        /// Create <see cref="IResolveProcessor{TResult}"/> by <typeparamref name="TItem"/> .
        /// </summary>
        /// <returns></returns>
        IResolveProcessor<TResult> Create(TItem item);
    }
}
