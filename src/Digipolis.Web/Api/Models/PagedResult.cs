using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Digipolis.Web.Api
{
    public class PagedResult<T>
    {
        [JsonProperty(PropertyName = "_links")]
        public PagedResultLinks Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public PagedResultEmbedded<T> Embedded { get; set; }

        [JsonProperty(PropertyName = "_page")]
        public Page Page { get; set; }

        public PagedResult(int page, int pageSize, int totalElements, IEnumerable<T> data)
        {
            Embedded = new PagedResultEmbedded<T>()
            {
                Data = data ?? new List<T>()
            };
            Page = new Page
            {
                Number = page,
                Size = data.Count(),
                TotalElements = totalElements,
                TotalPages = (int)Math.Ceiling((double)totalElements / (double)pageSize)
            };
            Links = new PagedResultLinks();
        }
    }
}
