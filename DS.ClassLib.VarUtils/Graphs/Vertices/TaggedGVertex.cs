using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// An object that represents fundamental unit (node) of graphs with tag.
    /// </summary>
    public struct TaggedGVertex<TTag> : IVertex
    {
        /// <summary>
        /// Instansiate an object that represents fundamental unit (node) of graphs with tag.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tag"></param>
        public TaggedGVertex(int id, TTag tag)
        {
            Id = id;
            Tag = tag;
        }

        /// <summary>
        /// Tagged object.
        /// </summary>
        public TTag Tag { get; }

        /// <inheritdoc/>
        public int Id { get; }

        public override bool Equals(object obj)
        {
            return obj is TaggedGVertex<TTag> vertex &&
                   base.Equals(obj) &&
                   Id == vertex.Id &&
                   EqualityComparer<TTag>.Default.Equals(Tag, vertex.Tag);
        }

        public override int GetHashCode()
        {
            int hashCode = -1880457927;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TTag>.Default.GetHashCode(Tag);
            return hashCode;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
