namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// The interface is used to create tasks to resolve.
    /// </summary>
    /// <typeparam name="TTask"></typeparam>
    public interface ITaskCreator<TTask>
    {
        /// <summary>
        /// Create task to resolve.
        /// </summary>
        /// <returns></returns>
        TTask CreateTask();
    }
}
