using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.DTO
{
    public class TagStructureDTO
    {
        public List<string> UidList { get; set; }
        public Dictionary<string, IEnumerable<string>> TagGroups { get; set; }

        public TagStructureDTO()
        {
            UidList = new List<string>();
            TagGroups = new Dictionary<string, IEnumerable<string>>();
        }
    }
}
