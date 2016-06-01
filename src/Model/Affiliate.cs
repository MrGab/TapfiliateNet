using System.Collections.Generic;
using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Affiliate : AffiliateBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}