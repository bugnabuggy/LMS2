using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Utilities.Logging.Log4net;
using System.Net;

namespace LMS.Utilities.Logging.Log4net
{
    public class HardcodedLog4netConfigurator
    {
        private IEnumerable<KeyValuePair<string, string>> _config;

        private string GetValue(string key)
        {
            return _config.Where(x => x.Key.Equals(key))
                                .Select(x => x.Value)
                                .FirstOrDefault();
        }

        public void Configure(IEnumerable<KeyValuePair<string, string>> config = null)
        {
            _config = config ?? new Dictionary<string, string>();

            var layoutPattern = GetValue("LayoutPattern") ?? "%level - %message%newline";
            var identityPattern = GetValue("IdentityPattern") ?? "%date{yyyy-MM-ddTHH:mm:ss.ffffffzzz} %P{log4net:HostName} %logger";
            var remoteServer = GetValue("RemoteServer") ?? "logs4.papertrailapp.com";
            var remotePort = GetValue("RemotePort") ?? "0000";
            var logFile = GetValue("LogFile") ?? "log.txt";


            var layout = new Fr8PatternLayout()
            {
                ConversionPattern = layoutPattern
            };

            var identity = new log4net.Layout.PatternLayout()
            {
                ConversionPattern = identityPattern
            };

            var fileLayout = new log4net.Layout.PatternLayout()
            {
                ConversionPattern = identityPattern + " : " + layoutPattern
            };

            var appender = new Fr8RemoteSyslogAppender() {
                Layout = layout,
                //Identity = identity,
                RemoteAddress = remoteServer,
                RemotePort = int.Parse(remotePort)
            };

            

            var fap = new log4net.Appender.FileAppender(fileLayout, logFile);

            log4net.Config.BasicConfigurator.Configure(appender, fap);
        }
    }
}
