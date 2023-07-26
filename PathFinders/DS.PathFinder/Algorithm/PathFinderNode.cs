using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
