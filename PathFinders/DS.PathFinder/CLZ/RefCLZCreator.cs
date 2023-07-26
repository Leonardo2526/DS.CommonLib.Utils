using System;

namespace FrancoGustavo.CLZ
{
    struct RefCLZCreator
    {
        private readonly byte Dist;
        public RefCLZCreator(byte dist)
        {
            Dist = dist;
        }

        public sbyte[,,] CreateOld()
        {
            int matrixSize = Dist * 2;
            sbyte[,,] ClearanceZone = new sbyte[matrixSize, matrixSize, matrixSize];

            for (int z = -Dist; z <= Dist; z ++)
            {
                for (int y = -Dist; y <= Dist; y ++)
                {
                    for (int x = -Dist; x <= Dist; x++)
                        ClearanceZone[x, y, z] = 1;
                }
            }

            return ClearanceZone;
        }

        public sbyte[,] Create()
        {
            int matrixSize = (int)Math.Pow((Dist * 2 + 1) , 3);
            sbyte[,] ClearanceZone = new sbyte[matrixSize, 3]; 
            sbyte lowBound = (sbyte)(-Dist);

            int i = 0;
            for (sbyte z = lowBound; z <= Dist; z++)
            {
                for (sbyte y = lowBound; y <= Dist; y++)
                {
                    for (sbyte x = lowBound; x <= Dist; x++)
                    {
                        ClearanceZone[i, 0] = x;
                        ClearanceZone[i, 1] = y;
                        ClearanceZone[i, 2] = z;
                        i++;
                    }
                }
            }

            return ClearanceZone;
        }
    }
}