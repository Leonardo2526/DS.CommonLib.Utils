using DS.System;
using System.Collections.Generic;

namespace FrancoGustavo
{
    public interface IGraph
    {
        Location Start { get; set; }
        Location Goal { get; set; }
        int[,] WeightMatrix { get; set; }
        int[,,] Matrix { get; set; }
    }
}
