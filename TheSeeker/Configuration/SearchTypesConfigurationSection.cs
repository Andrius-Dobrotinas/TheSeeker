using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TheSeeker.Configuration
{
    public class SearchTypesConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("SearchTypes", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SearchTypeElementCollection),
            AddItemName = "Type")]
        public SearchTypeElementCollection SearchTypes
        {
            get
            {
                return (SearchTypeElementCollection)base["SearchTypes"];
            }
        }
    }
}
