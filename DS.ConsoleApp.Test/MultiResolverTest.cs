using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Resolvers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class MultiResolverTest
    {
        private MultiResolver _resolver;

        public MultiResolverTest()
        {
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .WriteTo.Debug()
                 .CreateLogger();
        }

        public MultiResolverTest CreateResolver()
        {
            var taskCreator = new DummyCreator();
            var taskResolver = new DummyResolver()
            {
                Logger = Log.Logger,
            };

            var resolvers = new List<ITaskResolver>()
            { taskResolver};
            _resolver = new MultiResolver(taskCreator, resolvers)
            {
                Logger = Log.Logger
            };

            return this;
        }

        public ISolution Resolve()
        {
            return _resolver.TryResolve();
        }

        public async Task<ISolution> ResolveAsync()
        {
            return await _resolver.TryResolveAsync();
        }
    }
}
