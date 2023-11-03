namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// An object that represents fundamental unit (node) of graphs.
    /// </summary>
    public struct GVertex : IVertex
    {
        /// <summary>
        /// Instansiate an object that represents fundamental unit (node) of graphs.
        /// </summary>
        /// <param name="id"></param>
        public GVertex(int id)
        {
            Id = id;
        }

        /// <inheritdoc/>
        public int Id { get; }

        public override bool Equals(object obj)
        {
            return obj is GVertex vertex &&
                   Id == vertex.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
