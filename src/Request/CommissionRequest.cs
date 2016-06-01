using Newtonsoft.Json;

namespace TapfiliateNet.Request
{
    public class CommissionRequest
    {
        [JsonProperty("sub_amount")]
        public double SubAmount { get; set; }

        [JsonProperty("commission_type")]
        public string CommissionType { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }
    }
}
