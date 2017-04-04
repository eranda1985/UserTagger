using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.Core.Converters
{
    public class PostTagDecorator : PostTagRequestConverter
    {
        protected PostTagRequestConverter converter;

        public PostTagDecorator(PostTagRequestConverter converter)
        {
            this.converter = converter;
        }

        public new void Convert(TagStructureDTO source, out string dest)
        {
            converter.Convert(source, out dest);
        }
    }
}
