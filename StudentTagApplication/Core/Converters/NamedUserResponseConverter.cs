using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.Core.Converters
{
    public class NamedUserResponseConverter : IConverter<NamedUserDeserializer, TagStructureDTO>
    {
        public void Convert(NamedUserDeserializer source, out TagStructureDTO dest)
        {
            dest = new TagStructureDTO();
            if (source != null)
            {
                dest.UidList = new List<string> { source.NamedUser.NamedUserId };
                dest.TagGroups = new Dictionary<string, IEnumerable<string>>();

                foreach (var i in source.NamedUser.Tags)
                {
                    var list = i.Value as JsonArray;
                    dest.TagGroups.Add(i.Key, list.Select(o => o.ToString()).ToList());
                }
            }
        }
    }
}
