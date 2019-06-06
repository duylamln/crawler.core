using Newtonsoft.Json;

namespace Crawler.API.Core.Models.Firebase
{
    public class BaseFirebaseModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

}
