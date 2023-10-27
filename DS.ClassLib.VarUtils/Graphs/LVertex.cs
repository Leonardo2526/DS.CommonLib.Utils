using Rhino.Geometry;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// An object that represents fundamental unit (node) of graphs with its location.
    /// </summary>
    public class LVertex 
    {
        /// <summary>
        /// Instansiate an object that represents fundamental unit (node) of graphs with its location.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="location"></param>
        public LVertex(int id, Point3d location)
        {
            Id = id;
            Location = location;
        }

        /// <summary>
        /// Space location.
        /// </summary>
        public Point3d Location { get; }

        /// <summary>
        /// Unique id.
        /// </summary>
        public int Id { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is LVertex vertex &&
                   Id == vertex.Id;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -2003817546;
            hashCode = hashCode * -1521134295 + Location.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }


    }
}
