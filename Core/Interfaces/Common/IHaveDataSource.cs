using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Represents an object that has a data source
    /// </summary>
    /// <typeparam name="TResult">Type of items this object operates on</typeparam>
    public interface IHaveDataSource<TResult>
    {
        /// <summary>
        /// Object's data source
        /// </summary>
        IList<TResult> DataSource { get; set; }

        /// <summary>
        /// Occurs when DataSource is assigned a value
        /// </summary>
        event EventHandler DataSourceChanged;
    }
}
