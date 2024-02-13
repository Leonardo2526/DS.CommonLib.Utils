namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object that represents builder to get <see cref="IItemResolveFactoryBuilder{TItem, TResult}"/>.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IItemResolveFactoryBuilder<TItem, TResult>
    {
        /// <summary>
        /// Create factory to resolve <typeparamref name="TItem"/> and get it's resolved <typeparamref name="TResult"/>.
        /// </summary>
        /// <returns></returns>
        IItemResolveFactory<TItem, TResult> Create();

    }
}
