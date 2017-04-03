using System.Configuration;

namespace UniSA.UserTagger
{
    public class PluginElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public string Type
        {
            get
            {
                return this["type"] as string;
            }
        }

        [ConfigurationProperty("test", IsRequired = false, IsKey = true)]
        public string Test
        {
            get
            {
                return this["test"] as string;
            }

            set
            {
                this["test"] = value;
            }
        }

        [ConfigurationProperty("tagName", IsRequired = false, IsKey = true)]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
        public string TagName
        {
            get
            {
                return this["tagName"] as string;
            }

            set
            {
                this["tagName"] = value;
            }
        }
    }
}
