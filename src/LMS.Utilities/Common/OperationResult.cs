using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Utilities.Common
{
    public class OperationResult :IOperationResult
    {
        public bool Success { get; set; }
        public object Result { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}
