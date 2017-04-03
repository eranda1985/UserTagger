using UniSA.UserTagger.Core.Enums;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IApiClientFactory
    {
        IApiClient Create(ApiClientTypes type);
    }
}
