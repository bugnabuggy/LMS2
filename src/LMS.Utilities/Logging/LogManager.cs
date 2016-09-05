using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LMS.Utilities.Common;
using LMS.Utilities.Exceptions;


namespace LMS.Utilities.Logging
{
    public class LogManager :ILogManager
    {
        public IOperationResult Configure()
        {
            var result = new OperationResult() { Success = true };

            try
            {
                var fileInfo = new FileInfo("log4net.config");
                //log4net.Config.XmlConfigurator.Configure(fileInfo);
            }
            catch(Exception exp)
            {
                result.Success = false;
                result.Result = exp;
                result.Messages = exp.GetExceptionMessages();
            }

            return result;
        }

        public static ILog Logger(string name = "")
        {
            return new LogManager().GetLogger(name);
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
