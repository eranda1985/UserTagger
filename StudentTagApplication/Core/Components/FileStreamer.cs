using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Components
{
    public class FileStreamer
    {
        public string ReadAll(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            using (var fs = File.OpenRead(path))
            {
                using (var reader = new StreamReader(fs, Encoding.UTF8))
                {
                    fs.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
