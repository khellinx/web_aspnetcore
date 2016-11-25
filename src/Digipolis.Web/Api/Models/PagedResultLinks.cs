using Newtonsoft.Json;

namespace Digipolis.Web.Api
{
    public class PagedResultLinks
    {
        [JsonIgnore]
        internal string ControllerName { get; set; }

        [JsonIgnore]
        internal string ActionName { get; set; }

        [JsonIgnore]
        internal string RouteName { get; set; }

        [JsonIgnore]
        internal object RouteValues { get; set; }

        [JsonProperty(PropertyName = "first")]
        public Link First { get; set; }

        [JsonProperty(PropertyName = "prev")]
        public Link Previous { get; set; }

        [JsonProperty(PropertyName = "self")]
        public Link Self { get; set; }

        [JsonProperty(PropertyName = "next")]
        public Link Next { get; set; }

        [JsonProperty(PropertyName = "last")]
        public Link Last { get; set; }
    }
}