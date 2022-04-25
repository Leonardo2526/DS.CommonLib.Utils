using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.TraceUtils
{
    class ErrorActivityBuilder : ActivityBuilder
    {
        public ErrorActivityBuilder(string message, string traceSourceName) : base(message, traceSourceName)
        { }

        public override ActivityBuilder Build()
        {
            TS.TraceEvent(TraceEventType.Error, 1, "Error message: " + Message);

            return this;
        }
    }
}
