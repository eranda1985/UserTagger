using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.ApiClientWorker.Deserializers
{
    public class NamedUser
    {
        public string NamedUserId { get; set; }
        public Dictionary<string, object> Tags { get; set; }

        public NamedUser()
        {
            Tags = new Dictionary<string, object>();
        }
    }
}
