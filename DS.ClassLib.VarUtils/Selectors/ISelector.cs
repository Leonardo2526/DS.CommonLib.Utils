namespace DS.ClassLib.VarUtils.Selectors
{
    /// <summary>
    /// The interface is used to select <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISelector<T>
    {
        /// <summary>
        /// Select <typeparamref name="T"/> object.
        /// </summary>
        /// <returns>
        /// Selected <typeparamref name="T"/> object.
        /// </returns>
        T Select();
    }
}
