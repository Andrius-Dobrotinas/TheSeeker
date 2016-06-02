using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Represents an object that consumes search results and exposes its Data Source property 
    /// </summary>
    public interface IHaveStatus
    {
        string Status { get; set; }
    }

    public interface IAmReInitializable
    {
        /// <summary>
        /// Initializes or reinitializes the object and the Data Source for a new search
        /// </summary>
        void ReInitialize();
    }
}