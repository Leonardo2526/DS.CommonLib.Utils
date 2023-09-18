using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal class CCSModelTest
    {
        public CCSModelTest()
        {
            var model =  Run();
            //SetAllQuadrantsActivity(model);
            //SetQuadrantsActivityByVector(model);
            SetQuadrantsActivityByVectorAndQuadrant(model);
            //SetBoxActivity(model);
            PrintQuadrants(model);
        }

        private CCSModel Run()
        {
            return new CCSModel(); 
        }

        private void SetAllQuadrantsActivity(CCSModel cCSModel)
        {
            OrthoPlane orthoPlane = 0;
            orthoPlane = OrthoPlane.XY;
            cCSModel.SetAllQuadrantsActivity(false, orthoPlane);
        }

        private void SetQuadrantsActivityByVector(CCSModel cCSModel)
        {
            Vector3d vector = new Vector3d(1,0,0);
            cCSModel.SetQuadrantsActivity(false, vector);
        }

        private void SetQuadrantsActivityByVectorAndQuadrant(CCSModel cCSModel)
        {
            Vector3d vector = new Vector3d(0, 0, 1);
            OrthoPlane orthoPlane = 0;
            //orthoPlane = OrthoPlane.XZ;
            cCSModel.SetQuadrantsActivity(false, vector, orthoPlane);
        }

        private void SetBoxActivity(CCSModel cCSModel)
        {
            cCSModel.SetBoxActivity(0, false);
           var b= cCSModel.OctantsModel[0].BoxModel.IsEnable;
            Console.WriteLine(b);
        }

        private void GetQuadrantsModel(CCSModel cCSModel)
        {
            OrthoPlane orthoPlane = 0;
            //orthoPlane = OrthoPlane.XY;
            cCSModel.SetAllQuadrantsActivity(false, orthoPlane);

            cCSModel.QuadrantsModel.ToList().ForEach(c => { Console.WriteLine(c.Rectangle.Plane.Normal); });
        }


        private void PrintQuadrants(CCSModel cCSModel)
        {
            //cCSModel.XYQuadrantsModel.ToList().ForEach(c => { Console.WriteLine(c.IsEnable); });
            cCSModel.QuadrantsModel.ToList().ForEach(c => { Console.WriteLine(c.IsEnable); });

        }
    }
}
