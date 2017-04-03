using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Converters
{
    public class TagModelConverter : IConverter<TagModel, TagDTO>
    {
        public void Convert(TagModel source, out TagDTO dest)
        {
            dest = new TagDTO
            {
                Id = source.Id,
                IsInstall = source.InstallStatus,
                IsNew = source.IsNew,
                Name = source.TagName,
                TagGroup = source.TagGroupList.Select(s => new TagGroupDTO { Id = s.Id, Name = s.Name}).ToList()
            };
        }
    }
}
