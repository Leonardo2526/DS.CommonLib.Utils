using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.GridMap
{
    public interface IGraph
    {
        Point3D Start { get; set; }
        Point3D Goal { get; set; }
        int[,] WeightMatrix { get; set; }
        int[,,] Matrix { get; set; }
    }
}
