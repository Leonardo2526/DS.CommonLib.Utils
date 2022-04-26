using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.TraceUtils
{
    class InfoActivityBuilder : ActivityBuilder
    {
        public InfoActivityBuilder(string message, string traceSourceName) : base(message, traceSourceName)
        { }

        public override ActivityBuilder Build()
        {
            TS.TraceEvent(TraceEventType.Information, 4, Message);

            return this;
        }
    }
}
