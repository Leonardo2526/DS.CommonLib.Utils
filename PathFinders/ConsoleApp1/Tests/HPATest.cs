using BenchmarkDotNet.Attributes;
using DS.PathSearch.GridMap;
using HPA;
using System;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class HPATest
    {
        [Benchmark]
        public void Test1()
        {
            IMap Map = new DS.PathSearch.GridMap.d2.MapXY100();
            HPAAlgorythm.GetPath(Map);
        }


    }
}
