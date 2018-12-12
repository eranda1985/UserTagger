using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.Interfaces;

namespace UniSA.UserTagger.Core.Factory
{
    public class FileReaderFactory : IFileReaderFactory
    {
        public FileStreamer Create()
        {
            return new FileStreamer();
        }
    }
}
