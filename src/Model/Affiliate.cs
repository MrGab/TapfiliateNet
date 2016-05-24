using System.Collections.Generic;
using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Affiliate
    {
        [JsonProperty("affiliate_id")]
        public string AffiliateId { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }

        [JsonProperty("meta_data")]
        public IDictionary<string, string> Metadata { get; set; }
    }
}