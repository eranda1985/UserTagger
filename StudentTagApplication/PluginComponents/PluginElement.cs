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

        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public string Id
        {
            get
            {
                return this["id"] as string;
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

        [ConfigurationProperty("dbConnectionName", IsRequired = false, IsKey = true)]
        public string DBConnectionName
        {
            get
            {
                return this["dbConnectionName"] as string;
            }

            set
            {
                this["dbConnectionName"] = value;
            }
        }

        [ConfigurationProperty("filePath", IsRequired = false, IsKey = true)]
        public string FilePath
        {
            get
            {
                return this["filePath"] as string;
            }

            set
            {
                this["filePath"] = value;
            }
        }
    }
}
