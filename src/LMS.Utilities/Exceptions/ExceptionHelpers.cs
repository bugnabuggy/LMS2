using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Utilities.Exceptions
{
    public static class ExceptionHelpers
    {
        public static List<string> GetExceptionMessages(this Exception exception)
        {
            var exp = exception;
            var list = new List<string>() { exp.Message };

            while (exp.InnerException != null)
            {
                exp = exp.InnerException;
                list.Add(exp.Message);
            }

            return list;
        }

    }
}
