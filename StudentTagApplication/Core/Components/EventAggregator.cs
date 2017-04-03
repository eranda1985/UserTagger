using Prism.Events;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Components
{
    public class EventAggregator : IEventAggregatorBase
    {
        public delegate void AddTagEventDelegate(List<TagDTO> args);
        public delegate void TagUpdateEventDelegate(TagDTO args);

        public event AddTagEventDelegate AddTagEventObject;
        public event TagUpdateEventDelegate TagUpdateEventObject;

        public AddTagEvent RegisteredAddTagEvent { get; set; }
        public TagUpdateEvent RegisteredTagUpdateEvent { get; set; }

        public void RegisterEvent<TEventType>()
        {
            if(typeof(AddTagEvent).IsAssignableFrom(typeof(TEventType)))
            {
                var temp = RegisteredAddTagEvent;
                if (temp == null)
                    RegisteredAddTagEvent = new AddTagEvent();
            }

            else if (typeof(TagUpdateEvent).IsAssignableFrom(typeof(TEventType)))
            {
                var temp = RegisteredTagUpdateEvent;
                if (temp == null)
                    RegisteredTagUpdateEvent = new TagUpdateEvent();
            }
        }

        public void RePublishAddTagEvent(List<TagDTO> payload)
        {
            AddTagEventObject(payload);
        }

        public void ReSubscribeAddTagEvent(Action<List<TagDTO>> action)
        {
            AddTagEventObject += new AddTagEventDelegate(action);
        }

        public void RePublishTagUpdateEvent(TagDTO payload)
        {
            TagUpdateEventObject(payload);
        }

        public void ReSubscribeTagUpdateEvent(Action<TagDTO> action)
        {
            TagUpdateEventObject += new TagUpdateEventDelegate(action);
        }
    }
}
