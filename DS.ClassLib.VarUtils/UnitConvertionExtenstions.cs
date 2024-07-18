using Rhino;
using System;

namespace DS.ClassLib.VarUtils
{
    public static class UnitConvertionExtenstions
    {
        private readonly static double _mmToFeet =
            RhinoMath.UnitScale(UnitSystem.Millimeters, UnitSystem.Feet);
        private readonly static double _cmToFeet =
            RhinoMath.UnitScale(UnitSystem.Centimeters, UnitSystem.Feet);

        private readonly static double _feetToMM =
           RhinoMath.UnitScale(UnitSystem.Feet, UnitSystem.Millimeters);
        private readonly static double _feetToCM =
            RhinoMath.UnitScale(UnitSystem.Feet, UnitSystem.Centimeters);

        public static double MMToFeet(this double mm, int power = 1)
            => mm * Math.Pow(mm * _mmToFeet, power);
        public static double MMToFeet(this int mm, int power = 1)
           => MMToFeet((double)mm, power);

        public static double CMToFeet(this double cm, int power = 1)
            => cm * Math.Pow(cm * _cmToFeet, power);
        public static double CMToFeet(this int cm, int power = 1)
            => CMToFeet((double)cm, power);

        public static double FeetToMM(this double feet, int power = 1)
            => feet * Math.Pow(feet * _feetToMM, power);
        public static double FeetToMM(this int feet, int power = 1)
            => FeetToMM((double)feet, power);

        public static double FeetToCM(this double feet, int power = 1)
            => feet * Math.Pow(feet * _feetToCM, power);
        public static double FeetToCM(this int feet, int power = 1)
            => FeetToCM((double)feet, power);
    }
}

