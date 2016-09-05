using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Utilities.Common
{
    public interface IOperationResult
    {
        bool Success { get; set; }
        object Result { get; set; }
        IEnumerable<string> Messages { get; set; }
    }
}
