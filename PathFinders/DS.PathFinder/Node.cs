using DS.PathSearch;

namespace FrancoGustavo
{
    struct Node
    {
        public Node(int id, int lengthToBase, Location location)
        {
            Id = id;
            LengthToBase = lengthToBase;
            Location = location;
        }

        public int Id { get; set; }
        public int LengthToBase { get; set; }
        public Location Location { get; set; }
    }
}
