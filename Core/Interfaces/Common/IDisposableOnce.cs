using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Used for types that go into invalid state and cannot be used anymore after being disposed of
    /// </summary>
    public interface IDisposableOnce : IDisposable
    {
        /// <summary>
        /// Indicates whether the object has been disposed of and cannot be used anymore due to being in an invalid state
        /// </summary>
        bool IsDisposed { get; }
    }
}
