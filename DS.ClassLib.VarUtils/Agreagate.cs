namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The 'Aggregate' abstract class.
    /// </summary>
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    /// <summary>
    /// The 'Aggregate' generic abstract class.
    /// </summary>
    public abstract class Aggregate<T>
    {
        public abstract Iterator<T> CreateIterator();
    }
}
