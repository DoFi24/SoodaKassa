using GitMarket.Domain.Models.APIModels;
using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Domain.Models.UDSModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Infrastructure.APIs
{
    public class APIRequests
    {
        public static readonly string API_PATH = @"http://127.0.0.1:3001/api/v1";
        public static async Task<bool> RegisterAsync(string? login, string? password)
        {
            using (HttpClient httpClient = new())
            {
                var data = new Dictionary<string, string?>
                {
                    {"LOGIN", login },
                    {"PASSWORD", password }
                };

                var response = await httpClient.PostAsync($"{API_PATH}/auth/login", new FormUrlEncodedContent(data));

                var responeString = await response.Content.ReadAsStringAsync();

                var result = JObject.Parse(responeString).ToObject<AuthorizationToken>();

                if (result.success)
                {
                    Setts.Default.AuthorizationToken = result.data.remember_token;
                    Setts.Default.StaffId = result.data.USER.Id;
                    Setts.Default.Save();

                }

                return result.success;
            }
        }
        public static async Task<bool> GetIsValidAsync()
        {
            using (HttpClient httpClient = new())
            {
                var data = new Dictionary<string, string?>
                {
                    {"remember_token",Setts.Default.AuthorizationToken}
                };

                var response = await httpClient.PostAsync($"{API_PATH}/auth/valid-token", new FormUrlEncodedContent(data));

                var responeString = await response.Content.ReadAsStringAsync();

                var result = JObject.Parse(responeString).ToObject<IsValidModel>();

                return result.success;
            }
        }
        public static async Task SaveChangesAsync(JournalApiModel journal)
        {
            using HttpClient httpClient = new();

            var data = new StringContent(JsonConvert.SerializeObject(journal).ToString(), Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

            var response = await httpClient.PostAsync($"{API_PATH}/functions/FC_JOURNAL_COMMIT", data);

            await response.Content.ReadAsStringAsync();
        }
        public static async Task<List<T>> GetFromAPIAsync<T>(RequestModelGet rmodel, string requestFunction = "FC_PRODAJA_GET_PRODUCT")
        {
            using (HttpClient httpClient = new())
            {
                var data = new StringContent(JsonConvert.SerializeObject(rmodel).ToString(), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

                var response = await httpClient.PostAsync($"{API_PATH}/functions/{requestFunction}", data);

                var responeString = await response.Content.ReadAsStringAsync();

                var result = JObject.Parse(responeString).ToObject<ResponseModel<T>>();

                return result.data?.rows != null ? result.data.rows.ToList() : new List<T>();
            }
        }

        #region TaxesDiscountBonuses
        public static IEnumerable<DiscountProduct> GetDiscountSum(DiscountModel taxes, int page = 1)
        {
            List<DiscountProduct> product = new();
            var url = $"{API_PATH}/functions/FC_CALC_DISCOUNTS_SUM";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = $"Bearer {Setts.Default.AuthorizationToken}";

            httpRequest.ContentType = "application/json";

            var data = JsonConvert.SerializeObject(taxes).ToString();

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = JObject.Parse(streamReader.ReadToEnd()).ToObject<ResponseModel<DiscountProduct>>();

                product = result.data.rows.ToList();
            }
            return product;
        }
        public static IEnumerable<BonusProducts> GetBonusSum(TaxesModel model)
        {
            IEnumerable<BonusProducts> pro = new List<BonusProducts>();
            var url = $"{API_PATH}/functions/FC_CALC_BONUS_SUM";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = $"Bearer {Setts.Default.AuthorizationToken}";
            httpRequest.ContentType = "application/json";

            var data = JsonConvert.SerializeObject(model).ToString(); ;

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = JObject.Parse(streamReader.ReadToEnd()).ToObject<ResponseModel<BonusProducts>>();

                pro = result.data.rows.ToList();
            }
            return pro;
        }
        public static (IEnumerable<ProductTaxes>, decimal) GetTaxesSum(TaxesModel param)
        {
            IEnumerable<ProductTaxes> pro = new List<ProductTaxes>();

            var url = $"{API_PATH}/functions/FC_CALC_TAXES_SUM";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = $"Bearer {Setts.Default.AuthorizationToken}";

            httpRequest.ContentType = "application/json";

            var data = JsonConvert.SerializeObject(param).ToString();

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            decimal NDS = 0;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = JObject.Parse(streamReader.ReadToEnd()).ToObject<TaxesResponseModel<ProductTaxes>>();
                pro = result.data.rows.ToList();
                NDS = result.data.nds;
            }
            return (pro, NDS);
        }
        #endregion
        public static async Task<ProdajaModel> GetSale(ProdajaModels param, int page = 1)
        {
            using (HttpClient httpClient = new())
            {
                var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

                var response = await httpClient.PostAsync($"{API_PATH}/functions/FC_PRODAJA_COMMIT", data);

                var responeString = await response.Content.ReadAsStringAsync();

                var result = JObject.Parse(responeString).ToObject<SaleResponseModel>();

                return result is not null ? result.data : new ProdajaModel();
            }
        }
        public static async Task<string> ReturnCardTypeAsync(RequestModelGet rmodel)
        {
            using (HttpClient httpClient = new())
            {
                var data = new StringContent(JsonConvert.SerializeObject(rmodel).ToString(), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

                var response = await httpClient.PostAsync($"{API_PATH}/functions/FC_VERIFY_BARCODES", data);

                var responeString = await response.Content.ReadAsStringAsync();

                var result = JObject.Parse(responeString).ToObject<ResponseModel<VerifyModel>>();

                return result?.message == "success" ? result?.data?.rows.First().data.First().full_name : "not";
            }
        }

        #region UDSRequests

        public static async Task<ResponseUserInfoModel?> GetUDSUserInfoRequest(string total, string code)
        {
            using (HttpClient httpClient = new())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Setts.Default.CompanyId + ":" + Setts.Default.ApiKey)));
                try
                {
                    var response = await httpClient.GetStringAsync($"https://api.uds.app/partner/v2/customers/find?exchangeCode=true&total={total}&code={code}");
                    if (String.IsNullOrEmpty(response))
                        return null;
                    var result = JObject.Parse(response).ToObject<ResponseUserInfoModel>();

                    return result;
                }
                catch 
                {
                    return null;
                }
            }
        }

        public static async Task<uToperationResult?> MakeUDSOperationRequest(object param)
        {
            using (HttpClient httpClient = new())
            {
                try
                {
                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Setts.Default.CompanyId + ":" + Setts.Default.ApiKey)));

                    var response = await httpClient.PostAsync($"https://api.uds.app/partner/v2/operations", data);

                    var responeString = await response.Content.ReadAsStringAsync();

                    var result = JObject.Parse(responeString).ToObject<uToperationResult>();

                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static ResponseInfoModel? GetCashRequest(object param)
        {
            using (HttpClient httpClient = new())
            {
                try
                {
                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Setts.Default.CompanyId + ":" + Setts.Default.ApiKey)));

                    var response = httpClient.PostAsync($"https://api.uds.app/partner/v2/operations/calc", data);

                    var responeString = response.Result.Content.ReadAsStringAsync();

                    var result = JObject.Parse(responeString.Result).ToObject<ResponseInfoModel>();

                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static async Task<ResponseBonus?> GetBonusRequest(object param)
        {
            using (HttpClient httpClient = new())
            {
                try
                {
                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Setts.Default.AuthorizationToken);

                    var response = await httpClient.PostAsync($"https://api.uds.app/partner/v2/operations/reward", data);

                    var responeString = await response.Content.ReadAsStringAsync();

                    var result = JObject.Parse(responeString).ToObject<ResponseBonus>();

                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

    }
}

