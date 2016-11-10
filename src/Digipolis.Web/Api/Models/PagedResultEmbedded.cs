using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api
{
    public class PagedResultEmbedded<T>
    {
        [JsonProperty(PropertyName = "resourceList")]
        public IEnumerable<T> Data { get; set; }
    }
}
