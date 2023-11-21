﻿using Serilog;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// Represents a builder to create <see cref="IResolveFactory{TItem, TResult}"/>.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TTask"></typeparam>
    /// <typeparam name="TResult"></typeparam>    
    public abstract partial class FactoryBuilderBase<TItem, TTask, TResult> :
        IResolveFactoryBuilder<TItem, TResult>
    {

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// A factory get <typeparamref name="TResult"/> from <typeparamref name="TTask"/>.
        /// </summary>
        public IResolveFactory<TItem, TResult> ResolveFactory { get; private set; }

        /// <summary>
        /// Visualizator to show <typeparamref name="TTask"/>.
        /// </summary>
        public IItemVisualisator<TTask> TaskVisualizator { get; set; }

        /// <summary>
        /// Visualizator to show <typeparamref name="TResult"/>.
        /// </summary>
        public IItemVisualisator<TResult> ResultVisualizator { get; set; }

        /// <summary>
        /// Specifies if resolve task in separate thread.
        /// </summary>
        public bool ResolveParallel { get; set; }

        /// <inheritdoc/>
        public IResolveFactory<TItem, TResult> Create()
        {
            var taskCreator = BuildTaskCreator();
            if (taskCreator == null)
            { Logger?.Error("Failed to build task creator."); return null; }

            var taskResolver = BuildTaskResover();
            if (taskResolver == null)
            { Logger?.Error("Failed to build task resolver."); return null; }

            return ResolveFactory = new ResolveFactory<TItem, TTask, TResult>(taskCreator, taskResolver)
            {
                Logger = Logger,
                TaskVisualizator = TaskVisualizator,
                ResultVisualizator = ResultVisualizator,
                ResolveParallel = ResolveParallel
            };
        }

        /// <summary>
        /// Get an object to create <typeparamref name="TTask"/>s.
        /// </summary>
        /// <returns></returns>
        protected abstract ITaskCreator<TItem, TTask> BuildTaskCreator();

        /// <summary>
        /// Get an object to resolve <typeparamref name="TTask"/>s.
        /// </summary>
        /// <returns></returns>
        protected abstract ITaskResolver<TTask, TResult> BuildTaskResover();
    }


}