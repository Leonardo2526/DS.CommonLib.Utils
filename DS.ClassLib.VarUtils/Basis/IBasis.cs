namespace DS.ClassLib.VarUtils.Basis
{
    /// <summary>
    /// An object that represents basis <typeparamref name="T"/> of vectors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBasis<T>
    {
        /// <summary>
        /// Basis X.
        /// </summary>
        public T X { get; }

        /// <summary>
        /// Basis Y.
        /// </summary>
        public T Y { get; }

        /// <summary>
        /// Basis Z.
        /// </summary>
        public T Z { get; }
    }
}