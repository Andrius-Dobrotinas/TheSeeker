using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace TheSeeker.Logging
{
    public interface ILogger : IDisposable
    {
        void LogError(string message, object obj);
        void LogError(string message);
    }
}