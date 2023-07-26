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

using System.Collections.Generic;

namespace FrancoGustavo
{
        [Author("Franco, Gustavo")]
        internal class ComparePFNode : IComparer<PointPathFinderNode>
        {
            #region IComparer Members
            public int Compare(PointPathFinderNode x, PointPathFinderNode y)
            {
                if (x.F > y.F)
                    return 1;
                else if (x.F < y.F)
                    return -1;
                return 0;
            }
            #endregion
        }
}