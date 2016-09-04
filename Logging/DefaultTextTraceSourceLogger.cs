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

        /// <summary>
        /// Instantiates new logger
        /// </summary>
        /// <param name="name">Logger name</param>
        public DefaultTextTraceSourceLogger(string name)
        {
            var logFileName = ConfigurationManager.AppSettings["logFileName"];
            if (string.IsNullOrWhiteSpace(logFileName))
                throw new ArgumentException("Configuration element is empty", "logFileName");

            // Check if it's absolute or relative path name
            if (logFileName.StartsWith("\\") || logFileName.StartsWith("/"))
                logFileName = Path.Combine(Directory.GetCurrentDirectory(), logFileName.Substring(1));

            // Create file if it doesn't exist
            if (!File.Exists(logFileName))
                File.Create(logFileName).Dispose();

            fileStream = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

            textListener = new TextWriterTraceListener(fileStream);

            trace = new TraceSource(name, SourceLevels.All);
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