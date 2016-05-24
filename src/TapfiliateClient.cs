using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TapfiliateNet.Model;

//.NET 4.5+ only

namespace TapfiliateNet
{
    public sealed class TapfiliateClient
    {
        private const string BaseUrl = "https://tapfiliate.com/api/1.4/";
        private int TimeoutSeconds = 0;
        private readonly string ApiKey;

        private JsonSerializerSettings SerializerSettings
        {
            get
            {
                var serializer = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                };
                serializer.Converters.Add(new StringEnumConverter { CamelCaseText = true });

                return serializer;
            }
        }

        private HttpClient HttpClient
        {
            get
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Api-Key", ApiKey);

                if (TimeoutSeconds != 0)
                {
                    client.Timeout = TimeSpan.FromSeconds(TimeoutSeconds);
                }

                return client;
            } 
        }

        public TapfiliateClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        public TapfiliateClient(string apiKey, int timeoutSeconds)
        {
            ApiKey = apiKey;
            TimeoutSeconds = timeoutSeconds;
        }

        #region Affiliates

        public Affiliate GetAffiliate(string affiliateId)
        {
            var url = $"{BaseUrl}/affiliates/{affiliateId}/";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Affiliate>(response);
        }

        public IList<Affiliate> GetAffiliateList(string clickId, string sourceId)
        {
            var url = $"{BaseUrl}/affiliates/?click_id={clickId}&source_id={sourceId}";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Affiliate>>(response);
        }

        public Affiliate CreateAffiliate(Affiliate affiliate)
        {
            var url = $"{BaseUrl}/affiliates/";

            var payLoad = JsonConvert.SerializeObject(affiliate);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<Affiliate>(response);
        }

        public IDictionary<string, string> GetAffiliateMetadata(string affiliateId, string key)
        {
            var url = $"{BaseUrl}/affiliates/{affiliateId}/meta-data/{key}";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IDictionary<string, string>>(response);
        }

        public bool PostAffiliateMetadata(string affiliateId, IDictionary<string, string> metadata)
        {
            var url = $"{BaseUrl}/affiliates/{affiliateId}/meta-data/";

            var payLoad = JsonConvert.SerializeObject(metadata);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool SetAffiliateMetadata(string affiliateId, string key, string value)
        {
            var url = $"{BaseUrl}/affiliates/{affiliateId}/meta-data/{key}/";

            var payLoad = JsonConvert.SerializeObject(new { value = value });

            var response = HttpClient.PutAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool DeleteAffiliateMetadata(string affiliateId, string key)
        {
            var url = $"{BaseUrl}/affiliates/{affiliateId}/meta-data/{key}";

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        #endregion

        private T GetResponse<T>(HttpResponseMessage response)
        {
            var body = response.Content.ReadAsStringAsync().Result;

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(body);
            }

            throw new Exception(response.StatusCode + " - " + body);
        }

    }
}
