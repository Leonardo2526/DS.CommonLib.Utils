using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveAlgorythm
{
    class IteratorByXYPlane : ISpacePointsIterator
    {
        public void Iterate()
        {
            int x, y, z;
            z = 0;
            do
            {
                NeighboursPasser.A = 0;
                for (y = 0; y < PathSearch.MaxGridPoint.Y; y++)
                {
                    for (x = 0; x < PathSearch.MaxGridPoint.X; x++)
                    {
                        if (!WaveExpander.Operation(x, y, z))
                            continue;
                    }
                }
            } while (!WaveExpander.Grid.ContainsKey(WaveExpander.EndStepPoint) && NeighboursPasser.A != 0);
        }
    }
}
