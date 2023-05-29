using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The 'Iterator' abstract class.
    /// </summary>
    public abstract class Iterator
    {
        /// <summary>
        /// Gets first iteration item.
        /// </summary>
        /// <returns>First iteration item.</returns>
        public abstract object First();

        /// <summary>
        /// Gets next iteration item.
        /// </summary>
        /// <returns>Next iteration item.</returns>
        public abstract object Next();

        /// <summary>
        /// Gets whether iterations are complete
        /// </summary>
        /// <returns><see langword="true"/> if iterations are complete. Otherwise returns <see langword="false"/>.</returns>
        public abstract bool IsDone();

        /// <summary>
        /// Gets current iteration item.
        /// </summary>
        /// <returns>Current iteration item.</returns>
        public abstract object CurrentItem();
    }

    /// <summary>
    /// The 'Iterator' generic abstract class.
    /// </summary>
    public abstract class Iterator<T>
    {
        /// <summary>
        /// Gets first iteration item.
        /// </summary>
        /// <returns>First iteration item.</returns>
        protected abstract T First();

        /// <summary>
        /// Gets previous iteration item.
        /// </summary>
        /// <returns>Previous iteration item.</returns>
        protected abstract T Previous();

        /// <summary>
        /// Gets next iteration item.
        /// </summary>
        /// <returns>Next iteration item.</returns>
        protected abstract T Next();

        /// <summary>
        /// Gets whether iterations are complete
        /// </summary>
        /// <returns><see langword="true"/> if iterations are complete. Otherwise returns <see langword="false"/>.</returns>
        protected abstract bool IsDone();

        /// <summary>
        /// Gets current iteration item.
        /// </summary>
        /// <returns>Current iteration item.</returns>
        protected abstract T CurrentItem();
    }

    /// <summary>
    /// The 'Iterator' generic abstract class.
    /// </summary>
    public abstract class IteratorAsync<T>
    {
        /// <summary>
        /// Gets first iteration item.
        /// </summary>
        /// <returns>First iteration item.</returns>
        public abstract T First();

        /// <summary>
        /// Gets previous iteration item asyncronously.
        /// </summary>
        /// <returns>Previous iteration item.</returns>
        public abstract Task<T> PreviousAsync();

        /// <summary>
        /// Gets next iteration item asyncronously.
        /// </summary>
        /// <returns><see cref="Task{TResult}"/> of next iteration item.</returns>
        public abstract Task<T> NextAsync();

        /// <summary>
        /// Gets whether iterations are complete
        /// </summary>
        /// <returns><see langword="true"/> if iterations are complete. Otherwise returns <see langword="false"/>.</returns>
        public abstract bool IsDone();

        /// <summary>
        /// Gets current iteration item.
        /// </summary>
        /// <returns>Current iteration item.</returns>
        public abstract T CurrentItem();
    }
}
