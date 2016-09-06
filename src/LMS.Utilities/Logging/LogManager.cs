using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LMS.Utilities.Common;
using LMS.Utilities.Exceptions;
using LMS.Utilities.Logging.Log4net;

namespace LMS.Utilities.Logging
{
    public class LogManager :ILogManager
    {
        public IOperationResult Configure(IEnumerable<KeyValuePair<string, string>> config = null)
        {
            var result = new OperationResult() { Success = true };

            try
            {
                new HardcodedLog4netConfigurator().Configure(config);
            }
            catch(Exception exp)
            {
                result.Success = false;
                result.Result = exp;
                result.Messages = exp.GetExceptionMessages();
            }

            return result;
        }

        public ILog GetLogger(string name = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            }
            var log4netLogger = log4net.LogManager.GetLogger(name);

            var logger = new Log4NetLogger(log4netLogger);
            return logger; 
        }

      
    }
}
