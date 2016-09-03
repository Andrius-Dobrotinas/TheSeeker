using System;
using System.Configuration;

namespace TheSeeker.Configuration
{
    public class SearchTypeElementCollection : ConfigurationElementCollection
    {
        public new ISearchTypeConfiguration this[string typeName]
        {
            get
            {
                return (ISearchTypeConfiguration)BaseGet(typeName);
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
