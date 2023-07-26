using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.RVT.ModelSpaceFragmentation.Path.CLZ
{
    class CLZInfo
    {
        private static readonly double Distance = 100;
        private static double DistanceF { get; set; }
        private static int DistanceInSteps { get; set; }

        public static int FullDistanceInSteps { get; set; }
        public static double FullDistanceF { get; set; }
        public static double FullDistanceWithMarkPointF { get; set; }

        public static List<StepPoint> Points { get; set; }

        public static void GetInfo()
        {
            DistanceF = UnitUtils.Convert(Distance / 1000,
                                DisplayUnitType.DUT_METERS,
                                DisplayUnitType.DUT_DECIMAL_FEET);
            DistanceInSteps = (int)Math.Ceiling(DistanceF / InputData.PointsStepF);

            FullDistanceF = DistanceF + (ElementSize.ElemDiameterF / 2);
            FullDistanceInSteps = (int)Math.Round((FullDistanceF) / InputData.PointsStepF);
            FullDistanceWithMarkPointF = FullDistanceF + ModelSpacePointsGenerator.PointsStepF;

            Points = new List<StepPoint>();
        }

    }
}
