using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The 'Iterator' abstract class.
    /// </summary>
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

    /// <summary>
    /// The 'Iterator' generic abstract class.
    /// </summary>
    public abstract class Iterator<T>
    {
        protected abstract T First();
        protected abstract T Previous();
        protected abstract T Next();
        protected abstract bool IsDone();
        protected abstract T CurrentItem();
    }

    /// <summary>
    /// The 'Iterator' generic abstract class.
    /// </summary>
    public abstract class IteratorAsync<T>
    {
        public abstract T First();
        public abstract T Previous();
        public abstract Task<T> NextAsync();
        public abstract bool IsDone();
        public abstract T CurrentItem();
    }
}
