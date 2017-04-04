using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.JsonSerializers;

namespace UniSA.UserTagger.Core.Converters
{
    public class RemoveTagRequestConverter: PostTagDecorator
    {
        public RemoveTagRequestConverter(PostTagRequestConverter converter) : base(converter)
        {

        }

        public new void Convert(TagStructureDTO source, out string dest)
        {
            converter.ConvertForRemoveTag(source, out dest);
        }
    }   
}
