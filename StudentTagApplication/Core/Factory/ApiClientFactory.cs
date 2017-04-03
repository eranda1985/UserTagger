using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Enums;
using UniSA.UserTagger.Core.Components;

namespace UniSA.UserTagger.Core.Factory
{
    public class ApiClientFactory : IApiClientFactory
    {
        public IApiClient Create(ApiClientTypes type)
        {
            switch (type)
            {
                case ApiClientTypes.UrbanAirshipAPIClient:
                    return new UrbanAirshipAPIClient();
            }
            return null;
        }
    }
}
