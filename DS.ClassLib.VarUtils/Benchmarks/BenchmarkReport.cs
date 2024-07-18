using System;

namespace DS.ClassLib.VarUtils.Benchmarks
{
    public record BenchmarkReport(TimeSpan ActionTime, TimeSpan WarmUpTime, TimeSpan TotalTime)
    {
        public TimeSpan ActionTime { get; } = ActionTime;
        public TimeSpan WarmUpTime {  get; } = WarmUpTime;
        public TimeSpan TotalTime { get; } = TotalTime;
    }
}
