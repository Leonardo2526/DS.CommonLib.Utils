using BenchmarkDotNet.Running;
using System;

namespace Benchmark
{
    class BenchmarkProgram
    {
        static void Main(string[] args)
        {
            {
                //BenchmarkRunner.Run<FGHPATest>();
                BenchmarkRunner.Run<FrancoGustavoTest>();
                //BenchmarkRunner.Run<HPATest>();
                Console.ReadLine();
            }
        }
    }
}
