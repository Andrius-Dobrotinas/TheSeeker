using System;
using System.Collections.Generic;
using System.Configuration;

namespace TheSeeker.Configuration
{
    public class SearchTypeElementCollection : ConfigurationElementCollection
    {
        public new SearchTypeElement this[string typeName]
        {
            get
            {
                return (SearchTypeElement)BaseGet(typeName);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SearchTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SearchTypeElement)element).TypeName;
        }
    }
}
