using NPoco;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Models
{
    [TableName("Tags")]
    public class TagModel: IEntity
    {
        [Column("Name")]
        public string TagName { get; set; }

        [Column("ToInstall")]
        public bool InstallStatus { get; set; }

        [Column("IsActivated")]
        public bool IsActivated { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("TagGroup")]
        public List<TagGroupModel> TagGroupList { get; set; }

        [Column("TagGroup")]
        public int TagGroupId { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Ignore]
        public string TagGroupName { get; set; }
    }
}
