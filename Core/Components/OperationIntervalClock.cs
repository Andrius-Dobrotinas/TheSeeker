using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TheSeeker
{
    public class OperationIntervalClock : OperationTracker
    {
        private Stopwatch sw = new Stopwatch();
        private long operationInterval;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationInterval">Amount of time (in milliseconds) between each operation</param>
        public OperationIntervalClock(long operationInterval)
        {
            this.operationInterval = operationInterval;
        }

        public override bool CanAct
        {
            get
            {
                if (sw.IsRunning)
                {
                    if (sw.ElapsedMilliseconds > operationInterval)
                    {
                        sw.Restart();
                        return true;
                    }
                }
                else
                {
                    sw.Start();
                }
                return false;
            }
        }

        public override void Stop()
        {
            sw.Reset();
            base.Stop();
        }
    }
}
