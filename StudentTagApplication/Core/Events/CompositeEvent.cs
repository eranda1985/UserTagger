using Prism.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Events
{
    public abstract class CompositeEvent<T>: EventBase
    {
        protected IEventAggregatorBase aggregator;

        public void Initialize(IEventAggregatorBase aggregator)
        {
            this.aggregator = aggregator;
        }

        public abstract void Publish(T payload);
        public abstract void Subscribe(Action<T> action);
    }
}
