using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using System;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.ApiClientWorker.Interfaces;

namespace UniSA.UserTagger.PluginContexts
{
    public class PluginContext : IPluginContext
    {
        IConverter<TagModel, TagDTO> _tagModelConverter;
        IConverter<TagDTO, TagModel> _tagDTOConverter;
        IConverter<NamedUserDeserializer, TagStructureDTO> _namedUserConverter;
        IEventAggregatorBase _eventAggregator;
        IUrbanAirshipClientWorker _apiClientWorker;

        public PluginContext(
            IConverter<TagModel, TagDTO> tagModelConverter,
            IConverter<TagDTO, TagModel> tagDTOConverter,
            IConverter<NamedUserDeserializer, TagStructureDTO> namedUserConverter,
            IEventAggregatorBase eventAggregator,
            IUrbanAirshipClientWorker worker)
        {
            _tagModelConverter = tagModelConverter;
            _tagDTOConverter = tagDTOConverter;
            _namedUserConverter = namedUserConverter;
            _eventAggregator = eventAggregator;
            _apiClientWorker = worker;
        }

        public string TagFilePath
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEventAggregatorBase EventAggregator
        {
            get
            {
                return _eventAggregator;
            }

            set
            {
                _eventAggregator = value;
            }
        }

        public IConverter<TagModel, TagDTO> TagModelConverter
        {
            get
            {
                return _tagModelConverter;
            }

            set
            {
                _tagModelConverter = value;
            }
        }

        public IConverter<NamedUserDeserializer, TagStructureDTO> NamedUserConverter
        {
            get
            {
                return _namedUserConverter;
            }

            set
            {
                _namedUserConverter = value;
            }
        }

        public IUrbanAirshipClientWorker ApiClientWorker
        {
            get
            {
                return _apiClientWorker;
            }

            set
            {
                _apiClientWorker = value;
            }
        }

        public IConverter<TagDTO, TagModel> TagDTOConverter
        {
            get
            {
                return _tagDTOConverter;
            }

            set
            {
                _tagDTOConverter = value;
            }
        }
    }
}
