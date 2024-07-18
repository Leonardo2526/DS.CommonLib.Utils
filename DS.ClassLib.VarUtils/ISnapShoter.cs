using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface is used to create snapshots.
    /// </summary>
    /// <typeparam name="TSnapShot"></typeparam>
    public interface ISnapShoter<TSnapShot>
    {
        /// <summary>
        /// Make a snapshot.
        /// </summary>
        /// <returns>
        /// The <typeparamref name="TSnapShot"/>.
        /// </returns>
        IEnumerable<TSnapShot> MakeSnapShot();
    }
}
