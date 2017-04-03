using System;
using System.Collections.Generic;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.PluginContexts;
using UniSA.UserTagger.Handlers;

namespace UniSA.UserTagger.Plugins
{
    public class DispatcherPlugin : IPlugin
    {
        private IPluginContext _context;
        private Logger _logger;
        private Dictionary<string, object> _propertyBag;

        public string Name
        {
            get
            {
                return "DispatcherPlugin";
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

        public DispatcherPlugin(IPluginContext context)
        {
            _context = context;
            _logger = new Logger(GetType());
            _propertyBag = new Dictionary<string, object>();
        }

        public void PerfromAction()
        {
            _logger.Debug("Dispatcher plugin started");

            var currentContext = _context as PluginContext;

            var eventAggtor = currentContext.EventAggregator as EventAggregator;

            eventAggtor.RegisterEvent<AddTagEvent>();

            var addTagEventInstance = eventAggtor.RegisteredAddTagEvent;

            addTagEventInstance.Initialize(eventAggtor);

            Dispatcher dispatcher = new Dispatcher(eventAggtor);
            
            // Subscribe
            dispatcher.Subscribe(addTagEventInstance);

            _logger.Debug("Dispatcher plugin subscribed");
        }
    }
}
