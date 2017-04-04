using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Validators;

namespace UniSA.UserTagger.Core.Converters
{
    public class NamedUsersResponseConverter : IConverter<NamedUsersDeserializer, List<TagStructureDTO>>
    {
        public void Convert(NamedUsersDeserializer source, out List<TagStructureDTO> dest)
        {
            dest = new List<TagStructureDTO>();

            UserTaggerValidator.CheckNull(source.NamedUsers, "NamedUsers");

            if (source != null)
            {
                foreach (var user in source.NamedUsers)
                {
                    var temp = new TagStructureDTO();
                    temp.UidList = new List<string> { user.NamedUserId };
                    temp.TagGroups = new Dictionary<string, IEnumerable<string>>();

                    foreach (var i in user.Tags)
                    {
                        var list = i.Value as JsonArray;
                        temp.TagGroups.Add(i.Key, list.Select(o => o.ToString()).ToList());
                    }
                    dest.Add(temp);
                }
            }
        }
    }
}
