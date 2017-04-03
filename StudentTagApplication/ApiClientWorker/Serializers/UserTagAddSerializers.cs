using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.JsonSerializers
{
    public class JsonStringAddTags
    {
        public Audience audience { get; set; }
        public Dictionary<string, IEnumerable<string>> add { get; set; }
    }
}
