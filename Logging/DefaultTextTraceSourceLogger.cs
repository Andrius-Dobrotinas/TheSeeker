using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace TheSeeker.Logging
{
    public class DefaultTextTraceSourceLogger : ILogger
    {
        protected TraceSource trace;
        protected FileStream fileStream;
        protected TextWriterTraceListener textListener;

        public DefaultTextTraceSourceLogger(string name)
        {
            var logFileName = ConfigurationManager.AppSettings["logFileName"];
            if (string.IsNullOrWhiteSpace(logFileName))
                throw new ArgumentException("Configuration element is empty", "logFileName");

            trace = new TraceSource(name, SourceLevels.All);

            if (!File.Exists(logFileName))
                File.Create(logFileName);

            fileStream = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

            textListener = new TextWriterTraceListener(fileStream);
            trace.Listeners.Add(textListener);
        }

        public void LogError(string message, object obj)
        {
            trace.TraceData(TraceEventType.Error, 0, $"{DateTime.Now, 0:u} {message}", obj);
            trace.Flush();
        }

        public void LogError(string message)
        {
            trace.TraceEvent(TraceEventType.Error, 0, $"{DateTime.Now, 0:u} {message}");
            trace.Flush();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    fileStream.Dispose();
                    textListener.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
    #endregion
}