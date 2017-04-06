using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.DTO
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInstall { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        public List<TagGroupDTO> TagGroup { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
