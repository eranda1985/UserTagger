using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IApiClient
    {
        string UriPath { get; set; }
        IRestResponse<T> RunAsync<T>(IRestRequest request) where T: new();

        IRestRequest CreateRequest(Func<IRestRequest> t);
    }
}
