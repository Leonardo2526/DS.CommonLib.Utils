namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// The interface used to show <typeparamref name="TCollision"/>.
    /// </summary>
    /// <typeparam name="TCollision"></typeparam>
    public interface ICollisionVisualizator<TCollision> where TCollision : ICollision
    {
        /// <summary>
        /// Show <paramref name="collision"/> in document.
        /// </summary>
        /// <param name="collision">Collision to show.</param>
        void Show(TCollision collision);
    }
}
