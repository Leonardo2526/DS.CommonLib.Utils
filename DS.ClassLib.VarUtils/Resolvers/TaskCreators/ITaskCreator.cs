namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// The interface is used to create tasks to resolve.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TTask"></typeparam>
    public interface ITaskCreator<TItem, TTask>
    {
        /// <summary>
        /// Create task to resolve.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TTask CreateTask(TItem item);
    }
}
