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
}
