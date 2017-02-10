using System;
using Digipolis.Errors;

namespace Digipolis.Web.Exceptions
{
    public class ExceptionLogMessage
    {
        public Error Error { get; set; }

        public Exception Exception { get; set; }
    }
}
