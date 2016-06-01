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
using TapfiliateNet.Request;

//.NET 4.5+ only

namespace TapfiliateNet
{
    public sealed class TapfiliateClient
    {
        #region Constants and Fields

        /// <summary>
        /// The api endpoint.
        /// </summary>
        private const string _baseUrl = "https://tapfiliate.com/api";

        /// <summary>
        /// The api version.
        /// </summary>
        private const string _version = "1.4";

        /// <summary>
        /// The api key.
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// Timeout (in seconds)
        /// </summary>
        private readonly int _timeout = 20;

        /// <summary>
        /// Http Client for API requests
        /// </summary>
        private HttpClient HttpClient
        {
            get
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Api-Key", _apiKey);

                if (_timeout != 0)
                {
                    client.Timeout = TimeSpan.FromSeconds(_timeout);
                }

                return client;
            }
        }

        #endregion

        /// <summary>
        /// Initialize Tapfiliate API Client
        /// </summary>
        /// <param name="apiKey">API Key</param>
        public TapfiliateClient(string apiKey)
        {
            _apiKey = apiKey;
        }


        /// <summary>
        /// Initialize Tapfiliate API Client
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="timeout">Request timeout in seconds.</param>
        public TapfiliateClient(string apiKey, int timeout)
        {
            _apiKey = apiKey;
            _timeout = timeout;
        }

        #region Affiliate

        public Affiliate GetAffiliate(string affiliateId)
        {
            var url = GetRequestUrl("/affiliates/{0}/", affiliateId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Affiliate>(response);
        }

        public IList<Affiliate> GetAllAffiliates()
        {
            return GetAffiliateList(null, null);
        }

        public IList<Affiliate> GetAffiliateList(string clickId, string sourceId)
        {
            var url = GetRequestUrl("/affiliates/");

            if (!string.IsNullOrEmpty(clickId))
            {
                url = AddQueryStringToUrl(url, "click_id", clickId);
            }
            if (!string.IsNullOrEmpty(sourceId))
            {
                url = AddQueryStringToUrl(url, "source_id", sourceId);
            }

            var response = HttpClient.GetAsync(url).Result;
            
            return GetResponse<IList<Affiliate>>(response);
        }

        public Affiliate CreateAffiliate(AffiliateRequest affiliateRequest)
        {
            var url = GetRequestUrl("/affiliates/");

            var payLoad = JsonConvert.SerializeObject(affiliateRequest);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<Affiliate>(response);
        }

        public IDictionary<string, string> GetAffiliateMetadata(string affiliateId, string key)
        {
            var url = GetRequestUrl("/affiliates/{0}/meta-data/", affiliateId);

            if (!string.IsNullOrEmpty(key))
                url += key;

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IDictionary<string, string>>(response);
        }

        public bool SetAffiliateMetadata(string affiliateId, IDictionary<string, string> metadata)
        {
            var url = GetRequestUrl("/affiliates/{0}/meta-data/", affiliateId);

            var payLoad = JsonConvert.SerializeObject(metadata);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool UpdateAffiliateMetadataKey(string affiliateId, string key, string value)
        {
            var url = GetRequestUrl("/affiliates/{0}/meta-data/{1}/", affiliateId, key);

            var payLoad = JsonConvert.SerializeObject(new { value = value });

            var response = HttpClient.PutAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool DeleteAffiliateMetadataKey(string affiliateId, string key)
        {
            var url = GetRequestUrl("/affiliates/{0}/meta-data/{1}/", affiliateId, key);

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        #endregion

        #region Conversion

        public Conversion GetConversion(string conversionId)
        {
            var url = GetRequestUrl("/conversions/{0}/", conversionId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Conversion>(response);
        }

        public IList<Conversion> GetAllConversions()
        {
            return GetConversionList(null, null, null, null, null, null);
        }

        public IList<Conversion> GetConversionList(
            string programId,
            string externalId,
            string affiliateId,
            bool? pending,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            var url = GetRequestUrl("/conversions/");

            if (!string.IsNullOrEmpty(programId))
            {
                url = AddQueryStringToUrl(url, "program_id", programId);
            }
            if (!string.IsNullOrEmpty(externalId))
            {
                url = AddQueryStringToUrl(url, "external_id", externalId);
            }
            if (!string.IsNullOrEmpty(affiliateId))
            {
                url = AddQueryStringToUrl(url, "affiliate_id", affiliateId);
            }
            if (pending != null)
            {
                url = AddQueryStringToUrl(url, "pending", Convert.ToString(Convert.ToInt32(pending)));
            }
            if (dateFrom != null)
            {
                url = AddQueryStringToUrl(url, "date_from", dateFrom.Value.ToString("yyyy-MM-dd"));
            }
            if (dateTo != null)
            {
                url = AddQueryStringToUrl(url, "date_to", dateTo.Value.ToString("yyyy-MM-dd"));
            }
            
            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Conversion>>(response);
        }

        public Conversion CreateConversion(ConversionRequest conversionRequest, bool? overrideMaxCookieTime)
        {
            var url = GetRequestUrl("/conversions/");

            if (overrideMaxCookieTime != null)
            {
                url = AddQueryStringToUrl(url, "override_max_cookie_time", Convert.ToString(overrideMaxCookieTime));
            }

            var payLoad = JsonConvert.SerializeObject(conversionRequest);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<Conversion>(response);
        }

        public IList<Commission> AddCommissionsToConversion(string conversionId, IList<CommissionRequest> commissionRequests)
        {
            var url = GetRequestUrl("/conversions/{0}/commissions/", conversionId);

            var payLoad = JsonConvert.SerializeObject(commissionRequests);

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<IList<Commission>>(response);
        }

        #endregion

        #region Commission

        public Commission GetCommission(string commissionId)
        {
            var url = GetRequestUrl("/commissions/{0}/", commissionId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Commission>(response);
        }

        public List<Commission> GetCommissionListByReseller(string resellingId,DateTime from, DateTime to)
        {

            var url = GetRequestUrl("/reports/date/?date_from={0}&date_to={1}&sort_by=date&sort_direction=DESC", from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd"));

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<List<Commission>>(response);

        }

        public bool UpdateCommission(string commissionId, decimal amount, string comment)
        {
            var url = GetRequestUrl("/commissions/{0}/", commissionId);

            var payLoad = JsonConvert.SerializeObject(new { amount = amount, comment = comment });

            var response = HttpClient.PutAsync(url, new StringContent(payLoad)).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool ApproveCommission(string commissionId)
        {
            var url = GetRequestUrl("/commissions/{0}/approval/", commissionId);

            var response = HttpClient.PutAsync(url, null).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool DisapproveCommission(string commissionId)
        {
            var url = GetRequestUrl("/commissions/{0}/approval/", commissionId);

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        #endregion

        #region Program

        public Program GetProgram(string programId)
        {
            var url = GetRequestUrl("/programs/{0}/", programId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Program>(response);
        }

        public IList<Program> GetAllPrograms(string assetId)
        {
            var url = GetRequestUrl("/programs/");

            if (!string.IsNullOrEmpty(assetId))
            {
                url = AddQueryStringToUrl(url, "asset_id", assetId);
            }

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Program>>(response);
        }

        public string AddAffiliateToProgram(string programId, string affiliateId, bool? approved)
        {
            var url = GetRequestUrl("/programs/{0}/affiliates/", programId);

            var payLoad = JsonConvert.SerializeObject(new { affiliate = new { id = affiliateId }, approved = approved });

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            ProgramAffiliateReferralLink referralLink = GetResponse<ProgramAffiliateReferralLink>(response);
            return referralLink.Link;
        }

        public bool ApproveAffiliate(string programId, string affiliateId)
        {
            var url = GetRequestUrl("/programs/{0}/affiliates/{1}/approval/", programId, affiliateId);

            var response = HttpClient.PutAsync(url, null).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool DisapproveAffiliate(string programId, string affiliateId)
        {
            var url = GetRequestUrl("/programs/{0}/affiliates/{1}/approval/", programId, affiliateId);

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public ProgramAffiliate GetProgramAffiliate(string programId, string affiliateId)
        {
            var url = GetRequestUrl("/programs/{0}/affiliates/{1}/", programId, affiliateId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<ProgramAffiliate>(response);
        }

        #endregion

        #region Payout

        public Payout GetPayout(string payoutId)
        {
            var url = GetRequestUrl("/payouts/{0}/", payoutId);

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<Payout>(response);
        }

        public IList<Payout> GetAllPayouts()
        {
            var url = GetRequestUrl("/payouts/");

            var response = HttpClient.GetAsync(url).Result;

            return GetResponse<IList<Payout>>(response);
        }

        public IList<Payout> CreatePayout(DateTime upToDate)
        {
            var url = GetRequestUrl("/payouts/");

            var payLoad = JsonConvert.SerializeObject(new { up_to = upToDate.ToString("yyyy-MM-dd") });

            var response = HttpClient.PostAsync(url, new StringContent(payLoad)).Result;

            return GetResponse<IList<Payout>>(response);
        }

        public bool MarkPayoutAsPaid(string payoutId)
        {
            var url = GetRequestUrl("/payouts/{0}/paid", payoutId);

            var response = HttpClient.PutAsync(url, null).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public bool MarkPayoutAsUnpaid(string payoutId)
        {
            var url = GetRequestUrl("/payouts/{0}/paid", payoutId);

            var response = HttpClient.DeleteAsync(url).Result;

            return response.StatusCode == HttpStatusCode.NoContent;
        }


        #endregion

        #region Utils

        private string GetRequestUrl(string relativePath, params object[] args)
        {
            var relativeUrl = string.Format(relativePath, args);

            var baseUrl = _baseUrl + "/" + _version;

            return baseUrl + (relativeUrl.StartsWith("/") ? relativeUrl : "/" + relativeUrl);
        }

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
