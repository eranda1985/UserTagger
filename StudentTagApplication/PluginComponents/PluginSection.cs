using System.Configuration;

namespace UniSA.UserTagger
{
    public class PluginSection : ConfigurationSection
    {
        private static PluginSection section = ConfigurationManager.GetSection("PluginSection") as PluginSection;

        public static PluginSection Section
        {
            get
            {
                return section;
            }
        }

        [ConfigurationProperty("plugins", IsRequired = true)]
        public PluginElementCollection PluginMap
        {
            get
            {
                return this["plugins"] as PluginElementCollection;
            }
        }
    }
}

