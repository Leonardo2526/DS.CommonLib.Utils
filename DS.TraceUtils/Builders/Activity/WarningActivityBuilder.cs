using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.TraceUtils
{
    class WarningActivityBuilder : ActivityBuilder
    {
        public WarningActivityBuilder(string message, string traceSourceName) : base(message, traceSourceName)
        { }

        public override ActivityBuilder Build()
        {
            TS.TraceEvent(TraceEventType.Warning, 2, Message);

            return this;
        }
    }
}
