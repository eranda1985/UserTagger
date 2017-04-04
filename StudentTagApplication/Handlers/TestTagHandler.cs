using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Handlers
{
    public class TestTagHandler : BaseHandler, ISubscriber<TagUpdateEvent>
    {
        public TestTagHandler(string tagName):base(tagName)
        {

        }
        public void Subscribe(TagUpdateEvent args)
        {
            args.Subscribe(incomingTag =>
            {
                if (tagName == incomingTag.Name)
                {
                    // Do stuff related to this tag
                    if (incomingTag.IsNew && incomingTag.IsInstall)
                    {
                        // Add tag logic
                    }

                    else if (incomingTag.IsNew && (!incomingTag.IsInstall))
                    {
                        // Logic for Remove tag
                    }
                }
            });
        }
    }
}
