using Rhino.Geometry;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <inheritdoc/>
    public class TaggedLVertex<TTag> : LVertex
    {
        /// <inheritdoc/>
        public TaggedLVertex(int id, Point3d location, TTag tag) : base(id, location)
        {
            Tag = tag;
        }

        /// <summary>
        /// Tagged object.
        /// </summary>
        public TTag Tag { get; }
    }
}
