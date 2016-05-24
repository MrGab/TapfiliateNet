using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Conversion
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("click")]
        public Click Click { get; set; }

        [JsonProperty("commissions")]
        public IList<Commission> Commissions { get; set; }

        [JsonProperty("program")]
        public Program Program { get; set; }

        [JsonProperty("affiliate")]
        public Affiliate Affiliate { get; set; }

        [JsonProperty("meta_data")]
        public IDictionary<string, string> Metadata { get; set; }
    }
}
