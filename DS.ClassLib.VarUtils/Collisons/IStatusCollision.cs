namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// Represents collsion(intersection) between <typeparamref name="TItem1"/> and <typeparamref name="TItem2"/>
    /// that has advanced functionality like <see cref="CollisionStatus"/>.
    /// </summary>
    /// <typeparam name="TItem1"></typeparam>
    /// <typeparam name="TItem2"></typeparam>
    public interface IStatusCollision<TItem1, TItem2> : ICollision<TItem1, TItem2>
    {
        /// <summary>
        /// Collision id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Current collision staus.
        /// </summary>
        CollisionStatus Status { get; }

        /// <summary>
        /// Show collision in document.
        /// </summary>
        void Show();
    }

}