using System.Collections.Generic;
using Newtonsoft.Json;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.JsonSerializers;

namespace UniSA.UserTagger.Core.Converters
{
    public class AddTagRequestConverter : IConverter<TagStructureDTO, string>
    {
        public void Convert(TagStructureDTO source, out string dest)
        {
            JsonStringAddTags jsonBody = new JsonStringAddTags
            {
                audience = new Audience { named_user_id = new List<string>() },
                add = new Dictionary<string, IEnumerable<string>>(),
            };

            jsonBody.audience.named_user_id.AddRange(source.UidList);
            jsonBody.add = source.TagGroups;
            dest = JsonConvert.SerializeObject(jsonBody);
        }
    }
}
