using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker
{
    public struct SearchComponents<TResult>
    {
        public ISearchEngineWrapper<TResult> SearchEngineWrapper { get; set; }
        public ISearchResults<TResult> SearchResults { get; set; }
    }
}
