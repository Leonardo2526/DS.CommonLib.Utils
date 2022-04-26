﻿using DS.MainUtils;
using System;
using System.Diagnostics;
using System.IO;

namespace DS.TraceUtils
{
    public class LogBuilder
    {
        public LogBuilder(DirPathBuilder pathBuilder = null, SourceLevels sourceLevels = SourceLevels.Verbose)
        {
            this.SourceLevels = sourceLevels;

            ExpDirName = GetDirName(pathBuilder);
        }

        private TraceEventType TraceEventType;
        private StreamWriter SW;
        private string Message;
        private string ExpDirName;
        private ActivityBuilder ActivityBuilder;
        private SourceLevels SourceLevels;

        #region Methods

        public void ClearLog()
        {
            System.IO.File.WriteAllText(ExpDirName, string.Empty);
        }

        public void AddMessage(string message, TraceEventType traceEventType, bool append = true)
        {
            this.Message = message;
            this.TraceEventType = traceEventType;
            this.SW = GetStream(ExpDirName, append);

            TraceListener textListener = GetTextTraceListener();
            TraceListener consoleListener = GetConsoleTraceListener();

            Build(textListener);
            //Build(textListener, consoleListener);
        }

        public void AddMessage(string text, bool append = true)
        {
            var sw = GetStream(ExpDirName, append);
            using (sw)
            {
                sw.WriteLine(text);
                //Console.WriteLine(text);
            }
        }

        private void Build(TraceListener listener1, TraceListener listener2 = null)
        {
            ActivityBuilder = GetActivityBuilder();
            ActivityBuilder.TS.Switch.Level = SourceLevels;

            ActivityBuilder.TS.Listeners.Clear();

            AddListener(listener1);
            AddListener(listener2);

            ActivityBuilder.Build();
            ActivityBuilder.TS.Close();
        }

        private void AddListener(TraceListener listener)
        {
            if (listener is not null)
            {
                ActivityBuilder.TS.Listeners.Add(listener);
            }
        }

        private TraceListener GetTextTraceListener()
        {
            var textListener = new TextWriterTraceListener(SW, "name");
            textListener.Name = "WriteListener";
            return textListener;
        }

        private TraceListener GetConsoleTraceListener()
        {
            var consoleListener = new ConsoleTraceListener();
            consoleListener.Name = "ConsoleListener";
            return consoleListener;
        }

        private ActivityBuilder GetActivityBuilder()
        {
            string traceSourceName = "TS";
            ActivityBuilder activityBuilder = null;

            switch (TraceEventType)
            {
                case TraceEventType.Error:
                    activityBuilder = new ErrorActivityBuilder(Message, traceSourceName);
                    break;
                case TraceEventType.Warning:
                    activityBuilder = new WarningActivityBuilder(Message, traceSourceName);
                    break;
                case TraceEventType.Information:
                    activityBuilder = new InfoActivityBuilder(Message, traceSourceName);
                    break;
                case TraceEventType.Critical:
                    activityBuilder = new CriticalActivityBuilder(Message, traceSourceName);
                    break;
                case TraceEventType.Verbose:
                    activityBuilder = new MultipleActivityBuilder(Message, traceSourceName);
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
