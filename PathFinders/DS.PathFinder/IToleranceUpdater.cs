namespace DS.PathFinder
{
    /// <summary>
    /// The interface to update tolerance.
    /// </summary>
    public interface IToleranceUpdater
    {
        /// <summary>
        /// Update tolerance with <paramref name="tolerance"/> value.
        /// </summary>
        /// <param name="tolerance"></param>
        void Update(int tolerance);
    }
}
