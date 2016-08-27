using System;
using System.Collections.Generic;

namespace TheSeeker
{
    

    public interface IAmReInitializable
    {
        /// <summary>
        /// Initializes or reinitializes the object and the Data Source for a new search
        /// </summary>
        void ReInitialize();
    }
}