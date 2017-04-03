using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Publishers
{
    public class Notifier : IPublisher<AddTagEvent, List<TagDTO>>
    {
        public void Publish(AddTagEvent args, List<TagDTO> payload)
        {
            args.Publish(payload);
        }
    }
}
