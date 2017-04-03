using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.ApiClientWorker.Interfaces
{
    public interface IUrbanAirshipClientWorker
    {
        NamedUserDeserializer GetUserById(string id, IApiClient urbanAirshipClient);
        PostTagResponse PostTagToNamedUsers(string requestBody, IApiClient urbanAirshipClient);
        PostTagResponse ProcessAll(TagStructureDTO source);
        bool NoDuplicateTagsInDestinationUser(TagStructureDTO source, TagStructureDTO dest);
    }
}
