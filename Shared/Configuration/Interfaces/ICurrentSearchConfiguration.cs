using System;

namespace TheSeeker.Configuration
{
    public interface ICurrentSearchConfiguration
    {
        string SearchType { get; }
        string OperationTrackerType { get; }
        int ListRefreshInterval { get; }
    }
}
