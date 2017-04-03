using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Events
{
    public class AddTagEvent: CompositeListEvent<TagDTO, List<TagDTO>>
    {
        private IEventAggregatorBase _aggregator;

        public void Initialize(IEventAggregatorBase aggregator)
        {
            this._aggregator = aggregator;
        }
        public override void Publish(List<TagDTO> payload)
        {
            _aggregator.RePublishAddTagEvent(payload);
        }

        public override void Subscribe(Action<List<TagDTO>> action)
        {
            _aggregator.ReSubscribeAddTagEvent(action);
        }
    }
}
