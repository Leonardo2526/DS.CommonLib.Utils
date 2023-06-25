using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.GridMap
{
    public interface IMap
    {
        Point3D MinPoint { get;  }
        Point3D MaxPoint { get;  }
    }
}
