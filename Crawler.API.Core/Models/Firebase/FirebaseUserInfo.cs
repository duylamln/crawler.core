using Newtonsoft.Json;

namespace Crawler.API.Core.Models.Firebase
{
    public class FirebaseUserInfo: BaseFirebaseModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("uid")]
        public string Uid { get; set; }
        [JsonProperty("openProjectAPIKey")]
        public string OpenProjectAPIKey { get; set; }
    }
}
