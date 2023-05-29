namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface used to create factories to transfer objects.
    /// </summary>
    /// <typeparam name="TSource">Source object to transfer.</typeparam>
    /// <typeparam name="TTarget">Target place for transfer.</typeparam>
    public interface ITransferFactory<TSource, TTarget>
    {
        /// <summary>
        /// Transfer <paramref name="source"/> to <paramref name="target"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns>Transfered object.
        /// <para>
        /// Returns <see langword="null"/> if transfered was failed or <paramref name="source"/> is <see langword="null"/>.
        /// </para>
        /// </returns>
        TSource Transfer(TSource source, TTarget target);
    }

}
