using System;
using System.Collections.Generic;

namespace TheSeeker
{
    public interface ISearchManagerFactory
    {
        ISearchManager CreateSearchManager();
    }
}
