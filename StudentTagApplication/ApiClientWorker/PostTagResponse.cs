using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.ApiClientWorker
{
    public class PostTagResponse
    {
        public bool IsActionCompleted { get; set; }
        public bool IsSuccess { get; set; }
        public string OriginalAPIResponse { get; set; }
    }
}
