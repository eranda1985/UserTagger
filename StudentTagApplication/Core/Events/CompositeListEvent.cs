using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Events
{
    public class CompositeListEvent<U,T>: EventBase where T : IList<U> where U: new()
    {
        public virtual void Publish(T payload) { }
        public virtual void Subscribe(Action<T> action) { }
    }
}
