using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.API.Core.Models
{
    public class OpCreateTimeEntryResponse
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("comment")]
        public OpCreateTimeEntryResponseComment Comment { get; set; }

        [JsonProperty("spentOn")]
        public DateTimeOffset SpentOn { get; set; }

        [JsonProperty("hours")]
        public string Hours { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

    public class OpCreateTimeEntryResponseComment
    {
        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self")]
        public Self Self { get; set; }

        [JsonProperty("updateImmediately")]
        public Delete UpdateImmediately { get; set; }

        [JsonProperty("delete")]
        public Delete Delete { get; set; }

        [JsonProperty("project")]
        public Activity Project { get; set; }

        [JsonProperty("workPackage")]
        public Activity WorkPackage { get; set; }

        [JsonProperty("user")]
        public Activity User { get; set; }

        [JsonProperty("activity")]
        public Activity Activity { get; set; }
    }

    public partial class Activity
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public partial class Delete
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }

    public partial class Self
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }
}
