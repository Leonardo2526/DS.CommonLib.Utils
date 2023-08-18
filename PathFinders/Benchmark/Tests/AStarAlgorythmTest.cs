using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class AStarAlgorythmTest
    {
        Implementation.Test test = new Implementation.Test();

        [Benchmark]
        public void TestBenchmark1()
        {
            test.RunTest();
        }


    }
}
