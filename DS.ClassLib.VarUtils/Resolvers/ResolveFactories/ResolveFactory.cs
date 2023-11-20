using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// A factory to resolve <typeparamref name="TItem"/>.
    /// </summary>
    /// <typeparam name="TTask"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class ResolveFactory<TItem, TTask, TResult> : IResolveFactory<TItem, TResult>
    {
        private readonly ITaskCreator<TItem, TTask> _taskCreator;
        private readonly ITaskResolver<TTask, TResult> _taskResolver;
        private List<TTask> _tasks;
        private List<TResult> _results;

        /// <summary>
        /// Instansiate a factory to resolve 
        /// <typeparamref name="TItem"/> with <paramref name="taskCreator"/> and <paramref name="taskResolver"/>  
        /// where task is <typeparamref name="TTask"/> and result of resolving is <typeparamref name="TResult"/>.     
        /// </summary>
        /// <param name="taskCreator"></param>
        /// <param name="taskResolver"></param>
        public ResolveFactory(ITaskCreator<TItem, TTask> taskCreator,
            ITaskResolver<TTask, TResult> taskResolver)
        {
            _taskCreator = taskCreator;
            _taskResolver = taskResolver;
        }

        /// <summary>
        /// The results of resolving.
        /// </summary>
        public IEnumerable<TResult> Results => _results;

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public TResult TryResolve(TItem item)
        => ResolveAsync(item, false).Result;

        /// <inheritdoc/>
        public async Task<TResult> TryResolveAsync(TItem item)
       => await ResolveAsync(item, true);

        public async Task<TResult> ResolveAsync(TItem item, bool runParallel)
        {
            TResult result = default;

            var task = _taskCreator.CreateTask(item);
            if (task != null)
            {
                _tasks ??= new List<TTask>();
                _tasks.Add(task);
                Logger?.Information($"Task #{_tasks.Count} {task.GetType().Name} created.");

                result = await ResolveAsync(task, runParallel);
            }
            else
            {
                Logger?.Information($"Unable to get task to resolve.");
            }

            Logger?.Information($"Resolve factory tasks: {_tasks?.Count}.");
            Logger?.Information($"Resolve factory results: {_results?.Count}.");

            return result;
        }

        private async Task<TResult> ResolveAsync(TTask task, bool runParallel)
        {
            Logger?.Information($"Trying to resolve task with {_taskResolver.GetType().Name}.");

            var time1 = DateTime.Now;

            var result = runParallel ?
                 await Task.Run(() => _taskResolver.TryResolveAsync(task)) :
                _taskResolver.TryResolve(task);

            var time2 = DateTime.Now;
            TimeSpan totalInterval = time2 - time1;

            if (result != null)
            {
                _results ??= new List<TResult>();
                _results.Add(result);
                Logger?.Information($"Task resolved in {(int)totalInterval.TotalMilliseconds} ms successfully!");
            }
            else
            { Logger?.Information($"Unable to resolve task."); }

            return result;
        }

    }
}
