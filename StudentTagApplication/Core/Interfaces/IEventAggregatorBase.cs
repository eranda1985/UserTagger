using UniSA.UserTagger.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IEventAggregatorBase
    {
        void RePublishAddTagEvent(List<TagDTO> payload);
        void RePublishTagUpdateEvent(TagDTO payload);
        void ReSubscribeAddTagEvent(Action<List<TagDTO>> action);
        void ReSubscribeTagUpdateEvent(Action<TagDTO> action);
    }
}
