using Microsoft.Practices.Unity;
using UniSA.UserTagger.Core.Installers;
using UniSA.UserTagger.Core.Interfaces;
using System;

namespace UniSA.UserTagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var registeredPlugins = PluginSection.Section.PluginMap;

            using (var container = new UnityContainer())
            {
                Installer.RegisterTypes(container);

                foreach (var item in registeredPlugins)
                {
                    //Code goes here to instantiate and invoke the plugins
                    var p = item as PluginElement;
                    var type = Type.GetType(p.Type);
                    var plugin = container.Resolve(type, type.Name) as IPlugin;

                    if (!string.IsNullOrEmpty(p.TagName))
                    {
                        plugin.PropertyBag.Add("TagName", p.TagName);
                        plugin.PropertyBag.Add("DBConnectionName", p.DBConnectionName);
                        plugin.PropertyBag.Add("FilePath", p.FilePath);
                    }

                    plugin.PerfromAction();
                }
            }
        }
    }
}
