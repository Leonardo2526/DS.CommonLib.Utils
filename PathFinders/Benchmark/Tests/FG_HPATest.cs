using BenchmarkDotNet.Attributes;
using DSUtils.GridMap;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FGHPATest
    {
        public static int Count;
        IMap Map = new DSUtils.GridMap.d2.Map2d100();

        [Benchmark]
        public void HPATest1()
        {
            HPA.HPAAlgorythm.GetPath(Map);
        }

        [Benchmark]
        public void FGTest2()
        {
            //FrancoGustavo.FGAlgorythm.GetPathByMap(Map, new PathRequiment1());
        }


    }
}
