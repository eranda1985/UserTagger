/*using Newtonsoft.Json;
using RestSharp;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Deserializers;
using UniSA.UserTagger.Enums;
using UniSA.UserTagger.Interfaces;
using UniSA.UserTagger.Core.JsonSerializers;
using UniSA.UserTagger.PluginContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UniSA.UserTagger.Plugins
{
    public class TagUpdatePlugin : IPlugin
    {
        IPluginContext _context;
        IApiClientFactory _apiFactory;
        string[] _patterns = new string[] { "\"\",", ",\"\"", "\"\"" };

        public TagUpdatePlugin(IPluginContext context, IApiClientFactory apiFactory)
        {
            _context = context;
            _apiFactory = apiFactory;
        }

        public string Name
        {
            get
            {
                return "DefaultPlugin";
            }
        }

        public void PerfromAction()
        {
            Console.WriteLine("Plugin started execution..");

            #region // Load student data from repo.
            // Load student data from repo. 
            var concreteContext = _context as PluginContext;
            var tagStructures = concreteContext.Repository.List();
            #endregion

            Console.WriteLine("Student data loaded from model..");

            #region // Convert Students into DTOs. We can call this SourceDTO list. 
            // Convert into DTOs. We can call this SourceDTO list. 
            var sourceDTOList = new List<TagStructureDTO>();
            tagStructures.ForEach((s) =>
            {
                var temp = new TagStructureDTO();
                concreteContext.Converter.Convert(s, out temp);
                sourceDTOList.Add(temp);
            });
            #endregion

            Console.WriteLine("Finished Conversion..");
            Console.WriteLine("\r\n");

            #region Call the urban airship API for each user present in SourceDTO list and construct a list of another DTO's. 
            // Call the urban airship API for each user present in SourceDTO list and construct a list of another DTO's. 
            // We can call this as DestinationDTO list.
            // At the moment this is highly inefficient
            var destinationDTOList = new List<TagStructureDTO>();
            var urbanAirshipClient = _apiFactory.Create(ApiClientTypes.UrbanAirshipAPIClient);

            sourceDTOList.ForEach((dto) =>
            {
                Console.WriteLine("Retrieving student {0} from API.", dto.Uid);

                var requestObject = urbanAirshipClient.CreateRequest(() =>
                {
                    var request = new RestRequest("api/named_users/", Method.GET);
                    request.AddParameter("id", dto.Uid);
                    return request;
                });

                var apiResult = urbanAirshipClient.RunAsync<StudentDeserializer>(requestObject);

                if (apiResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Data retrieved for student {0} from API.", dto.Uid);

                    var resultDTO = new TagStructureDTO
                    {
                        Uid = apiResult.Data.NamedUser.NamedUserId,
                        TagGroups = new Dictionary<string, IEnumerable<string>>()
                    };

                    foreach (var i in apiResult.Data.NamedUser.Tags)
                    {
                        var list = i.Value as JsonArray;
                        resultDTO.TagGroups.Add(i.Key, list.Select(o => o.ToString()).ToList());
                    }
                    destinationDTOList.Add(resultDTO);
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error calling API {0}", apiResult.StatusCode + " " + apiResult.StatusDescription + " " + apiResult.ErrorMessage);
                    Console.ResetColor();
                }
            });
            #endregion

            Console.WriteLine("Completed data retrieval for all");
            Console.WriteLine("\r\n");

            #region Find the changes between SourceDTO and DestinationDTO i.e. which TagGroups need to be updated
            // Find the changes between SourceDTO and DestinationDTO i.e. which TagGroups need to be updated
            var tagsToModifyList = new List<TagStructureDTO>();
            var tagsToAddList = new List<TagStructureDTO>();

            if (destinationDTOList.Any())
            {
                foreach (var p in sourceDTOList)
                {
                    var destination = destinationDTOList.Where(d => d.Uid == p.Uid).SingleOrDefault();
                    if (destination == null)
                        continue;

                    var sourceKeys = p.TagGroups.Keys;
                    var destinationKeys = destination.TagGroups.Keys;

                    var common = sourceKeys.Where(e => destinationKeys.Any(a => a == e)).ToList();
                    var diff = sourceKeys.Where(e => !destinationKeys.Any(a => a == e)).ToList();

                    if (common.Any())
                    {
                        var temp = new TagStructureDTO { Uid = p.Uid, TagGroups = new Dictionary<string, IEnumerable<string>>() };
                        var list = p.TagGroups.Where(x => common.Any(y => y == x.Key));
                        foreach (var i in list)
                        {
                            temp.TagGroups.Add(i.Key, i.Value);
                        }
                        tagsToModifyList.Add(temp);
                    }

                    if (diff.Any())
                    {
                        var temp = new TagStructureDTO { Uid = p.Uid, TagGroups = new Dictionary<string, IEnumerable<string>>() };
                        var list = p.TagGroups.Where(x => diff.Any(y => y == x.Key));
                        foreach (var i in list)
                        {
                            temp.TagGroups.Add(i.Key, i.Value);
                        }
                        tagsToAddList.Add(temp);
                    }
                }
            }
            #endregion

            Console.WriteLine("Finished finding changes");

            Console.WriteLine("\r\n");
            Console.WriteLine("Started calling API for updating tags");

            #region // Call the urban airship API with the outcome of previous step. 
            // Call the urban airship API with the outcome of previous step. 
            foreach (var i in tagsToModifyList)
            {
                //Call the API to add or update Tags
                var requestObject = urbanAirshipClient.CreateRequest(() =>
                {
                    Console.WriteLine("Tag update request for student {0} from API.", i.Uid);

                    JsonStringModifyTags jsonBody = new JsonStringModifyTags
                    {
                        audience = new Audience { named_user_id = new List<string>() },
                        set = new Dictionary<string, IEnumerable<string>>()
                    };

                    jsonBody.audience.named_user_id.Add(i.Uid);
                    jsonBody.set = i.TagGroups;
                    string jsonString = JsonConvert.SerializeObject(jsonBody);

                    jsonString = CheckPatternMatching(jsonString, _patterns);

                    var request = new RestRequest("api/named_users/tags", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                    return request;
                });

                var apiResult = urbanAirshipClient.RunAsync<StudentDeserializer>(requestObject);
                if (apiResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Successfully updated tags for student {0} from API.", i.Uid);
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error calling API {0}", apiResult.StatusCode + " " + apiResult.StatusDescription + " " + apiResult.ErrorMessage);
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\r\n");
            Console.WriteLine("Completed tag update process.");

            foreach (var i in tagsToAddList)
            {
                //Call the API to add or update Tags
                var requestObject = urbanAirshipClient.CreateRequest(() =>
                {
                    Console.WriteLine("Tag add request for student {0} from API.", i.Uid);

                    JsonStringAddTags jsonBody = new JsonStringAddTags
                    {
                        audience = new Audience { named_user_id = new List<string>() },
                        add = new Dictionary<string, IEnumerable<string>>(),
                    };

                    jsonBody.audience.named_user_id.Add(i.Uid);
                    jsonBody.add = i.TagGroups;
                    string jsonString = JsonConvert.SerializeObject(jsonBody);

                    jsonString = CheckPatternMatching(jsonString, _patterns);

                    var request = new RestRequest("api/named_users/tags", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                    return request;
                });

                var apiResult = urbanAirshipClient.RunAsync<StudentDeserializer>(requestObject);
                if (apiResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Successfully added tags for student {0} from API.", i.Uid);
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error calling API {0}", apiResult.StatusCode + " " + apiResult.StatusDescription + " " + apiResult.ErrorMessage);
                    Console.ResetColor();
                }
            }
            #endregion

            Console.WriteLine("\r\n");
            Console.WriteLine("Completed tag add process.");


            // Add custom tags as found in TagFilePath

            // Update the tag registry
        }

        private string CheckPatternMatching(string input, string[] patterns)
        {
            string result = input;
            foreach (var pat in patterns)
            {
                var matches = Regex.Matches(result,pat);
                foreach (var y in matches)
                {
                    var g = y as Match;
                    result = result.Remove(g.Index, g.Length);
                }
            }

            return result;
        }
    }
}
*/