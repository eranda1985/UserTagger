using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.PluginContexts;
using UniSA.UserTagger.Subscribers;

namespace UniSA.UserTagger.Plugins
{
    public class ScholarshipTagHandlerPlugin : IPlugin
    {
        private IPluginContext _context;
        private Dictionary<string, object> _propertyBag;
        private Logger _logger;

        private string _tagName { get; set; }

        public string Name
        {
            get
            {
                return "ScholarshipTagHandlerPlugin";
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

        public ScholarshipTagHandlerPlugin(IPluginContext context)
        {
            _context = context;
            _logger = new Logger(GetType());
            _propertyBag = new Dictionary<string, object>();
        }

        public void PerfromAction()
        {
            var value = _propertyBag.SingleOrDefault(p => p.Key == "TagName").Value;

            if(value != null)
            {
                _tagName = value as string;

                var pluginContext = _context as PluginContext;

                var eventAggtor = pluginContext.EventAggregator as EventAggregator;

                eventAggtor.RegisterEvent<TagUpdateEvent>();

                var registeredEvent = eventAggtor.RegisteredTagUpdateEvent;

                registeredEvent.Initialize(eventAggtor);

                var handler = new ScholarshipTagHandler(_tagName, pluginContext.ApiClientWorker);

                handler.Subscribe(registeredEvent);
            }
        }
    }
}
