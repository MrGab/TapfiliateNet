using System.Collections.Generic;
using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public abstract class AffiliateBase
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("company")]
        public AffiliateCompany Company { get; set; }

        [JsonProperty("meta_data")]
        public IDictionary<string, string> Metadata { get; set; }
    }

    public class Affiliate : AffiliateBase
    {
        [JsonProperty("affiliate_id")]
        public string AffiliateId { get; set; }
    }

    public class ProgramAffiliate : AffiliateBase
    {
        [JsonProperty("referral_link")]
        public string ReferralLink { get; set; }
    }

    public class AffiliateCompany
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public AffiliateCompanyAddress Address { get; set; }
    }

    public class AffiliateCompanyAddress
    {
        [JsonProperty("address")]
        public string StreetAddress { get; set; }

        [JsonProperty("postal_code")]
        public string Postcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public AffiliateCompanyAddressCountry Country { get; set; }
    }

    public class AffiliateCompanyAddressCountry
    {
        [JsonProperty("code")]
        public string Name { get; set; }
    }
}