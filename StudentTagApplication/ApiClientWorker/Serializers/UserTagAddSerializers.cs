using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Interfaces;

namespace UniSA.UserTagger.Core.JsonSerializers
{
    public class UserTagAddSerializers: ISerializer
    {
        public Audience audience { get; set; }
        public Dictionary<string, IEnumerable<string>> add { get; set; }
    }
}
