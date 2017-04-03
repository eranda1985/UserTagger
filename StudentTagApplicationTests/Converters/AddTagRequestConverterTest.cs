using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Converters;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;

namespace StudentTagApplicationTests.Converters
{
    [TestFixture]
    public class AddTagRequestConverterTest
    {
        private IConverter<TagStructureDTO, string> _addTagRequestConverter;

        [SetUp]
        public void SetUp()
        {
            _addTagRequestConverter = new AddTagRequestConverter();
        }

        [Test]
        public void Should_Convert_Add_Tag_Request_Successfully()
        {
            // Arrange
            TagStructureDTO tagRequest = new TagStructureDTO
            {
                TagGroups = new Dictionary<string, IEnumerable<string>>(),
                UidList = new List<string> { "userId1", "userId2" }
            };
            tagRequest.TagGroups.Add("groupName", new List<string> { "tagName1"});
            string expected = "{\"audience\":{\"named_user_id\":[\"userId1\",\"userId2\"]},\"add\":{\"groupName\":[\"tagName1\"]}}";
            string actual;

            // Act
            _addTagRequestConverter.Convert(tagRequest, out actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
