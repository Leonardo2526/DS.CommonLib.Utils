using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// The interface used to build <typeparamref name="TCollision"/>s.
    /// </summary>
    /// <typeparam name="TItem1"></typeparam>
    /// <typeparam name="TITem2"></typeparam>
    /// <typeparam name="TCollision"></typeparam>
    public interface ICollisionFactory<TItem1, TITem2, TCollision> where TCollision : ICollision
    {
        /// <summary>
        /// Created <typeparamref name="TCollision"/>s.
        /// </summary>
        IEnumerable<TCollision> Collisions { get; }

        /// <summary>
        /// Build collision between <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1">First element</param>
        /// <param name="item2"></param>
        /// <returns>
        /// Collision beteween <paramref name="item1"/> and <paramref name="item2"/>.
        /// <para>
        /// Returns <see langword="null"/> if <paramref name="item1"/> and <paramref name="item2"/> have no intersections.
        /// </para>
        /// </returns>
        TCollision CreateCollision(TItem1 item1, TItem1 item2);
    }
}