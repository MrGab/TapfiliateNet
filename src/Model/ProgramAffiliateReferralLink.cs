using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapfiliateNet.Model
{
    class ProgramAffiliateReferralLink
    {
        [JsonProperty("referral_link")]
        public string Link { get; set; }
    }
}
