using System;

namespace TheSeeker.Configuration
{
    public interface ISearchTypeConfiguration
    {
        string TypeName { get; }
        string SearchEngineType { get; }
        string SearchResultsConsumerType { get; }
        string SearchManagerType { get; }
    }
}
