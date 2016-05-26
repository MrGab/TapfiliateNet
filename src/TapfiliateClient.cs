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
using TapfiliateNet.Model.Request;

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

        #region Affiliate

        public Affiliate GetAffiliate(string affiliateId)
        {
            var url = BaseUrl + "/affiliates/" + affiliateId + "/";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Affiliate>(response);
        }
        public IList<Affiliate> GetAllAffiliates()
        {
            return GetAffiliateList(null,null);
        }
        public IList<Affiliate> GetAffiliateList(string clickId, string sourceId)
        {
            var url = BaseUrl + "/affiliates/";
            if (!String.IsNullOrEmpty(clickId))
            {
                url = AddQueryStringToUrl(url,"click_id",clickId);
            }
            if (!String.IsNullOrEmpty(sourceId))
            {
                url = AddQueryStringToUrl(url,"source_id",sourceId);
            }
           

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Affiliate>>(response);
        }
        public Affiliate CreateAffiliate(AffiliateRequest affiliateRequest)
        {
            var url = BaseUrl + "/affiliates/";

            var payLoad = JsonConvert.SerializeObject(affiliateRequest);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<Affiliate>(response);
        }
        public IDictionary<string, string> GetAffiliateMetadata(string affiliateId, string key)
        {
            var url = BaseUrl + "/affiliates/" + affiliateId + "/meta-data/";
            if (!String.IsNullOrEmpty(key))
                url += key;

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IDictionary<string, string>>(response);
        }
        public bool SetAffiliateMetadata(string affiliateId, IDictionary<string, string> metadata)
        {
            var url = BaseUrl + "/affiliates/" + affiliateId + "/meta-data/";

            var payLoad = JsonConvert.SerializeObject(metadata);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }
        public bool UpdateAffiliateMetadataKey(string affiliateId, string key, string value)
        {
            var url = BaseUrl + "/affiliates/" + affiliateId + "/meta-data/" + key;

            var payLoad = JsonConvert.SerializeObject(new { value = value });

            var response = HttpClient.PutAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }
        public bool DeleteAffiliateMetadataKey(string affiliateId, string key)
        {
            var url = BaseUrl + "/affiliates/" + affiliateId + "/meta-data/" + key;

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        #endregion

        #region Conversion

        public Conversion GetConversion(string conversionId)
        {
            var url = BaseUrl + "/conversions/" + conversionId + "/";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Conversion>(response);
        }

        public IList<Conversion> GetAllConversions()
        {
            var url = BaseUrl + "/conversions/";
            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Conversion>>(response);
        }

        public IList<Conversion> GetConversionList(
            string programId,
            string externalId,
            string affiliateId,
            bool pending,
            DateTime dateFrom,
            DateTime dateTo )
        {
            var url = BaseUrl + "/conversions/";
            if (!String.IsNullOrEmpty(programId))
            {
                url = AddQueryStringToUrl(url, "program_id", programId);
            }
            if (!String.IsNullOrEmpty(externalId))
            {
                url = AddQueryStringToUrl(url, "external_id", externalId);
            }
            if (!String.IsNullOrEmpty(affiliateId))
            {
                url = AddQueryStringToUrl(url, "affiliate_id", affiliateId);
            }

            url = AddQueryStringToUrl(url, "pending", Convert.ToString(Convert.ToInt32(pending)));
            if (dateFrom != null)
            {
                url = AddQueryStringToUrl(url, "date_from", dateFrom.ToString("yyyy-MM-dd"));
            }
            if (dateTo != null)
            {
                url = AddQueryStringToUrl(url, "date_to", dateTo.ToString("yyyy-MM-dd"));
            }
            
            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Conversion>>(response);
        }

        public Conversion CreateConversion (ConversionRequest conversionRequest, bool overrideMaxCookieTime)
        {
            var url = BaseUrl + "/conversions/?override_max_cookie_time=" + Convert.ToString(overrideMaxCookieTime);

            var payLoad = JsonConvert.SerializeObject(conversionRequest);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<Conversion>(response);
        }

        public IList<Commission> addCommissionsToConversion (string conversionId, IList<CommissionRequest>commissionRequests)
        {
            var url = BaseUrl + "/conversions/" + conversionId + "/commissions/";

            var payLoad = JsonConvert.SerializeObject(commissionRequests);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<IList<Commission>>(response);
        }

        #endregion

        #region Commission

        public Commission GetCommission (string commissionId)
        {
            var url = BaseUrl + "/commissions/" + commissionId + "/";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Commission>(response);
        }

        public bool UpdateCommission(string commissionId, CommissionUpdateRequest commissionUpdateRequest)
        {
            var url = BaseUrl + "/commissions/" + commissionId + "/";

            var payLoad = JsonConvert.SerializeObject(commissionUpdateRequest);

            var response = HttpClient.PutAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool ApproveCommission(string commissionId, bool approved)
        {
            var url = BaseUrl + "/commissions/" + commissionId + "/approval";

            var response = HttpClient.PutAsync(url, new StringContent("")).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool DeleteCommission(string commissionId, bool approved)
        {
            var url = BaseUrl + "/commissions/" + commissionId + "/approval";

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        #endregion

        #region Program

        public Program GetProgram(string programId)
        {
            var url = BaseUrl + "/programs/" + programId + "/";

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Program>(response);
        }

        public IList<Program> GetAllPrograms (string assetId)
        {
            return GetProgramList(null);
        }

        public IList<Program> GetProgramList (string assetId)
        {
            var url = BaseUrl + "/programs/";

            if (assetId != null)
                url = AddQueryStringToUrl(url, "asset_id", assetId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Program>>(response);
        }

        #endregion

        #region Utils
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
        private string AddQueryStringToUrl(string url,string key, string value)
        {
            if (url.LastIndexOf("/") == (url.Length -1))
                url += "?" + key + "=" + value;
            else
                url += "&" + key + "=" + value;
            
            return url;
        }

        #endregion

    }
}
