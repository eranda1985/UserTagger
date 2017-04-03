using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;

namespace UniSA.UserTagger.Core.Events
{
    public class TagUpdateEvent : CompositeEvent<TagDTO>
    {
        public override void Publish(TagDTO payload)
        {
            aggregator.RePublishTagUpdateEvent(payload);
        }

        public override void Subscribe(Action<TagDTO> action)
        {
            aggregator.ReSubscribeTagUpdateEvent(action);
        }
    }
}
