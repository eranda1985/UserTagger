using System.Collections.Generic;
using Newtonsoft.Json;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.JsonSerializers;

namespace UniSA.UserTagger.Core.Converters
{
    public class AddTagRequestConverter : PostTagDecorator
    {
        public AddTagRequestConverter(PostTagRequestConverter converter) : base(converter)
        {
            
        }

        public new void Convert(TagStructureDTO source, out string dest)
        {
            converter.ConvertForAddTag(source, out dest);
        }
    }
}
