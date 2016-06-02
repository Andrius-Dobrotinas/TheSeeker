using System;
using System.Collections.Generic;
using System.Threading;

namespace TheSeeker
{
    public interface ISearchAsync
    {
        /// <summary>
        /// Searches the specified location using the supplied search pattern
        /// </summary>
        /// <param name="location">Location to search</param>
        /// <param name="searchPattern"></param>
        /// <param name="cancellationToken">Search cancellation token</param>
        void Search(string location, string searchPattern, CancellationToken cancellationToken);
    }
}
