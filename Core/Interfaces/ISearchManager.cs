﻿using System;
using System.Collections.Generic;

namespace TheSeeker
{
    public interface ISearchManager : ISearchFinishedHandler
    {
        /// <summary>
        /// Indicates whether a search is currently running
        /// </summary>
        bool IsSearching { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchLocation"></param>
        /// <param name="searchPattern"></param>
        /// <returns>true if new search started, false if there was an active search running</returns>
        bool Search(string searchLocation, string searchPattern);

        /// <summary>
        /// Stops the current search
        /// </summary>
        void Stop();
    }
}