//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  gustavo_franco@hotmail.com
//
//  Copyright (C) 2006 Franco, Gustavo 
//
#define DEBUGON


using System;
using System.Windows.Media.Media3D;

namespace FrancoGustavo
{
    [Author("Franco, Gustavo")]
    public struct PathFinderNode
    {
        public int F;
        public int G;
        public int H;  // f = gone + heuristic
        public int X;
        public int Y;
        public int Z;
        public int PX; // Parent
        public int PY;
        public int PZ;
        public int Id;
        public int ANX;
        public int ANY;
        public int ANZ;
    }

    public struct FloatPathFinderNode
    {
        public double F;
        public double G;
        public double H;  // f = gone + heuristic
        public double X;
        public double Y;
        public double Z;
        public double PX; // Parent
        public double PY;
        public double PZ;
        //public int Id;
        public double ANX;
        public double ANY;
        public double ANZ;
        public Vector3D Dir;

        public override bool Equals(object obj)
        {
            if (obj is not FloatPathFinderNode) throw new InvalidCastException();

            double comp = 0.001;
            return obj is FloatPathFinderNode node &&
                   Math.Abs(X - node.X) < comp &&
                   Math.Abs(Y - node.Y) < comp &&
                   Math.Abs(Z - node.Z) < comp;
        }
    }
}