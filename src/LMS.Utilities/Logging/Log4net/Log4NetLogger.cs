using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Utilities.Logging
{
    public class Log4NetLogger : ILog
    {
        log4net.ILog _logger;
        public Log4NetLogger(log4net.ILog logger)
        {
            _logger = logger;
        }

        public void Trace(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }
    }
}
