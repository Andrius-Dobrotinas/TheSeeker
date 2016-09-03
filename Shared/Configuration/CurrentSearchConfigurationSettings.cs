using System;
using System.Collections.Generic;
using System.Configuration;

namespace TheSeeker.Configuration
{
    public abstract class CurrentSearchConfiguration : ConfigurationSection, ICurrentSearchConfiguration
    {
        /// <summary>
        /// Type of items produced by Search Engine that transport found item data
        /// </summary>
        [ConfigurationProperty("searchType", IsRequired = true)]
        public string SearchType
        {
            get
            {
                return (string)this["searchType"];
            }
        }

        /// <summary>
        /// Type of object that controls the flow of search results
        /// </summary>
        [ConfigurationProperty("operationTrackerType", IsRequired = true)]
        public string OperationTrackerType
        {
            get
            {
                return (string)this["operationTrackerType"];
            }
        }

        /// <summary>
        /// Interval at which found items list is to be refreshed
        /// </summary>
        [ConfigurationProperty("listRefreshInterval", IsRequired = false, DefaultValue = 100)]
        public int ListRefreshInterval
        {
            get
            {
                return (int)this["listRefreshInterval"];
            }
        }
    }
}
