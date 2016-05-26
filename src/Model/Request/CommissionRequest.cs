using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapfiliateNet.Model.Request
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
