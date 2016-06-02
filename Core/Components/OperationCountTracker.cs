using System;
using System.Collections.Generic;

namespace TheSeeker
{
    public class OperationCountTracker : OperationTracker
    {
        private long operationCount;
        private long count = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationCount">Number of checks (calls to CanAct) between each operation</param>
        public OperationCountTracker(long operationCount)
        {
            this.operationCount = operationCount;
        }

        public override bool CanAct
        {
            get
            {
                count++;
                if (count == operationCount)
                {
                    count = 0;
                    return true;
                }
                return false;
            }
        }

        public override void Stop()
        {
            count = 0;
            base.Stop();
        }
    }
}
