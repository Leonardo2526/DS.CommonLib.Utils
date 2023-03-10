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
        public abstract T First();
        public abstract T Next();
        public abstract bool IsDone();
        public abstract T CurrentItem();
    }
}
