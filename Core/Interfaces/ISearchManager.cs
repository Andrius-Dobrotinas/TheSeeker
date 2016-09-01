using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Interface for factories for creating search managers 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ISearchManager<TResult> : ISearchManager
    {
        /// <summary>
        /// Implementations must create a Search Manager using the supplied components
        /// </summary>
        void Create(ISearchEngineWrapper<TResult> searchEngineWrapper, IOperationTracker operationTracker);
    }

    public interface ISearchManager : IDisposableOnce
    {
        ISearchEngineWrapper SearchEngineWrapper { get; }
        IOperationTracker OperationTracker { get; }
        ISearchBox SearchBox { get; }
    }
}
