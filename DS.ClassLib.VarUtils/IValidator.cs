namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface represents validator for <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> where T : class
    {
        /// <summary>
        /// Specifies if <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsValid(T value);
    }
}
