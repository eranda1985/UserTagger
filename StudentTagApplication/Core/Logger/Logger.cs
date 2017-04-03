using log4net;
using UniSA.UserTagger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Logger
{
    public class Logger : ILogger
    {
        private ILog _logger;
        public Logger(Type type)
        {
            _logger = LogManager.GetLogger(type);
            
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Debug(message);
        }
    }
}
