using NSubstitute;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.Converters;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Factory;
using UniSA.UserTagger.Core.Interfaces;

using ApiLib = UniSA.UserTagger.ApiClientWorker;

namespace UniSA.UserTaggerTests.ApiClientWorker
{
    [TestFixture]
    public class ApiClientWorkerTest
    {
        private IApiClientFactory _apiClientfactory;
        private IUrbanAirshipClientWorker _worker;
        private IConverter<NamedUserDeserializer, TagStructureDTO> _namedUserConverter;
        private IConverter<NamedUsersDeserializer, List<TagStructureDTO>> _namedUsersConverter;
        private IConverter<TagStructureDTO, string> _addTagRequestConverter;
        private RemoveTagRequestConverter removeTagRequestConverter;
        private AddTagRequestConverter addTagRequestConverter;
        private TagStructureDTO existingUser;

        [SetUp]
        public void SetUp()
        {
            _apiClientfactory = new ApiClientFactory();
            _worker = new ApiLib.UrbanAirshipClientWorker(_apiClientfactory, _namedUserConverter, _namedUsersConverter, removeTagRequestConverter, addTagRequestConverter);

            existingUser = new TagStructureDTO();
            existingUser.UidList = new List<string> { "WELEY001" };
            existingUser.TagGroups.Add("Campus", new List<string> { "Mawson" });
        }

        [Test]
        public void Should_Return__Duplicates_For_Existing_Users()
        {
            // Arrange
            TagStructureDTO source = new TagStructureDTO();
            source.UidList = new List<string> { "WELEY001", "WELEY00888" };
            source.TagGroups.Add("Campus", new List<string> { "Mawson" });

            // Act
            var result = _worker.NoDuplicateTagsInDestinationUser(source, existingUser);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Should_NOT_Return__Duplicates_For_Existing_Users()
        {
            // Arrange
            TagStructureDTO source = new TagStructureDTO();
            source.UidList = new List<string> { "WELEY001", "WELEY00888" };
            source.TagGroups.Add("Campus", new List<string> { "Mawsonx" });

            // Act
            var result = _worker.NoDuplicateTagsInDestinationUser(source, existingUser);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
