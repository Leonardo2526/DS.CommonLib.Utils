using BenchmarkDotNet.Attributes;
using DS.PathSearch.GridMap;
using System;

namespace ConsoleApp1.Tests
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FrancoGustavoTest
    {
        public static int Count;

        [Benchmark]
        public void FGTest()
        {
            IMap Map = new DS.PathSearch.GridMap.d2.MapXY20();
            //IMap Map = new DS.PathSearch.GridMap.d2.MapXY1000();
            //IMap Map = new DS.PathSearch.GridMap.d3.Map3d100();
            Count = FrancoGustavo.FGAlgorythm.GetPathByMap(Map, new PathRequiment0()).Count;
            Console.WriteLine(Count);
        }


    }
}
