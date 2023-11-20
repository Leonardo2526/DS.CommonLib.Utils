using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object to resolve <typeparamref name="TItem"/> with set of factories.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class ResolveProcessor<TItem, TResult>
    {
        private readonly IEnumerable<IResolveFactory<TItem, TResult>> _resolveFactories;
        private readonly Queue<IResolveFactory<TItem, TResult>> _factoriesQueue = new();
        private readonly List<TResult> _results = new();


        /// <summary>
        /// Instansiate a resolver with set of <paramref name="resolveFactories"/>.
        /// </summary>
        /// <param name="resolveFactories"></param>
        public ResolveProcessor(IEnumerable<IResolveFactory<TItem, TResult>> resolveFactories)
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
        /// Try to resove <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// The result of resolving an <paramref name="item"/>.
        /// </returns>
        public TResult TryResolve(TItem item)
            => ResolveAsync(item, false).Result;

        /// <summary>
        /// Try to resove <paramref name="item"/> asynchronously.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// The result of resolving an <paramref name="item"/>.
        /// </returns>
        public async Task<TResult> TryResolveAsync(TItem item)
            => await ResolveAsync(item, true);

        private async Task<TResult> ResolveAsync(TItem item, bool runParallel)
        {
            TResult result = default;

            while (_factoriesQueue.Count > 0)
            {
                var factory = _factoriesQueue.Peek();

                Logger?.Information($"Start resolving with #{_factoriesQueue.Count} resolve factory.");

                result = runParallel ?
                    await Task.Run(() => factory.TryResolveAsync(item)) :
                    factory.TryResolve(item);

                if (result.Equals(default))
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
