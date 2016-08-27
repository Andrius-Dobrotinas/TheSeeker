using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Represents an form control object that has a data source and updates to which must be performed on its own thread
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IDataConsumerControl<TResult> : ISyncInvoke, IHaveDataSource<TResult>
    {

    }
}
