using System;
using System.Configuration;

namespace UniSA.UserTagger
{
    public class PluginElementCollection: ConfigurationElementCollection
    {
        public PluginElement this[object key]
        {
            get
            {
                return base.BaseGet(key) as PluginElement;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "plugin";
            }
        }

        protected override bool IsElementName(string elementName)
        {
            bool isName = false;
            if (!String.IsNullOrEmpty(elementName))
                isName = elementName.Equals("plugin");
            return isName;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PluginElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PluginElement)element).Type;
        }
    }
}