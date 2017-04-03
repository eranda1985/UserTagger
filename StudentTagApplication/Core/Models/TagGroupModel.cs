using NPoco;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.Core.Models
{
    [TableName("TagGroups")]
    public class TagGroupModel: IEntity
    {
        public string Name { get; set; }
    }
}
