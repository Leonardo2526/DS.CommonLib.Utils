namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface is used to build <typeparamref name="TResult"/> from <typeparamref name="TItem"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IBuilder<TItem, TResult>
    {
        /// <summary>
        /// Create <typeparamref name="TResult"/> from <typeparamref name="TItem"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// <typeparamref name="TResult"/>.
        /// </returns>
        TResult Build(TItem item);
    }
}
