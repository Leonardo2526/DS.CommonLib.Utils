using BenchmarkDotNet.Attributes;
using DSUtils.GridMap;
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
            IMap Map = new DSUtils.GridMap.d2.Map2d100();
            HPAAlgorythm.GetPath(Map);
        }


    }
}
