using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.Core.Models
{
    public class GenericUserModel: IEntity
    {
        [Column("Username")]
        public string Username { get; set; }
    }
}
