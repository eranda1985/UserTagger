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
            throw new NotImplementedException();
        }
    }
}
