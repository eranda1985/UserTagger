using System.Collections.Generic;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }
        Dictionary<string, object> PropertyBag { get; set; }
        void PerfromAction();
    }
}
