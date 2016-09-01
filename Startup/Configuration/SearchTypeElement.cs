using System;
using System.Collections.Generic;
using System.Configuration;

namespace TheSeeker.Configuration
{
    public class SearchTypeElement : ConfigurationElement
    {
        /// <summary>
        /// Node name in application configuration file
        /// </summary>
        internal const string Name = "SearchType";

        /// <summary>
        /// Type of items produced by Search Engine that transport found item data
        /// </summary>
        [ConfigurationProperty("typeName", IsRequired = true)]
        public string TypeName
        {
            get
            {
                return (string)this["typeName"];
            }
        }

        [ConfigurationProperty("searchEngineType", IsRequired = true)]
        public string SearchEngineType
        {
            get
            {
                return (string)this["searchEngineType"];
            }
        }

        /// <summary>
        /// Type of object that consumers items found by the Search Engine
        /// </summary>
        [ConfigurationProperty("searchResultsConsumerType", IsRequired = true)]
        public string SearchResultsConsumerType
        {
            get
            {
                return (string)this["searchResultsConsumerType"];
            }
        }

        /// <summary>
        /// Type of factory that creates a Search Manager with injected Search Engine, Consumer and Operation Tracker
        /// </summary>
        [ConfigurationProperty("searchManagerType", IsRequired = true)]
        public string SearchManagerType
        {
            get
            {
                return (string)this["searchManagerType"];
            }
        }
    }
}
