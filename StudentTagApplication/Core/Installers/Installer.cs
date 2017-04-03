using Microsoft.Practices.Unity;
using UniSA.UserTagger.ApiClientWorker;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Converters;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Factory;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using UniSA.UserTagger.Core.Repository;
using UniSA.UserTagger.PluginContexts;
using UniSA.UserTagger.Plugins;

namespace UniSA.UserTagger.Core.Installers
{
    public class Installer
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container
                // Register repositories 
                .RegisterType<IRepository<TagModel>, TagRepository>(new TransientLifetimeManager())

                // Register converters 
                .RegisterType<IConverter<TagModel, TagDTO>, TagModelConverter>(new TransientLifetimeManager())
                .RegisterType<IConverter<NamedUserDeserializer, TagStructureDTO>, NamedUserResponseConverter>(new TransientLifetimeManager())
                .RegisterType<IConverter<TagStructureDTO, string>, AddTagRequestConverter>(new TransientLifetimeManager())
                .RegisterType<IConverter<TagDTO, TagModel>, TagDTOConverter>(new TransientLifetimeManager())

                // Register plugin contexts
                .RegisterType<IPluginContext, PluginContext>(new ContainerControlledLifetimeManager())

                // Register factories 
                .RegisterType<IApiClientFactory, ApiClientFactory>(new ContainerControlledLifetimeManager())

                .RegisterType<IEventAggregatorBase, EventAggregator>(new ContainerControlledLifetimeManager())

                .RegisterType<IUrbanAirshipClientWorker, UrbanAirshipClientWorker>(new TransientLifetimeManager())

                // Register plugins here
                .RegisterType<DispatcherPlugin>(new TransientLifetimeManager())
                .RegisterType<NotifierPlugin>(new TransientLifetimeManager())
                .RegisterType<MockPlugin>(new TransientLifetimeManager());
        }

    }
}
