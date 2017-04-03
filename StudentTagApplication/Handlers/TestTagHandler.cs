using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Subscribers
{
    public class TestTagHandler : BaseHandler, ISubscriber<TagUpdateEvent>
    {
        public TestTagHandler(string tagName):base(tagName)
        {

        }
        public void Subscribe(TagUpdateEvent args)
        {
            args.Subscribe(x =>
            {
                if(tagName == x.Name)
                {
                    // Do stuff related to this tag
                }
            });
        }
    }
}
