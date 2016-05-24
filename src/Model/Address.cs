using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Address
    {
        [JsonProperty("address")]
        public string StreetAddress { get; set; }

        [JsonProperty("postal_code")]
        public string Postcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }
    }
}