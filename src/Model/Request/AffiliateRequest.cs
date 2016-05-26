using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapfiliateNet.Model.Request
{
    class AffiliateRequest
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
        public AffiliateRequestCompany Company { get; set; }
    }
    public class AffiliateRequestCompany
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public AffiliateRequestCompanyAddress Address { get; set; }
    }
    public class AffiliateRequestCompanyAddress
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
        public AffiliateRequestCompanyAddressCountry Country { get; set; }
    }
    public class AffiliateRequestCompanyAddressCountry
    {
        [JsonProperty("code")]
        public string Name { get; set; }
    }
}
