using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    public class DummyResolver : ITaskResolver<IResolveTask>
    {
        private readonly List<ISolution> _solutions = new();

        public IEnumerable<ISolution> Solutions => _solutions;

        /// <summary>
        /// Operations logger.
        /// </summary>
        public ILogger Logger { get; set; }

        public ISolution TryResolve(IResolveTask task)
        {
            var time1 = DateTime.Now;

            var solution = new DummySolution();
            Task.Delay(1000).Wait();

            var time2 = DateTime.Now;
            TimeSpan totalInterval = time2 - time1;
            Logger?.Information($"Task resoved in {(int)totalInterval.TotalMilliseconds} ms");

            _solutions.Add(solution);
           return solution;
        }

        public async Task<ISolution> TryResolveAsync(IResolveTask task)
        {
            return await Task.Run(() =>TryResolve(task));
        }
    }
}
