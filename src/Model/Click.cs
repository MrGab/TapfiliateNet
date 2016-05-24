using System;
using Newtonsoft.Json;

namespace TapfiliateNet.Model
{
    public class Click
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}