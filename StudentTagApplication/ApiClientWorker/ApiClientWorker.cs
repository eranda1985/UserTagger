using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;

namespace UniSA.UserTagger.ApiClientWorker
{
    public class UrbanAirshipClientWorker: IUrbanAirshipClientWorker
    {
        private IApiClientFactory _apiClientFactory;
        private IConverter<NamedUserDeserializer, TagStructureDTO> _namedUserConverter;
        private IConverter<TagStructureDTO, string> _addTagRequestConverter;
        private ILogger _logger;
        private string _tagName;

        public UrbanAirshipClientWorker(
            IApiClientFactory apiClientfactory, 
            IConverter<NamedUserDeserializer, TagStructureDTO> namedUserConverter,
            IConverter<TagStructureDTO, string> addTagRequestConverter)
        {
            _apiClientFactory = apiClientfactory;
            _namedUserConverter = namedUserConverter;
            _addTagRequestConverter = addTagRequestConverter;
            _logger = new Logger(GetType());
        }

        public PostTagResponse ProcessAll(TagStructureDTO source)
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
                _addTagRequestConverter.Convert(dest, out jsonstring);
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
                _logger.Error(string.Format("Error calling API {0}", apiResult.StatusCode + " " + apiResult.Content));
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
                _logger.Error(string.Format("Error calling API {0}", apiResult.StatusCode + " " + apiResult.Content));
                response.IsSuccess = false;
                response.IsActionCompleted = true;
                response.OriginalAPIResponse = apiResult.Content;
            }
            return response;
        }
    }
}
