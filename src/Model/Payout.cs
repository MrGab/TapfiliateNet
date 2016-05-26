using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapfiliateNet.Model
{
    public class Payout
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("affiliate")]
        public PayoutAffiliate Affiliate { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }
    }

    public class PayoutAffiliate
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("payout_methods")]
        public IList<PayoutAffiliatePayoutMethod> PayoutMethods { get; set; }
    }

    public class PayoutAffiliatePayoutMethod
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("details")]
        public PayoutAffiliatePayoutMethodsDetail Details;

        [JsonProperty("primary")]
        public bool Primary { get; set; }
    }

    public abstract class PayoutAffiliatePayoutMethodsDetail {}

    public class PayoutAffiliatePayoutMethodsDetailsPaypal : PayoutAffiliatePayoutMethodsDetail
    {
        [JsonProperty("paypal_address")]
        public string PaypalAddress { get; set; }
    }
}
