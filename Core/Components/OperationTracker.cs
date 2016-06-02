using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Default base abstract implementation of IOperationTracker that calls Finished event handler on Stop()
    /// </summary>
    public abstract class OperationTracker : IOperationTracker
    {
        public abstract bool CanAct { get; }

        public virtual event Action Finished;

        public virtual void Stop()
        {
            if (Finished != null)
            {
                Finished();
            }
        }
    }
}
