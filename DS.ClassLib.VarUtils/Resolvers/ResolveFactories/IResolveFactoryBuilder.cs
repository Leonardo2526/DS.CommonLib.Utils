namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object that represents builder to get <see cref="IResolveFactoryBuilder{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IResolveFactoryBuilder<TResult>
    {
        /// <summary>
        /// Create factory to resolve a taskand get it's resolved <typeparamref name="TResult"/>.
        /// </summary>
        /// <returns></returns>
        IResolveFactory<TResult> Create(int id = 0);

    }
}
