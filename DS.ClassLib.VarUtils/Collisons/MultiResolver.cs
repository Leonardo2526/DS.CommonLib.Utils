using DS.ClassLib.VarUtils.Collisions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Collisons
{
    /// <summary>
    /// An object to resove tasks with set of resolvers.
    /// </summary>
    public class MultiResolver : IMultiResolver
    {
        private int _tasksIteratedCount;
        private readonly List<ISolution> _solutions = new();

        /// <summary>
        /// Instansiate an object to create tasks with <paramref name="taskCreator"/> 
        /// and resolve them with <paramref name="taskResolvers"/>.
        /// </summary>
        /// <param name="taskCreator"></param>
        /// <param name="taskResolvers"></param>
        public MultiResolver(IEnumerator<IResolveTask> taskCreator, IEnumerable<ITaskResolver> taskResolvers)
        {
            TaskCreator = taskCreator;
            TaskResolvers = taskResolvers;
        }


        delegate Task<ISolution> ResolveRunner(ITaskResolver resolver, IResolveTask task);


        /// <inheritdoc/>
        public IEnumerator<IResolveTask> TaskCreator { get; }

        /// <inheritdoc/>
        public IEnumerable<ITaskResolver> TaskResolvers { get; }

        /// <inheritdoc/>
        public IEnumerable<ISolution> Solutions => _solutions;

        /// <summary>
        /// Operations logger.
        /// </summary>
        public ILogger Logger { get; set; }


        /// <inheritdoc/>
        public ISolution TryResolve() => 
            Task.Run(() => GetSolution(RunResolver)).Result;

        /// <inheritdoc/>
        public Task<ISolution> TryResolveAsync() =>
            GetSolution(RunResolverAsync);
        

        private async Task<ISolution> GetSolution(ResolveRunner resolveRunner)
        {
            ISolution solution = null;

            if (TaskCreator.MoveNext())
            {
                _tasksIteratedCount++;
                foreach (var resolver in TaskResolvers)
                {
                    Logger.Information($"Trying to resolve task {_tasksIteratedCount} with {resolver.GetType().Name}.");
                    solution = await resolveRunner.Invoke(resolver, TaskCreator.Current);
                    if (solution != null) 
                    {
                        Logger.Information($"Task {_tasksIteratedCount} resolved.");
                        _solutions.Add(solution);
                        break; 
                    }
                }
            }
            else
            {
                Logger.Information("Unable to get next task to resolve.");
                Logger.Information($"Total tasks iterated {_tasksIteratedCount}.");
            }

            return solution;
        }

        private Task<ISolution> RunResolverAsync(ITaskResolver resolver, IResolveTask task) =>
            resolver.TryResolveAsync(task);

        private Task<ISolution> RunResolver(ITaskResolver resolver, IResolveTask task) =>
            Task.Run(() => resolver.TryResolve(task));

    }
}
