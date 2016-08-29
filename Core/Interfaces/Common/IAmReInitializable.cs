using System;
using System.Collections.Generic;

namespace TheSeeker
{
    public interface IAmReInitializable
    {
        /// <summary>
        /// Initializes or reinitializes the object instead of creating a new instance
        /// </summary>
        void ReInitialize();
    }
}