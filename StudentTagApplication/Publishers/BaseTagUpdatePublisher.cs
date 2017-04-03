using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;

namespace UniSA.UserTagger.Publishers
{
    public class BaseTagUpdatePublisher : IPublisher<TagUpdateEvent, TagDTO>
    {
        public void Publish(TagUpdateEvent args, TagDTO payload)
        {
            args.Publish(payload);
        }
    }
}
