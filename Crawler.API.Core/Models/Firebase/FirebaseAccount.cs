using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.API.Core.Models.Firebase
{
    public class FirebaseAccount: BaseFirebaseModel
    {
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("openProjectAPIKey")]
        public string OpenProjectAPIKey { get; set; }
    }
}
