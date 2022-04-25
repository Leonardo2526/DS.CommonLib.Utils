using System.Diagnostics;

namespace DS.TraceUtils
{
    public abstract class ActivityBuilder
    {
        public ActivityBuilder(string message, string traceSourceName)
        {
            Message = message;            
            TS = new TraceSource(traceSourceName);
        }

        public TraceSource TS { get; protected set; }
        public string Message { get; protected set; }

    public abstract ActivityBuilder Build();
      
    }
}
