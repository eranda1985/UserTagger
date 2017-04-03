using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
    }
}
