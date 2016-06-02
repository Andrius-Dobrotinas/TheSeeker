using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker.Forms
{
    public class ConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("listRefreshInterval", IsRequired = false, DefaultValue = 100)]
        public int ListRefreshInterval
        {
            get
            {
                return (int)this["listRefreshInterval"];
            }
        }

        [ConfigurationProperty("searchEngine", IsRequired = true)]
        public string SearchEngine
        {
            get
            {
                return (string)this["searchEngine"];
            }
        }
    }
}
