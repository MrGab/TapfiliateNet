using Newtonsoft.Json;

namespace TapfiliateNet.Request
{
    public class CommissionRequest
    {
        [JsonProperty("id")]
        public int Id { get; set;}
        [JsonProperty("sub_amount")]
        public double SubAmount { get; set; }
        [JsonProperty("commission_type")]
        public double CommissionType { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
