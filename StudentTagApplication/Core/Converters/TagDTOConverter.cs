using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using UniSA.UserTagger.Core.Validators;

using AppLogger = UniSA.UserTagger.Core.Logger;

namespace UniSA.UserTagger.Core.Converters
{
    public class TagDTOConverter : IConverter<TagDTO, TagModel>
    {
        ILogger _logger;

        public TagDTOConverter()
        {
            _logger = new AppLogger.Logger(GetType());
        }

        public void Convert(TagDTO source, out TagModel dest)
        {
            // Add validation logic here for null checking 
            UserTaggerValidator.CheckNull(source.TagGroup, "TagGroups");

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
