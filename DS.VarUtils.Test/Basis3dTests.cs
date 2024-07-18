using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.VarUtils.Test
{
    internal class Basis3dTests
    {
        private Basis3d _originBasis = new(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis);

        private readonly static Basis3d[] _checkBases =
           {
            new(new Point3d(1, 0, 0), Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis),
            new(Point3d.Origin, Vector3d.YAxis, Vector3d.YAxis, Vector3d.ZAxis)
        };


        [Test]
        public void EqualBasis_001BasesAreEqual_ShouldPass()
        {
            var basis1 = _originBasis;

            var origin = Point3d.Origin;
            var x = Vector3d.XAxis;
            var y = Vector3d.YAxis;
            var z = Vector3d.ZAxis;
            var basis2 = new Basis3d(origin, x, y, z);
            Assert.That(basis1 == basis2);
        }
        [Test]
        [TestCaseSource(nameof(_checkBases))]
        public void NotEqualBasis_002BasesAreNotEqual_ShouldPass(Basis3d checkBasis)
            => Assert.That(checkBasis != _originBasis);

        [Test]
        [TestCaseSource(nameof(_checkBases))]
        public void NotEqualBasis_003BasesAreNotEqual_ShouldFail(Basis3d checkBasis)
            => Assert.That(_originBasis, Is.Not.EqualTo(checkBasis));
    }

    
}
