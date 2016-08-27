using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace TheSeeker.Configuration
{
    /// <summary>
    /// General settings for displaying search results in Windows Forms application
    /// </summary>
    public class CurrentFormsSearchConfiguration : CurrentSearchConfiguration
    {
        /// <summary>
        /// Node name in application configuration file
        /// </summary>
        internal const string Name = "FormDisplaySearchResultsSettings";
    }
}
