using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ProducesDefaultResponsesAttribute : Attribute
    {
        readonly int _successStatusCode;

        public ProducesDefaultResponsesAttribute(int successStatusCode)
        {
            _successStatusCode = successStatusCode;
        }

        public int SuccessStatusCode
        {
            get { return _successStatusCode; }
        }

        public Type SuccessResponseType { get; set; }
    }
}
