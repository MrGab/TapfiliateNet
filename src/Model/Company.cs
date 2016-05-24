using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Company
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}