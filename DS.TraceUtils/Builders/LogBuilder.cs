using DS.MainUtils;
using System;
using System.Diagnostics;
using System.IO;

namespace DS.TraceUtils
{
    public class LogBuilder
    {
        public LogBuilder(string message, TraceEventType traceEventType,
            DirPathBuilder pathBuilder = null, bool append = false)
        {
            this.Message = message;
            this.TraceEventType = traceEventType;

            string expDirName = GetDirName(pathBuilder);
            SW = GetStream(expDirName, append);

            CreateLog();
        }

        private readonly TraceEventType TraceEventType;
        private readonly StreamWriter SW;
        private readonly string Message;


        #region Methods

        private void CreateLog()
        {
            TraceListener textListener = GetTextTraceListener();
            TraceListener consoleListener = GetConsoleTraceListener();

            ActivityBuilder activityBuilder = GetActivityBuilder();

            activityBuilder.TS.Listeners.Clear();
            activityBuilder.TS.Listeners.Add(textListener);
            activityBuilder.TS.Listeners.Add(consoleListener);

            activityBuilder.Build();
            activityBuilder.TS.Close();
        }

        private TraceListener GetTextTraceListener()
        {
            var textListener = new TextWriterTraceListener(SW, "name");
            textListener.Name = "WriteListener";
            return textListener;
        }

        private TraceListener GetConsoleTraceListener()
        {
            var textListener = new ConsoleTraceListener();
            textListener.Name = "ConsoleListener";
            return textListener;
        }

        private ActivityBuilder GetActivityBuilder()
        {
            string traceSourceName = "TraceSource";
            ActivityBuilder activityBuilder;

            switch (TraceEventType)
            {
                case TraceEventType.Error:
                    activityBuilder = new ErrorActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch = new SourceSwitch("ErrorSwitch", "Error");
                    return activityBuilder;
                case TraceEventType.Verbose:
                    activityBuilder = new MultipleActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch = new SourceSwitch("VerboseSwitch", "Verbose");
                    return activityBuilder;
            }

            return null;
        }

        private string GetDirName(DirPathBuilder pathBuilder)
        {
            if (pathBuilder is null)
            {
                pathBuilder = new DirPathBuilder();
            }
            string dirName = pathBuilder.GetPath();
            return Environment.ExpandEnvironmentVariables(dirName);
        }

        private StreamWriter GetStream(string expDirName, bool append)
        {
            var SWBuilder = new SWBuilderDefault(expDirName, append);
            return SWBuilder.Build();
        }

        #endregion
    }
}
