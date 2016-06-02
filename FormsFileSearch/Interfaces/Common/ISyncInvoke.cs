using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Represents an object created on a separate thread and therefore requiring updates to it to be performed via Invoke method and only when the handle is created
    /// </summary>
    public interface ISyncInvoke
    {
        /// <summary>
        /// Invokes a method on the thread that the object has been created on
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        object Invoke(Delegate method);

        /// <summary>
        /// Indicates that object's handle has been created and Invoke may be called
        /// </summary>
        bool IsHandleCreated { get; }
    }
}
