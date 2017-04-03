using RestSharp;
using RestSharp.Authenticators;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace UniSA.UserTagger.Core.Components
{
    public class UrbanAirshipAPIClient : IApiClient
    {
        public string Name { get { return "UrbanAirshipAPIClient"; } }
        private RestClient _client;
        private string path;
        private ILogger _logger;

        public string UriPath
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        public UrbanAirshipAPIClient()
        {
            if (_client == null)
            {
                // TODO: Going ahead the following hard coded stuff needs to be taken out. 
                _client = new RestClient("https://go.urbanairship.com");
                _client.Authenticator = new HttpBasicAuthenticator("bsVQR0IGS9SErIHeAddL4A", "kFq55DFIQVuc82shxv8xgQ");
            }
            _logger = new Logger.Logger(GetType());
        }

        public IRestRequest CreateRequest(Func<IRestRequest> t)
        {
            return t();
        }

        // TODO: Try to make this async 
        Task<T> IApiClient.RunAsync<T>(IRestRequest request)
        {
            if (request == null)
                throw new Exception("Uri request cannot be empty");

            var taskCompletionSource = new TaskCompletionSource<T>();

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/vnd.urbanairship+json; version=3");

            var t = _client.ExecuteAsync<T>(request, (response) => 
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.Debug(string.Format("Data retrieved for student {0} from API."));
                    taskCompletionSource.SetResult(response.Data);
                }
                else
                {
                    _logger.Error(string.Format("Error calling API {0}", response.StatusCode + " " + response.Content));
                }
                
            });

            //t.Wait();

            return taskCompletionSource.Task;
        }
    }
}
