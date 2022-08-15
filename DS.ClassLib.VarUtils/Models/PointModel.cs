namespace DS.ClassLib.VarUtils
{
    public struct PointModel 
    {
        public int X, Y, Z;
        public PointModel(int x, int y, int z)
        {
            this.X = x; 
            this.Y = y;
            this.Z = z; 
        }

        public override bool Equals(object obj)
        {
            return obj is PointModel model &&
                   X == model.X &&
                   Y == model.Y &&
                   Z == model.Z;
        }

        public override int GetHashCode()
        {
            int hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }
    }

}
