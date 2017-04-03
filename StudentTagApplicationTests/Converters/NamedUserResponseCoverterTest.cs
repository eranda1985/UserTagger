using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Deserializers;
using UniSA.UserTagger.Core.Converters;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;

namespace StudentTagApplicationTests.Converters
{
    [TestFixture]
    public class NamedUserResponseCoverterTest
    {
        private IConverter<NamedUserDeserializer, TagStructureDTO> _namedUserConverter;

        [SetUp]
        public void SetUp()
        {
            _namedUserConverter = new NamedUserResponseConverter();
        }

        [Test]
        public void Should_Convert_named_User_Response_Successfully()
        {
            // Arrange
            NamedUserDeserializer apiResponse = new NamedUserDeserializer
            {
                NamedUser = new NamedUser
                {
                    NamedUserId = "userId",
                    Tags = new Dictionary<string, object>()
                }
            };
            apiResponse.NamedUser.Tags.Add("groupName", new JsonArray { "tagName1", "tagName2" });

            TagStructureDTO expected = new TagStructureDTO
            {
                TagGroups = new Dictionary<string, IEnumerable<string>>(),
                UidList = new List<string> { "userId" }
            };
            expected.TagGroups.Add("groupName", new List<string> { "tagName1", "tagName2" });

            // Act
            TagStructureDTO actual;
            _namedUserConverter.Convert(apiResponse, out actual);

            // Assert
            Assert.AreEqual(expected.UidList.Count, actual.UidList.Count);
            Assert.AreEqual(expected.UidList, actual.UidList);
            Assert.AreEqual(expected.TagGroups, actual.TagGroups);
        }
    }
}
