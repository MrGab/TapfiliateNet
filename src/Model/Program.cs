using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Program
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("cookie_time")]
        public int CookieTime { get; set; }
    }
}