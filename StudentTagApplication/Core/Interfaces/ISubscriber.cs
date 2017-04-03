using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface ISubscriber<T> where T: EventBase
    {
        void Subscribe(T args);
    }
}
