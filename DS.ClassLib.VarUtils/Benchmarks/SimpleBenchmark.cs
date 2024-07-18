using DS.ClassLib.VarUtils.Benchmarks;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnitsNet;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The object to run actions in order to assess their relative performance.
    /// </summary>
    public static class SimpleBenchmark
    {
        private static ILogger _logger;


        /// <summary>
        /// Logger used for writing log events.
        /// </summary>
        public static ILogger Logger
        { get => _logger; set => _logger = value; }

        /// <summary>
        /// Run <paramref name="action"/> to test it's performance with <paramref name="testsCount"/> iterations.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testsCount"></param>
        /// <param name="maxRMSDeviation">Max value deviation from average.</param>
        /// <param name="cutOdds">Remove odd values from result that have difference 
        /// from average more than <paramref name="maxRMSDeviation"/>.</param>
        /// <returns>
        /// The result of performance test.
        /// </returns>
        public static BenchmarkReport Run(
            Action action,
            int testsCount = 100,
            double maxRMSDeviation = 0.15,
            bool cutOdds = false)
        {
            if (maxRMSDeviation <= 0 || maxRMSDeviation >= 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(maxRMSDeviation)} " +
                $"must have value from 0 to 1.");
            }


            var warmUpTime = WarmUp(action);
            var actualTimes = RunActual(action, testsCount);
            var sumTicks = actualTimes.Sum(t => t.Ticks);
            var actionTime = GetAverage(actualTimes, maxRMSDeviation, cutOdds);

            return new BenchmarkReport(actionTime, warmUpTime, new TimeSpan(sumTicks));
        }

        private static TimeSpan WarmUp(Action action)
        {
            var sw = Stopwatch.StartNew();
            action.Invoke();
            sw.Stop();
            _logger?.Information($"Warmed at: {sw.Elapsed.TotalMilliseconds} ms.");
            return sw.Elapsed;
        }


        private static IList<TimeSpan> RunActual(Action action, int testsCount)
        {

            IList<TimeSpan> resutTimes = [];
            for (int i = 0; i < testsCount; i++)
            {
                var sw = Stopwatch.StartNew();
                action.Invoke();
                sw.Stop();
                resutTimes.Add(sw.Elapsed);
            }

            return resutTimes;
        }

        private static TimeSpan GetAverage(
            IList<TimeSpan> timeSpans,
            double maxRMSDeviation = 0.15,
            bool cutOdds = false)
        {
            var resultTicks = timeSpans
              .Select(t => (double)t.Ticks);
            var averageTicks = resultTicks.Average();
            _logger?.Information($"Average ticks are: {(int)averageTicks} ticks.");
            _logger?.Information($"Average time is: {new TimeSpan((int)averageTicks).TotalMilliseconds} ms.");

            var rms = MathUtils.StandardDeviation(resultTicks, averageTicks);
            _logger?.Information($"RMS is: {Math.Round(rms)} ticks.");

            var rRms = rms / averageTicks;
            _logger?.Information($"Relation RMS is: {Math.Round(rRms, 3) * 100} %.");

            if (cutOdds && rRms > maxRMSDeviation)
            {
                _logger?.Warning($"Relation RMS is more than allowed {maxRMSDeviation * 100} %.");
                if (cutOdds)
                {
                    var maxRms = averageTicks * maxRMSDeviation;
                    int origin = resultTicks.Count();
                    resultTicks = resultTicks
                        .Where(v => Math.Abs(v - averageTicks) <= maxRms);
                    averageTicks = (int)resultTicks.Average();
                    _logger?.Information($" {origin - resultTicks.Count()} results were cutted as odds.");

                    rms = MathUtils.StandardDeviation(resultTicks, averageTicks);
                    rRms = rms / averageTicks;
                    _logger?.Information($"Cutted RMS is: {Math.Round(rms)} ticks ({Math.Round(rRms, 3) * 100} %.).");
                }
            }

            return new TimeSpan((int)averageTicks);
        }

    }
}
