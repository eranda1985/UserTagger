using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.Core.Repository;
using UniSA.UserTagger.PluginContexts;
using UniSA.UserTagger.Publishers;
using System.Collections.Generic;
using System;

namespace UniSA.UserTagger.Plugins
{
    public class NotifierPlugin : IPlugin
    {
        private IPluginContext _context;
        private Logger _logger;
        private Dictionary<string, object> _propertyBag;

        public string Name
        {
            get
            {
                return "NotifierPlugin";
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

        public NotifierPlugin(IPluginContext context)
        {
            _context = context;
            _logger = new Logger(GetType());
            _propertyBag = new Dictionary<string, object>();
        }

        public void PerfromAction()
        {
            var pluginContext = _context as PluginContext;

            _logger.Debug("Notifier Plugin started");

            var eventAggtor = pluginContext.EventAggregator as EventAggregator;

            eventAggtor.RegisterEvent<AddTagEvent>();

            var addTagEventInstance = eventAggtor.RegisteredAddTagEvent;

            addTagEventInstance.Initialize(eventAggtor);

            Notifier notifier = new Notifier();

            // get new Tags from DB
            List<TagDTO> result = new List<TagDTO>();

            try
            {
                _logger.Debug("Retrieving Tags from Database..");

                using (var repo = new TagRepository())
                {
                    var list = repo.List("select t.* from Tags t");

                    // Convert to DTO's 
                    foreach (var l in list)
                    {
                        var tempDto = new TagDTO();
                        pluginContext.TagModelConverter.Convert(l, out tempDto);
                        result.Add(tempDto);
                    }
                }

                _logger.Debug("Tags Retrieved and preparing to publish..");

                // Publish with DTO's as the payload 
                notifier.Publish(addTagEventInstance, result);

                _logger.Debug("Tags Published");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
    }
}
