using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Handlers;
using UniSA.UserTagger.PluginContexts;

namespace UniSA.UserTagger.Plugins
{
    public class GenericTagHandlerPlugin : IPlugin
    {
        public string Name => "GenericTagHandlerPlugin";

        private IPluginContext _context;
        private Dictionary<string, object> _propertyBag;
        private string _tagName { get; set; }


        public Dictionary<string, object> PropertyBag { get => _propertyBag; set { _propertyBag = value; } }

        public GenericTagHandlerPlugin(IPluginContext context)
        {
            _context = context;
            _propertyBag = new Dictionary<string, object>();
        }

        public void PerfromAction()
        {
            var val = _propertyBag.SingleOrDefault(p => p.Key == "TagName").Value;

            if (val != null)
            {
                _tagName = val as string;

                var pluginContext = _context as PluginContext;

                var eventAggtor = pluginContext.EventAggregator as EventAggregator;

                eventAggtor.RegisterEvent<TagUpdateEvent>();

                var registeredEvent = eventAggtor.RegisteredTagUpdateEvent;

                registeredEvent.Initialize(eventAggtor);

                var connString = _propertyBag.SingleOrDefault(p => p.Key == "DBConnectionName").Value as string;

                var handler = new GenericTagHandler(_tagName, connString, pluginContext.ApiClientWorker, pluginContext.TagDTOConverter);

                handler.Subscribe(registeredEvent);
            }
        }
    }
}
