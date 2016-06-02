using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Used for telling the caller whether it can act
    /// </summary>
    public interface IOperationTracker
    {
        /// <summary>
        /// Indicates whether an operation can be performed
        /// </summary>
        bool CanAct { get; }

        /// <summary>
        /// Stops whatever mechanism an implementing class is using to track operations
        /// and resets its internal state
        /// </summary>
        void Stop();

        /// <summary>
        /// Occurs when the tracker's work is done
        /// </summary>
        event Action Finished;
    }
}
