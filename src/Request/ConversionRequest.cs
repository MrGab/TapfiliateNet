using System.Collections.Generic;
using Newtonsoft.Json;

namespace TapfiliateNet.Request
{
    public class ConversionRequest
    {
        [JsonProperty("visitor_id")]
        public string VisitorId { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("click")]
        public ConversionClickRequest Click { get; set; }

        [JsonProperty("commission_type")]
        public string CommissionType { get; set; }

        [JsonProperty("commissions")]
        public IList<CommissionRequest> Commissions { get; set; }

        [JsonProperty("meta_data")]
        public IDictionary<string, string> MetaData { get; set; }

        [JsonProperty("program_group")]
        public string ProgramGroup { get; set; }

    }

    public class ConversionClickRequest
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
