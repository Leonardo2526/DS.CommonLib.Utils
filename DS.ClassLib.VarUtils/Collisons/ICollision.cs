namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// Represents intersection between objects.
    /// </summary>
    public interface ICollision
    {
    }


    /// <summary>
    /// Represents collsion(intersection) between <typeparamref name="TItem1"/> and <typeparamref name="TItem2"/>.
    /// </summary>
    /// <typeparam name="TItem1"></typeparam>
    /// <typeparam name="TItem2"></typeparam>
    public interface ICollision<TItem1, TItem2> : ICollision
    {
        /// <summary>
        /// First item of collision.
        /// </summary>
        TItem1 Item1 { get; }


        /// <summary>
        /// Second item of collision.
        /// </summary>
        TItem2 Item2 { get; }

    }
}
