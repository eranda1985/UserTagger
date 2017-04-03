using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.Core.Models
{
    [TableName("ScholarshipUserDetail")]
    public class ScholarshipUserModel: IEntity
    {
        [Column("UId")]
        public string UserId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }
    }
}
