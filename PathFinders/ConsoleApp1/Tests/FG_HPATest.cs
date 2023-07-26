using BenchmarkDotNet.Attributes;
using DS.PathSearch.GridMap;
using HPA;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FGHPATest
    {
        public static int Count;
        IMap Map = new DS.PathSearch.GridMap.d2.MapXY100();

        [Benchmark]
        public void HPATest1()
        {
            HPAAlgorythm.GetPath(Map);
        }

        [Benchmark]
        public void FGTest2()
        {
            //FrancoGustavo.FGAlgorythm.GetPathByMap(Map, new PathRequiment1());
        }


    }
}
