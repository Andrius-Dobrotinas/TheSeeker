using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker.Forms
{
    public interface ISearchForm : ICanHide, IDisposable
    {
        /// <summary>
        /// SearchBox that this SearchForm works with
        /// </summary>
        ISearchBox SearchBox { get; }
    }
}
