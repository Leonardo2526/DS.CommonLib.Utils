using AStarAlgorythm;
using DS.PathSearch;
using System;
using System.Collections.Generic;
using WaveAlgorythm;

namespace Implementation
{
    public class Test
    {
        public void RunTest()
        {
            IDataInput dataInput;
            //dataInput = new ModelPlaneInput1();
            //dataInput = new Input1CLZ4();
            dataInput = new Input2D();
            //dataInput = new Input1NoWalls();

            AStar aStar = new AStar(dataInput.Start, dataInput.Goal, dataInput.MaxGridPoint, dataInput.Unpassablelocations);
            List<Location> AStarPath = aStar.GetPath();
            AStarPath = aStar.GetPathWithVisualizition();

            Console.WriteLine(AStarPath.Count.ToString());
            Console.ReadLine();
        }
    }
}
