using DS.ClassLib.VarUtils.Extensions.Tuples;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object to resolve a task with set of factories.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ResolveProcessor<TResult> : IResolveProcessor<TResult>
    {
        private readonly Queue<IResolveFactory<TResult>> _factoriesQueue = new();
        private Queue<IResolveFactory<TResult>> _specificFactoriesQueue = new ();
        private readonly IEnumerable<IResolveFactory<TResult>> _resolveFactories;


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
        public IEnumerable<TResult> Results
        {
            get
            {
                var results = new List<TResult>();
                foreach (var factory in _resolveFactories)
                {
                    var fr = factory.Results;
                    if (fr != null)
                    { results.AddRange(fr); }
                }
                return results;
            }
        }


        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }


        /// <inheritdoc/>
        public IEnumerable<IResolveFactory<TResult>> ResolveFactories => _resolveFactories;

        /// <inheritdoc/>
        public CancellationTokenSource CancellationTokenSource { get; set; }

        /// <inheritdoc/>
        public TResult TryResolve(IEnumerable<IResolveFactory<TResult>> resolveFactories)
        {
            _specificFactoriesQueue.Clear();
            resolveFactories.ToList().ForEach(_specificFactoriesQueue.Enqueue);
            return ResolveAsync(_specificFactoriesQueue, false).Result;
        }

        /// <inheritdoc/>
        public async Task<TResult> TryResolveAsync(IEnumerable<IResolveFactory<TResult>> resolveFactories)
        {

            _specificFactoriesQueue.Clear();
            resolveFactories.ToList().ForEach(_specificFactoriesQueue.Enqueue);
            return await ResolveAsync(_specificFactoriesQueue, true);
        }



        private async Task<TResult> ResolveAsync(Queue<IResolveFactory<TResult>> resolveFactoriesQueue, bool runAsync)
        {
            TResult result = default;

            while (resolveFactoriesQueue.Count > 0)
            {
                var factory = resolveFactoriesQueue.Peek();

                Logger?.Information($"Start resolving with #{resolveFactoriesQueue.Count} resolve factory.");

                result = runAsync ?
                    await factory.TryResolveAsync() :
                    factory.TryResolve();

                if (result is null || result.Equals(default) || result.IsTupleNull())
                {
                    resolveFactoriesQueue.Dequeue();
                    Logger?.Information($"Factory #{resolveFactoriesQueue.Count + 1} returns null result, so it was removed from resolving queue.");
                }
                else
                {
                    Logger?.Information($"Processor results: {Results?.Count()}.");
                    break;
                }

                if (CancellationTokenSource.Token.IsCancellationRequested)
                {
                    Logger?.Information($"Resolving cancellation requested).");
                    break;
                }
            }
            return result;
        }

    }
}
