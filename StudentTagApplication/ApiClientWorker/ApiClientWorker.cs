using System.Linq;
using System.Collections.Generic;
using RestSharp;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.Converters;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;

namespace UniSA.UserTagger.ApiClientWorker
{
    public class UrbanAirshipClientWorker: IUrbanAirshipClientWorker
    {
        private IApiClientFactory _apiClientFactory;
        private IConverter<NamedUserDeserializer, TagStructureDTO> _namedUserConverter;
        private IConverter<NamedUsersDeserializer, List<TagStructureDTO>> _namedUsersConverter;
        private AddTagRequestConverter _addTagRequestConverter;
        private RemoveTagRequestConverter _removeTagRequestConverter;
        private ILogger _logger;
        private string _tagName;

        public UrbanAirshipClientWorker(
            IApiClientFactory apiClientfactory, 
            IConverter<NamedUserDeserializer, TagStructureDTO> namedUserConverter,
            IConverter<NamedUsersDeserializer, List<TagStructureDTO>> namedUsersConverter,
            RemoveTagRequestConverter removeTagRequestConverter,
            AddTagRequestConverter addTagRequestConverter)
        {
            _apiClientFactory = apiClientfactory;
            _namedUserConverter = namedUserConverter;
            _namedUsersConverter = namedUsersConverter;
            _addTagRequestConverter = addTagRequestConverter;
            _removeTagRequestConverter = removeTagRequestConverter;
            _logger = new Logger(GetType());
        }

        public PostTagResponse ProcessTagAdd(TagStructureDTO source)
        {
            TagStructureDTO dest = new TagStructureDTO();
            TagStructureDTO resultDTO;

            var urbanAirshipClient = _apiClientFactory.Create(Core.Enums.ApiClientTypes.UrbanAirshipAPIClient);
            _tagName = source.TagGroups.SingleOrDefault().Value.SingleOrDefault();

            foreach (var item in source.UidList)
            {
                var destinationUser = GetUserById(item.ToLower(), urbanAirshipClient);
                _namedUserConverter.Convert(destinationUser, out resultDTO);

                if (resultDTO == null || (resultDTO.UidList.Count == 0))
                    continue;

                dest.UidList.Add(resultDTO.UidList.SingleOrDefault());
            }

            if (dest.UidList.Count > 0)
            {
                dest.TagGroups = source.TagGroups;
                string jsonstring;
                _addTagRequestConverter.Convert(dest, out jsonstring);
                return PostTagToNamedUsers(jsonstring, urbanAirshipClient);
            }

            return new PostTagResponse { IsActionCompleted = true};
        }

        public PostTagResponse ProcessTagRemove(TagDTO tag)
        {
            TagStructureDTO dest = new TagStructureDTO();
            var urbanAirshipClient = _apiClientFactory.Create(Core.Enums.ApiClientTypes.UrbanAirshipAPIClient);
            var tempNamedUserResult = new NamedUsersDeserializer();
            var allUsers = GetAllNamedUsers(urbanAirshipClient);
            tempNamedUserResult.NamedUsers.AddRange(allUsers.NamedUsers);

            while (!string.IsNullOrEmpty(allUsers.NextPage))
            {
                allUsers = GetAllNamedUsers(urbanAirshipClient, allUsers.NextPage.Replace("https://go.urbanairship.com", ""));
                tempNamedUserResult.NamedUsers.AddRange(allUsers.NamedUsers);
            }

            List<TagStructureDTO> resultDTOs = null;
            _namedUsersConverter.Convert(tempNamedUserResult, out resultDTOs);

            if (resultDTOs == null || (resultDTOs.Count == 0))
                return null;

            var usersWithMatchingTag = resultDTOs
                .Where(u => u.TagGroups
                .Any(t => t.Key == tag.TagGroup.SingleOrDefault().Name))
                .Where(q => q.TagGroups
                .Any(o => o.Value
                .Any(k => k == tag.Name))).ToList();

            if (usersWithMatchingTag.Count > 0)
            {
                usersWithMatchingTag.ForEach(p =>
                {
                    dest.UidList.AddRange(p.UidList);
                });
            }

            if (dest.UidList.Count > 0)
            {
                dest.TagGroups = new Dictionary<string, IEnumerable<string>>();
                dest.TagGroups.Add(tag.TagGroup.SingleOrDefault().Name, new List<string> { tag.Name });
                string jsonstring;
                _removeTagRequestConverter.Convert(dest, out jsonstring);
                return PostTagToNamedUsers(jsonstring, urbanAirshipClient);
            }

            return new PostTagResponse { IsActionCompleted = true };
        }

        public NamedUserDeserializer GetUserById(string id, IApiClient urbanAirshipClient)
        {
            // Get a single named user by Id
            var requestObject = urbanAirshipClient.CreateRequest(() =>
            {
                var request = new RestRequest("api/named_users/", Method.GET);
                request.AddParameter("id", id);
                return request;
            });

            var apiResult = urbanAirshipClient.RunAsync<NamedUserDeserializer>(requestObject);

            if (apiResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.Debug(string.Format("Data retrieved for student {0} from API.", id));
                return apiResult.Data;
            }
            else
            {
                _logger.Error(string.Format("Error calling API api/named_users/id - {0}", apiResult.StatusCode + " " + apiResult.Content));
                return null;
            }
        }

        public NamedUsersDeserializer GetAllNamedUsers(IApiClient urbanAirshipClient, string url = null)
        {
            // Get a single named user by Id
            var requestObject = urbanAirshipClient.CreateRequest(() =>
            {
                var urlString = url ?? "api/named_users";
                var request = new RestRequest(urlString, Method.GET);
                return request;
            });

            var apiResult = urbanAirshipClient.RunAsync<NamedUsersDeserializer>(requestObject);

            if (apiResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return apiResult.Data;
            }
            else
            {
                _logger.Error(string.Format("Error calling API  api/named_users - {0}", apiResult.StatusCode + " " + apiResult.Content));
                return null;
            }
        }

        public bool NoDuplicateTagsInDestinationUser(TagStructureDTO source, TagStructureDTO dest)
        {
            // Make sure there are no duplicates
            var duplicates = dest.TagGroups
                .Where(x => x.Value
                .Where(p => source.TagGroups.SingleOrDefault().Value
                .Any(q => q == p)).ToList().Count > 0).ToList();

            if (duplicates.Count == 0)
                return true;

            return false;
        }

        public PostTagResponse PostTagToNamedUsers(string requestBody, IApiClient urbanAirshipClient)
        {
            //Call the API to add Tags
            var requestObject = urbanAirshipClient.CreateRequest(() =>
            {
                var request = new RestRequest("api/named_users/tags", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
                return request;
            });

            var apiResult = urbanAirshipClient.RunAsync<NamedUserDeserializer>(requestObject);

            var response = new PostTagResponse();

            if (apiResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.Debug(string.Format("Successfully posted tag {0} from API.", _tagName));
                response.IsSuccess = true;
                response.IsActionCompleted = true;
                response.OriginalAPIResponse = apiResult.Content;
            }
            else
            {
                _logger.Error(string.Format("Error calling API api/named_users/tags - {0}", apiResult.StatusCode + " " + apiResult.Content));
                response.IsSuccess = false;
                response.IsActionCompleted = true;
                response.OriginalAPIResponse = apiResult.Content;
            }
            return response;
        }
    }
}
