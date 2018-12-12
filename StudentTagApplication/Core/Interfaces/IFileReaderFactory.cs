using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Components;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IFileReaderFactory
    {
        FileStreamer Create();
    }
}
