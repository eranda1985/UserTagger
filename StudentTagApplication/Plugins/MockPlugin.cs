using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace UniSA.UserTagger.Plugins
{
    public class MockPlugin : IPlugin
    {
        IPluginContext _context;
        private Dictionary<string, object> _propertyBag;

        public MockPlugin(IPluginContext context)
        {
            _context = context;
            _propertyBag = new Dictionary<string, object>();
        }
        public string Name
        {
            get
            {
                return "MockPlugin";
            }
        }

        public Dictionary<string, object> PropertyBag
        {
            get
            {
                return _propertyBag;
            }

            set
            {
                _propertyBag = value;
            }
        }

        public void PerfromAction()
        {
        }
    }
}
