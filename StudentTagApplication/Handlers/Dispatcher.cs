using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Subscribers
{
    public class Dispatcher : ISubscriber<AddTagEvent>
    {
        private BaseTagUpdatePublisher _tagUpdatePublisher;
        private IEventAggregatorBase _eventAggregator;
        Logger _logger;

        public Dispatcher(IEventAggregatorBase eventAggregator)
        {
            _tagUpdatePublisher = _tagUpdatePublisher ?? new BaseTagUpdatePublisher();
            _eventAggregator = eventAggregator;
            _logger = new Logger(GetType());
        }
        public void Subscribe(AddTagEvent args)
        {
            var eventAggtor = _eventAggregator as EventAggregator;

            eventAggtor.RegisterEvent<TagUpdateEvent>();

            var tagUpdateEventInstance = eventAggtor.RegisteredTagUpdateEvent;

            tagUpdateEventInstance.Initialize(eventAggtor); 

            args.Subscribe((x) =>
            {
                _logger.Debug("Dispatcher triggered");

                x.ForEach(tag =>
                {
                    _tagUpdatePublisher.Publish(tagUpdateEventInstance, tag);
                });
            });
        }
    }
}
