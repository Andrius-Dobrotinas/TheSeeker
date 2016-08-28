using System;
using System.Collections.Generic;

namespace TheSeeker.Startup
{
    public struct StartupObjects
    {
        public ISearchManager SearchManager { get; set; }
        public object[] OriginalInputObjects { get; set; }
    }
}
