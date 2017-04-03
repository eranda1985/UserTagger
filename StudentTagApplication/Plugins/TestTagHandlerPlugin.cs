using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.PluginContexts;
using UniSA.UserTagger.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Plugins
{
    public class TestTagHandlerPlugin : IPlugin
    {
        private IPluginContext _context;
        private Dictionary<string, object> _propertyBag;
        private string _tagName { get; set; }

        public string Name
        {
            get
            {
                return "TestTagHandlerPlugin";
            }
        }

        public Dictionary<string, object> PropertyBag
        {
            get
            {
                return _propertyBag;
            }

            set
            {
                _propertyBag = value;
            }
        }

        public TestTagHandlerPlugin(IPluginContext context)
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

                var handler = new TestTagHandler(_tagName);

                handler.Subscribe(registeredEvent);
            }
        }
    }
}
