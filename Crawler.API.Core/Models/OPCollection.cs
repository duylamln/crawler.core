using Newtonsoft.Json;
using System.Collections.Generic;

namespace Crawler.API.Core.Models
{
    public class OPCollection<T> where T : Element
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("_embedded")]
        public Embedded<T> Embedded { get; set; }
    }

    public partial class Embedded<T> where T : Element
    {
        [JsonProperty("elements")]
        public List<T> Elements { get; set; }
    }
}
