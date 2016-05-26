using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapfiliateNet.Model.Request
{
    public class CommissionUpdateRequest
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("comment")]
        public int Comment { get; set; }
    }
}
