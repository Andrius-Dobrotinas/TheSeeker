using System;
using System.Collections.Generic;

namespace TheSeeker.Forms
{
    public interface ISearchResultsConsumerControl<TResult> : ISearchResultsConsumer<TResult>,
        IDataConsumerControl<TResult>,
        IHaveStatus,
        IAmReInitializable,
        IDisposable
    {

    }
}
