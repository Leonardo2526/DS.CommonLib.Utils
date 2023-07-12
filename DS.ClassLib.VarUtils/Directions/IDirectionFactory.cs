using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Directions
{
    public interface IDirectionFactory
    {
        List<Vector3D> Directions { get; }

        IDirectionFactory Build(Vector3D main, Vector3D normal, List<int> angles = null);

        List<Vector3D> GetDirections();
    }
}
