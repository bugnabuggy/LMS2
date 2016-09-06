using LMS.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Utilities.Logging
{
    public interface ILogManager
    {
        IOperationResult Configure(IEnumerable<KeyValuePair<string,string>> config = null);
        ILog GetLogger(string name = "");
    }
}
