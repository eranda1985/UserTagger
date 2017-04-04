using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.Handlers;
using UniSA.UserTagger.PluginContexts;

namespace UniSA.UserTagger.Plugins
{
    public class AssignmentTagHandlerPlugin : IPlugin
    {
        private IPluginContext _context;
        private Dictionary<string, object> _propertyBag;
        private Logger _logger;

        private string _tagName { get; set; }
        

        public string Name
        {
            get
            {
                return "AssignmentTagHandlerPlugin";
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

        public AssignmentTagHandlerPlugin(IPluginContext context)
        {
            _context = context;
            _propertyBag = new Dictionary<string, object>();
            _logger = new Logger(GetType());
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

                var handler = new AssignmentTagHandler(_tagName, pluginContext.ApiClientWorker, pluginContext.TagDTOConverter);

                try
                {
                    handler.Subscribe(registeredEvent);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.ToString());
                }
            }
        }
    }
}
