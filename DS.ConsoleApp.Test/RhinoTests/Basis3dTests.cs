using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using DS.ClassLib.VarUtils.Points;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal static class Basis3dTests
    {
        public static Basis3d ConvertBasis()
        {
            var basis = CreateNormRoundBasis();
            Console.WriteLine("Basis is: \n" + basis);
            PrintAngles(basis);
            Console.WriteLine("IsOrthonormal: " + basis.IsOrthonormal());
            Console.WriteLine("IsRighthanded: " + basis.IsRighthanded());           

            var aproxBasis = basis.ToRightandedOrthonormal();
            //var aproxBasis = new Basis3d(basis.X, vyAprox, vzAprox);
            Console.WriteLine();
            Console.WriteLine("\nAproximate basis is: \n" + aproxBasis);
            PrintAngles(aproxBasis);
            PrintUnits(aproxBasis);
            Console.WriteLine("IsOrthonormal: " + aproxBasis.IsOrthonormal());
            Console.WriteLine("IsRighthanded: " + aproxBasis.IsRighthanded());

            TryCreateTransforms(aproxBasis);

            return aproxBasis;
        }

        private static void PrintAngles(Basis3d basis)
        {
            var a_vxvy = Vector3d.VectorAngle(basis.X, basis.Y).RadToDeg();
            var a_vxvz = Vector3d.VectorAngle(basis.X, basis.Z).RadToDeg();
            var a_vyvz = Vector3d.VectorAngle(basis.Y, basis.Z).RadToDeg();
            Console.WriteLine("\nAngles.");
            Console.WriteLine("Angle vx - vy: " + a_vxvy + $". Delta = {90 - a_vxvy}");
            Console.WriteLine("Angle vx - vz: " + a_vxvz + $". Delta = {90 - a_vxvz}");
            Console.WriteLine("Angle vy - vz: " + a_vyvz + $". Delta = {90 - a_vyvz}");
        }

        public static void TestBasisOrthonormal()
        {
            //var basis = CreateSimpleBasis();
            var basis = CreateNormRoundBasis();
            //basis = basis.Round();
            basis = basis.Normalize();
            //basis = NormalizeTo(basis);

            Console.WriteLine("Basis is: \n" + basis);

            //check units
            PrintUnits(basis);

            //check angles
            var a_vxvy = Vector3d.VectorAngle(basis.X, basis.Y).RadToDeg();
            var a_vxvz = Vector3d.VectorAngle(basis.X, basis.Z).RadToDeg();
            var a_vyvz = Vector3d.VectorAngle(basis.Y, basis.Z).RadToDeg();
            Console.WriteLine("\nAngles.");
            Console.WriteLine("Angle vx - vy: " + a_vxvy + $". Delta = {90 - a_vxvy}");
            Console.WriteLine("Angle vx - vz: " + a_vxvz + $". Delta = {90 - a_vxvz}");
            Console.WriteLine("Angle vy - vz: " + a_vyvz + $". Delta = {90 - a_vyvz}");


            Console.WriteLine("\nCheck orthonormals.");

            Console.WriteLine("IsOrthonormal: " + basis.IsOrthonormal());
            Console.WriteLine("IsRighthanded: " + basis.IsRighthanded());

            //TryCreateTransforms(basis);
        }

        private static void PrintUnits(Basis3d basis)
        {
            Console.WriteLine("\nUnits.");
            Console.WriteLine("IsUnitVector basisX: " + basis.X.IsUnitVector);
            Console.WriteLine("IsUnitVector basisY: " + basis.Y.IsUnitVector);
            Console.WriteLine("IsUnitVector basisZ: " + basis.Z.IsUnitVector);
        }

        public static void TryCreateTransforms(Basis3d targetBasis)
        {
            Console.WriteLine("\nTransforms: ");
            var sourceBasis = CreateOriginBasis();
            GetTransforms(sourceBasis, targetBasis);
        }

        public static void TryCreateTransforms1()
        {
            var sourceBasis = CreateBasis1();
            var targetBasis = CreateBasis2();
            GetTransforms(sourceBasis, targetBasis);
        }


        public static Basis3d NormalizeTo(Basis3d basis)
        {
            var b = basis.X.Unitize() && basis.Y.Unitize() && basis.Z.Unitize();
            if (!b) { Console.WriteLine("Failed to Utilze!!!"); }
            return new Basis3d(basis.X, basis.Y, basis.Z);
        }

        public static bool Normalize(Basis3d basis)
           => basis.X.Unitize() && basis.Y.Unitize() && basis.Z.Unitize();


        public static void GetTransforms(this Basis3d sourceBasis, Basis3d targetBasis)
        {
            sourceBasis = sourceBasis.Round();
            targetBasis = targetBasis.Round();

            sourceBasis = sourceBasis.ToRightandedOrthonormal();
            targetBasis = targetBasis.ToRightandedOrthonormal();


            Console.WriteLine("sourceBasis: " + sourceBasis);
            Console.WriteLine("targetBasis: " + targetBasis);

            if (!sourceBasis.IsRighthanded())
            { throw new ArgumentException("sourceBasis is not Righthanded"); }
            if (!sourceBasis.IsOrthonormal())
            { throw new ArgumentException("sourceBasis is not Orthonormal"); }
            if (!targetBasis.IsRighthanded())
            { throw new ArgumentException("targetBasis is not Righthanded"); }
            if (!targetBasis.IsOrthonormal())
            { throw new ArgumentException("targetBasis is not Orthonormal"); }

            Rhino.Geometry.Transform transform = sourceBasis.GetTransform(targetBasis);
            if(!transform.GetEulerZYZ(out double alpha1, out double beta1, out double gamma1))
            { throw new ArgumentException("failed"); }

            double alpha = Math.Round(alpha1.RadToDeg(), 1);
            double beta = Math.Round(beta1.RadToDeg() ,1);
            double gamma = Math.Round(gamma1.RadToDeg(), 1);

            Console.WriteLine("alpha: " + alpha);
            Console.WriteLine("beta: " + beta);
            Console.WriteLine("beta: " + gamma);
        }

        private static Basis3d CreateNormBasis()
        {
            var vx = new Vector3d(-0.5720614028176878, -0.41562693777745152, 0.70710678118654591);
            var vy = new Vector3d(-0.58778525229246859, 0.80901699437495056, -1.1102230246251565E-16);
            var vz = new Vector3d(0.5720031066241591, 0.41558458311363178, 0.70717883924457681);

            return new Basis3d(vx, vy, vz);
        }

        private static Basis3d CreateNormRoundBasis()
        {
            var vx = new Vector3d(-0.572, -0.416, 0.707);
            var vy = new Vector3d(-0.588, 0.809, 0);
            var vz = new Vector3d(0.572, 0.416, 0.707);

            return new Basis3d(vx, vy, vz);
        }

        private static Basis3d CreateOriginBasis()
        {
            var vx = new Vector3d(1, 0, 0);
            var vy = new Vector3d(0, 1, 0);
            var vz = new Vector3d(0, 0, 1);

            return new Basis3d(vx, vy, vz);
        }

        private static Basis3d CreateBasis1()
        {
            var vx = new Vector3d(-0.572061402817688, -0.41562693777745136, 0.70710678118654569);
            var vy = new Vector3d(-0.5877852522924687, 0.80901699437495067, -2.7755575615628914E-17);
            var vz = new Vector3d(-0.57206140281768514, -0.41562693777744919, -0.70710678118654935);

            return new Basis3d(vx, vy, vz);
        }

        private static Basis3d CreateBasis2()
        {
            var vx = new Vector3d(-0.5720614028176878, -0.41562693777745152, 0.707106781186546);
            var vy = new Vector3d(-0.5877852522924687, 0.80901699437495078, -1.4551251001691181E-16);
            var vz = new Vector3d(-0.57206140281768547, -0.41562693777744958, -0.70710678118654913);

            return new Basis3d(vx, vy, vz);
        }
    }
}
