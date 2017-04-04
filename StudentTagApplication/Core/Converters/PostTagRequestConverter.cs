using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.JsonSerializers;

namespace UniSA.UserTagger.Core.Converters
{
    public class PostTagRequestConverter : IConverter<TagStructureDTO, string>
    {
        public void Convert(TagStructureDTO source, out string dest)
        {
            dest = "";
        }

        public void ConvertForAddTag(TagStructureDTO source, out string dest)
        {
            dest = "";
            UserTagAddSerializers jsonBody = new UserTagAddSerializers
            {
                audience = new Audience { named_user_id = new List<string>() },
                add = new Dictionary<string, IEnumerable<string>>(),
            };

            jsonBody.audience.named_user_id.AddRange(source.UidList);
            jsonBody.add = source.TagGroups;
            dest = JsonConvert.SerializeObject(jsonBody);
        }

        public void ConvertForRemoveTag(TagStructureDTO source, out string dest)
        {
            dest = "";
            UserTagRemoveSerializer jsonBody = new UserTagRemoveSerializer
            {
                audience = new Audience { named_user_id = new List<string>() },
                remove = new Dictionary<string, IEnumerable<string>>(),
            };

            jsonBody.audience.named_user_id.AddRange(source.UidList);
            jsonBody.remove = source.TagGroups;
            dest = JsonConvert.SerializeObject(jsonBody);
        }
    }
}
