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
    public class TagDTOConverterTest
    {
        private IConverter<TagDTO, TagModel> _tagDTOConverter;

        [SetUp]
        public void SetUp()
        {
            _tagDTOConverter = new TagDTOConverter();
        }

        [Test]
        public void Should_Convert_DTO_Into_Model_Successfully()
        {
            // Arrange
            TagDTO dto = new TagDTO
            {
                Id = 1,
                Name = "testTagName",
                IsInstall = true,
                IsNew = false,
                CreatedDate = DateTime.Now.AddDays(-1),
                ModifiedDate = DateTime.Now,
                TagGroup = new List<TagGroupDTO> { new TagGroupDTO { Id = 11, Name = "TagGroup" } }
            };
            TagModel model = new TagModel();

            // Act 
            _tagDTOConverter.Convert(dto, out model);

            // Assert
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.TagName);
            Assert.AreEqual(dto.IsInstall, model.InstallStatus);
            Assert.AreEqual(dto.IsNew, model.IsNew);
            Assert.AreEqual(dto.ModifiedDate, model.ModifiedDate);
            Assert.AreEqual(dto.CreatedDate, model.CreatedDate);
            Assert.AreEqual(dto.TagGroup.Select(s => s.Name), model.TagGroupList.Select(s => s.Name));
            Assert.AreEqual(dto.TagGroup.SingleOrDefault().Id, model.TagGroupId);
            Assert.AreEqual(dto.TagGroup.SingleOrDefault().Name, model.TagGroupName);
        }
    }
}
