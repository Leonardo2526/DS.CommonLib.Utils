using BenchmarkDotNet.Attributes;
using DS.PathSearch.GridMap;
using System;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FrancoGustavoTest
    {
        //public static int Count;

        [Benchmark]
        public void FGTest()
        {
            //IMap Map = new DS.PathSearch.GridMap.d2.MapXY20();
            //IMap Map = new DSUtils.GridMap.d3.Map3d20();
            //var res = FrancoGustavo.FGAlgorythm.GetPathByMap(Map, new PathRequiment1());
            //Console.WriteLine(Count);
            for (int i = 0; i < 10000000; i++)
            {
                var s = i.ToString();
            }
        }


    }
}
