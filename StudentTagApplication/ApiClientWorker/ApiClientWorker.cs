using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using System;
using System.Collections.Generic;
using UniSA.UserTagger.Core.Converters;

namespace UniSA.UserTagger.ApiClientWorker
{
    public class UrbanAirshipClientWorker: IUrbanAirshipClientWorker
    {
        private IApiClientFactory _apiClientFactory;
        private IConverter<NamedUserDeserializer, TagStructureDTO> _namedUserConverter;
        private IConverter<NamedUsersDeserializer, List<TagStructureDTO>> _namedUsersConverter;
        private IConverter<TagStructureDTO, string> _postTagRequestConverter;
        private ILogger _logger;
        private string _tagName;

        public UrbanAirshipClientWorker(
            IApiClientFactory apiClientfactory, 
            IConverter<NamedUserDeserializer, TagStructureDTO> namedUserConverter,
            IConverter<NamedUsersDeserializer, List<TagStructureDTO>> namedUsersConverter,
            IConverter<TagStructureDTO, string> postTagRequestConverter)
        {
            _apiClientFactory = apiClientfactory;
            _namedUserConverter = namedUserConverter;
            _namedUsersConverter = namedUsersConverter;
            _postTagRequestConverter = postTagRequestConverter;
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
                var destinationUser = GetUserById(item, urbanAirshipClient);

                _namedUserConverter.Convert(destinationUser, out resultDTO);

                if (resultDTO == null || (resultDTO.UidList.Count == 0))
                    continue;

                if (NoDuplicateTagsInDestinationUser(source, resultDTO))
                {
                    dest.UidList.Add(resultDTO.UidList.SingleOrDefault());
                }
            }

            if (dest.UidList.Count > 0)
            {
                dest.TagGroups = source.TagGroups;

                string jsonstring;

                var addtagConverter = new AddTagRequestConverter((PostTagRequestConverter)_postTagRequestConverter);

                addtagConverter.Convert(dest, out jsonstring);

                return PostTagToNamedUsers(jsonstring, urbanAirshipClient);
            }

            return new PostTagResponse { IsActionCompleted = true};
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
                _logger.Debug(string.Format("Successfully added tag {0} from API.", _tagName));
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

        public PostTagResponse ProcessTagRemove(TagDTO tag)
        {
            TagStructureDTO dest = new TagStructureDTO();
            var urbanAirshipClient = _apiClientFactory.Create(Core.Enums.ApiClientTypes.UrbanAirshipAPIClient);
            var allUsers = GetAllNamedUsers(urbanAirshipClient);

            List<TagStructureDTO> resultDTOs = null;
            _namedUsersConverter.Convert(allUsers, out resultDTOs);

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
                var removeTagConverter = _postTagRequestConverter as RemoveTagRequestConverter;
                removeTagConverter.Convert(dest, out jsonstring);
                return PostTagToNamedUsers(jsonstring, urbanAirshipClient);
            }

            return new PostTagResponse { IsActionCompleted = true };
        }

        public NamedUsersDeserializer GetAllNamedUsers(IApiClient urbanAirshipClient)
        {
            // Get a single named user by Id
            var requestObject = urbanAirshipClient.CreateRequest(() =>
            {
                var request = new RestRequest("api/named_users", Method.GET);
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
    }
}
