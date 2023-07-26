using System;

namespace FrancoGustavo
{
    static class NodesCheker
    {
        public static int newNodeANX { get; set; }
        public static int newNodeANY { get; set; }
        public static int newNodeANZ { get; set; }

        public static bool IfMinLength(byte length, PathFinderNode parentNode, int newNodeX, int newNodeY, int newNodeZ)
        {
            if ((newNodeY - parentNode.ANY) != 0 && (newNodeX - parentNode.ANX) != 0)
            {
                if (Math.Abs(parentNode.Y - parentNode.ANY) == 0 && Math.Abs(parentNode.X - parentNode.ANX) < length)
                    return false;
                if (Math.Abs(parentNode.X - parentNode.ANX) == 0 && Math.Abs(parentNode.Y - parentNode.ANY) < length)
                    return false;

                WriteNodeAN(parentNode);
            }
            else if ((newNodeY - parentNode.ANY) != 0 && (newNodeZ - parentNode.ANZ) != 0)
            {

                if (Math.Abs(parentNode.Y - parentNode.ANY) == 0 && Math.Abs(parentNode.Z - parentNode.ANZ) < length)
                    return false;
                if (Math.Abs(parentNode.Z - parentNode.ANZ) == 0 && Math.Abs(parentNode.Y - parentNode.ANY) < length)
                    return false;

                WriteNodeAN(parentNode);
            }
            else if ((newNodeX - parentNode.ANX) != 0 && (newNodeZ - parentNode.ANZ) != 0)
            {

                if (Math.Abs(parentNode.Z - parentNode.ANZ) == 0 && Math.Abs(parentNode.X - parentNode.ANX) < length)
                    return false;
                if (Math.Abs(parentNode.X - parentNode.ANX) == 0 && Math.Abs(parentNode.Z - parentNode.ANZ) < length)
                    return false;

                WriteNodeAN(parentNode);
            }
            else
            {
                newNodeANX = parentNode.ANX;
                newNodeANY = parentNode.ANY;
                newNodeANZ = parentNode.ANZ;
            }

            return true;
        }

        private static void WriteNodeAN(PathFinderNode parentNode)
        {
            newNodeANX = parentNode.X;
            newNodeANY = parentNode.Y;
            newNodeANZ = parentNode.Z;
        }
    }
}
