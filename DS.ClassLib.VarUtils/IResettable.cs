namespace DS.ClassLib.VarUtils
{
    /// <summary>
    ///Provides a mechanism to set objects values to its default states.
    /// </summary>
    public interface IResettable
    {
        /// <summary>
        /// Try to set objects values to its default states.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if reset was successful.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        bool TryReset();
    }
}
