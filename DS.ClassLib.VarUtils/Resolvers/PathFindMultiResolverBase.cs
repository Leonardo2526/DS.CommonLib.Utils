using DS.ClassLib.VarUtils.Resolvers.ResolveTasks;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    public class PathFindMultiResolverBase
    {
        private readonly List<ISolution> _solutions = new();
        private int _tasksIteratedCount;

        /// <summary>
        /// Operations logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public IEnumerable<ISolution> Solutions => _solutions;


        /// <inheritdoc/>
        public IEnumerator<PathFindVertexTask> TaskCreator { get; protected set; }

        /// <inheritdoc/>
        public IEnumerable<ITaskResolver<PathFindVertexTask>> TaskResolvers { get; protected set; }

        protected async Task<ISolution> Resolve(bool runAsync)
        {
            ISolution solution = null;

            if (TaskCreator.MoveNext())
            {
                _tasksIteratedCount++;
                foreach (var resolver in TaskResolvers)
                {
                    Logger?.Information($"Trying to resolve task {_tasksIteratedCount} with {resolver.GetType().Name}.");
                    solution = runAsync ?
                        await resolver.TryResolveAsync(TaskCreator.Current) :
                        resolver.TryResolve(TaskCreator.Current);
                    if (solution != null)
                    {
                        Logger?.Information($"Task {_tasksIteratedCount} resolved.");
                        _solutions.Add(solution);
                        break;
                    }
                    else
                    {
                        Logger?.Information($"Unable to resolve task {_tasksIteratedCount} with {resolver.GetType().Name}.");
                    }
                }
            }
            else
            {
                Logger?.Information("Unable to get next task to resolve.");
            }

            Logger?.Information($"Total tasks iterated {_tasksIteratedCount}.");
            Logger?.Information($"Total solutions {_solutions.Count}.");

            return solution;
        }
    }
}