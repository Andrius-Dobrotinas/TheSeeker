using System;
using System.Collections.Generic;

namespace TheSeeker
{
    public interface ISearchFinishedHandler
    {
        /// <summary>
        /// Occurs when the search has finished (naturally or due to having been cancelled)
        /// </summary>
        event EventHandler<TimeSpan> SearchFinished;
    }
}
