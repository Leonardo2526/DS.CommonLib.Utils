using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.TraceUtils
{
    class CriticalActivityBuilder : ActivityBuilder
    {
        public CriticalActivityBuilder(string message, string traceSourceName) : base(message, traceSourceName)
        { }

        public override ActivityBuilder Build()
        {
            TS.TraceEvent(TraceEventType.Critical, 3, Message);

            return this;
        }
    }
}
