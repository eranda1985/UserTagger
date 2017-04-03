using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Handlers
{
    public class BaseHandler
    {
        protected string tagName { get; set; }
        public BaseHandler(string tagName)
        {
            this.tagName = tagName;
        }
    }
}
