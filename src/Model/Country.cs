using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Country
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}