using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Tests
{
    [MemoryDiagnoser]
    [RankColumn]
    public class Loop
    {
        [Benchmark]
        public void Test()
        {
            for (int i = 0; i < 10000000; i++)
            {
                var s = i.ToString();
            }
        }
    }
}
