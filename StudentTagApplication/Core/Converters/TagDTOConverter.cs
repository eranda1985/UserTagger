using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;

namespace UniSA.UserTagger.Core.Converters
{
    public class TagDTOConverter : IConverter<TagDTO, TagModel>
    {
        public void Convert(TagDTO source, out TagModel dest)
        {
            dest = new TagModel
            {
                Id = source.Id,
                InstallStatus = source.IsInstall,
                IsNew = source.IsNew,
                TagName = source.Name,
                ModifiedDate = source.ModifiedDate,
                CreatedDate = source.CreatedDate,
                TagGroupId = source.TagGroup.SingleOrDefault().Id,
                TagGroupName = source.TagGroup.SingleOrDefault().Name,
                TagGroupList = source.TagGroup.Select(s => new TagGroupModel { Id = s.Id, Name = s.Name }).ToList()
            };
        }
    }
}
