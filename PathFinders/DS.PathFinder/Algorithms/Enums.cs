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


namespace FrancoGustavo
{
    [Author("Franco, Gustavo")]
    enum PathFinderNodeType
    {
        Start = 1,
        End = 2,
        Open = 4,
        Close = 8,
        Current = 16,
        Path = 32
    }

    public enum HeuristicFormula
    {
        Manhattan = 1,
        MaxDXDY = 2,
        DiagonalShortCut = 3,
        Euclidean = 4,
        EuclideanNoSQR = 5,
        Custom1 = 6
    }
}