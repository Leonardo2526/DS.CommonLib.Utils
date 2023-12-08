using DS.ClassLib.VarUtils.Extensions.Tuples;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object to resolve a task with set of factories.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ResolveProcessor<TResult>
    {
        private readonly IEnumerable<IResolveFactory<TResult>> _resolveFactories;
        private readonly Queue<IResolveFactory<TResult>> _factoriesQueue = new();
        private readonly List<TResult> _results = new();


        /// <summary>
        /// Instansiate a resolver with set of <paramref name="resolveFactories"/>.
        /// </summary>
        /// <param name="resolveFactories"></param>
        public ResolveProcessor(IEnumerable<IResolveFactory<TResult>> resolveFactories)
        {
            _resolveFactories = resolveFactories;
            resolveFactories.ToList().ForEach(_factoriesQueue.Enqueue);
        }


        /// <summary>
        /// The results of resolving.
        /// </summary>
        public IEnumerable<TResult> Results => _results;


        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }


        /// <summary>
        /// Propagates notification that operations should be canceled.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }
       

        /// <summary>
        /// Try to resove a task.
        /// </summary>
        /// <returns>
        /// The result of resolving a task.
        /// </returns>
        public TResult TryResolve()
            => ResolveAsync(false).Result;

        /// <summary>
        /// Try to resove a task asynchronously.
        /// </summary>
        /// <returns>
        /// The result of resolving a task.
        /// </returns>
        public async Task<TResult> TryResolveAsync()
            => await ResolveAsync(true);

        private async Task<TResult> ResolveAsync( bool runAsync)
        {
            TResult result = default;

            while (_factoriesQueue.Count > 0)
            {
                var factory = _factoriesQueue.Peek();

                Logger?.Information($"Start resolving with #{_factoriesQueue.Count} resolve factory.");

                result = runAsync ?
                    await factory.TryResolveAsync() :
                    factory.TryResolve();

                if (result is null || result.Equals(default) || result.IsTupleNull())
                {
                    _factoriesQueue.Dequeue();
                    Logger?.Information($"Factory #{_factoriesQueue.Count + 1} returns null result, so it was removed from resolving queue.");
                }
                else
                {
                    _results.Add(result);
                    Logger?.Information($"Processor results: {_results?.Count}.");
                    break;
                }

                if (CancellationToken.IsCancellationRequested)
                {
                    Logger?.Information($"Resolving cancellation requested).");
                    break;
                }
            }
            return result;
        }

    }
}
