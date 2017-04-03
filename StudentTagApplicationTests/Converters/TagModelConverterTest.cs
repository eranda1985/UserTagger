using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Converters;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;

namespace StudentTagApplicationTests.Converters
{
    [TestFixture]
    public class TagModelConverterTest
    {
        private IConverter<TagModel, TagDTO> _tagModelConverter;

        [SetUp]
        public void SetUp()
        {
            _tagModelConverter = new TagModelConverter();
        }

        [Test]
        public void Should_Convert_Model_Into_DTO_Successfully()
        {
            // Arrange
            TagModel model = new TagModel
            {
                Id = 1,
                InstallStatus = true,
                IsNew = true,
                TagName = "TestTagName",
                TagGroupList = new List<TagGroupModel> { new TagGroupModel { Id = 1, Name = "tagGroup1" } },
                CreatedDate = DateTime.Now.AddDays(-1),
                ModifiedDate = DateTime.Now
            };
            TagDTO dto = new TagDTO();

            // Act
            _tagModelConverter.Convert(model, out dto);

            // Assert
            Assert.AreEqual(model.Id, dto.Id);
            Assert.AreEqual(model.InstallStatus, dto.IsInstall);
            Assert.AreEqual(model.IsNew, dto.IsNew);
            Assert.AreEqual(model.ModifiedDate, dto.ModifiedDate);
            Assert.AreEqual(model.TagGroupList.SingleOrDefault().Name, dto.TagGroup.SingleOrDefault().Name);
            Assert.AreEqual(model.TagGroupList.SingleOrDefault().Id, dto.TagGroup.SingleOrDefault().Id);
        }

    }
}
