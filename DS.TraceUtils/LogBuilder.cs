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

            ExpDirName = GetDirName(pathBuilder);
            SW = GetStream(ExpDirName, append);

            CreateLog();
        }

        private TraceEventType TraceEventType;
        private StreamWriter SW;
        private string Message;
        private TraceListener TextListener;
        private string ExpDirName;

        #region Methods

        public void AddMessage(string message, TraceEventType traceEventType)
        {
            this.Message = message;
            this.TraceEventType = traceEventType;
            this.SW = GetStream(ExpDirName, true);

            CreateLog();
        }

        private void CreateLog()
        {
            TextListener = GetTextTraceListener();
            TraceListener consoleListener = GetConsoleTraceListener();

            ActivityBuilder activityBuilder = GetActivityBuilder();

            activityBuilder.TS.Listeners.Clear();
            activityBuilder.TS.Listeners.Add(TextListener);
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
            string traceSourceName = "TS";
            ActivityBuilder activityBuilder = null;

            switch (TraceEventType)
            {
                case TraceEventType.Error:
                    activityBuilder = new ErrorActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch.Level = SourceLevels.Error;
                    break;
                case TraceEventType.Warning:
                    activityBuilder = new WarningActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch.Level = SourceLevels.Warning;
                    break;
                case TraceEventType.Information:
                    activityBuilder = new InfoActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch.Level = SourceLevels.Information;
                    break;
                case TraceEventType.Critical:
                    activityBuilder = new CriticalActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch.Level = SourceLevels.Critical;
                    break;
                case TraceEventType.Verbose:
                    activityBuilder = new MultipleActivityBuilder(Message, traceSourceName);
                    activityBuilder.TS.Switch.Level = SourceLevels.Verbose;
                    break;
            }
                    return activityBuilder;

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
