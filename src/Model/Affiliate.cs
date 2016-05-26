using System.Collections.Generic;
using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Affiliate : AffiliateMaster
    {
        [JsonProperty("affiliate_id")]
        public string AffiliateId { get; set; }
    }
}