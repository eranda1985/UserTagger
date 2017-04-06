using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Validators;

namespace UniSA.UserTagger.Core.Converters
{
    public class TagModelConverter : IConverter<TagModel, TagDTO>
    {
        public void Convert(TagModel source, out TagDTO dest)
        {
            UserTaggerValidator.CheckNull(source.TagGroupList, "TagGroupsList");

            dest = new TagDTO
            {
                Id = source.Id,
                IsInstall = source.InstallStatus,
                IsActivated = source.IsActivated,
                IsDeleted = source.IsDeleted,
                Name = source.TagName,
                ModifiedDate = source.ModifiedDate,
                CreatedDate = source.CreatedDate,
                TagGroup = source.TagGroupList.Select(s => new TagGroupDTO { Id = s.Id, Name = s.Name}).ToList()
            };
        }
    }
}
