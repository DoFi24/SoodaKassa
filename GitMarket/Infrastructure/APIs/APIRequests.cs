﻿using GitMarket.Domain.Models.APIModels;
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
using System.Windows;

namespace GitMarket.Infrastructure.APIs
{
    public class APIRequests
    {
        public static string API_PATH = @"http://127.0.0.1:3001/api/v1";
        public static async Task<bool> RegisterAsync(string? login, string? password)
        {
            try
            {
                using HttpClient httpClient = new();

                var data = new Dictionary<string, string?>
                    {
                        {"LOGIN", login },
                        {"PASSWORD", password }
                    };

                var response = await httpClient.PostAsync($"{API_PATH}/auth/login", new FormUrlEncodedContent(data));

                var responeString = await response.Content.ReadAsStringAsync();

                var result = JObject.Parse(responeString).ToObject<AuthorizationToken>();

                if (result!.success)
                {
                    Setts.Default.AuthorizationToken = result!.data!.remember_token;
                    Setts.Default.StaffId = result!.data!.USER!.Id;
                    Setts.Default.CashierId = result.data.USER.Uds_Id.ToString();
                    Setts.Default.Save();

                }

                return result.success;
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return false;
            }
        }
        public static async Task<bool> GetIsValidAsync()
        {
            try
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
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return false;
            }
        }
        public static async Task SaveChangesAsync(JournalApiModel journal)
        {
            try
            {
                using HttpClient httpClient = new();

                var data = new StringContent(JsonConvert.SerializeObject(journal).ToString(), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

                var response = await httpClient.PostAsync($"{API_PATH}/functions/FC_JOURNAL_COMMIT", data);

                await response.Content.ReadAsStringAsync();
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
            }
        }
        public static async Task<List<T>> GetFromAPIAsync<T>(RequestModelGet rmodel, string requestFunction = "FC_PRODAJA_GET_PRODUCT")
        {
            try
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
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return new List<T>();
            }
        }
        public static LastCheckResponce? GetFromAPIAsyncSingle(object rmodel, string requestFunction)
        {
            try
            {
                using (HttpClient httpClient = new())
                {
                    var data = new StringContent(JsonConvert.SerializeObject(rmodel).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

                    var response = httpClient.PostAsync($"{API_PATH}/functions/{requestFunction}", data);

                    var responeString = response.Result.Content.ReadAsStringAsync();

                    var result = JObject.Parse(responeString.Result).ToObject<LastCheckResponce>();

                    return result ?? new LastCheckResponce();
                }
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return new LastCheckResponce();
            }
        }

        #region TaxesDiscountBonuses
        public static IEnumerable<DiscountProduct> GetDiscountSum(DiscountModel taxes)
        {
            try
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
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return new List<DiscountProduct>();
            }
        }
        public static IEnumerable<BonusProducts> GetBonusSum(TaxesModel model)
        {
            try
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
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return new List<BonusProducts>();
            }
        }
        public static (IEnumerable<ProductTaxes>, decimal) GetTaxesSum(TaxesModel param)
        {
            try
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
                    pro = result!.data!.rows!.ToList();
                    NDS = result.data.nds;
                }
                return (pro, NDS);
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return (new List<ProductTaxes>(), 0);
            }
        }

        #endregion
        public static async Task<ProdajaModel?> GetSale(ProdajaModels param)
        {
            try
            {
                using (HttpClient httpClient = new())
                {
                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

                    var response = await httpClient.PostAsync($"{API_PATH}/functions/FC_PRODAJA_COMMIT", data);

                    var responeString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(responeString))
                        return null;

                    var result = JObject.Parse(responeString).ToObject<SaleResponseModel>();

                    return result is not null ? result.data : new ProdajaModel();
                }
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return null;
            }
        }
        public static async Task<string> ReturnCardTypeAsync(RequestModelGet rmodel)
        {
            try
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
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return "not";
            }
        }

        #region UDSRequests
        public static async Task<ResponseUserInfoModel?> GetUDSUserInfoRequest(string total, string code)
        {
            try
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
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return null;
            }
        }

        public static async Task<uToperationResult?> MakeUDSOperationRequest(object param)
        {
            try
            {

                using (HttpClient httpClient = new())
                {
                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Setts.Default.CompanyId + ":" + Setts.Default.ApiKey)));

                    var response = await httpClient.PostAsync($"https://api.uds.app/partner/v2/operations", data);

                    var responeString = await response.Content.ReadAsStringAsync();

                    return JObject.Parse(responeString).ToObject<uToperationResult>();

                }
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return null;
            }
        }

        public static ResponseInfoModel? GetCashRequest(object param)
        {
            try
            {
                using (HttpClient httpClient = new())
                {
                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Setts.Default.CompanyId + ":" + Setts.Default.ApiKey)));

                    var response = httpClient.PostAsync($"https://api.uds.app/partner/v2/operations/calc", data);

                    var responeString = response.Result.Content.ReadAsStringAsync();

                    var result = JObject.Parse(responeString.Result).ToObject<ResponseInfoModel>();

                    return result;

                }

            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return null;
            }
        }

        public static async Task<ResponseBonus?> GetBonusRequest(object param)
        {
            try
            {
                using (HttpClient httpClient = new())
                {

                    var data = new StringContent(JsonConvert.SerializeObject(param).ToString(), Encoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Setts.Default.AuthorizationToken);

                    var response = await httpClient.PostAsync($"https://api.uds.app/partner/v2/operations/reward", data);

                    var responeString = await response.Content.ReadAsStringAsync();

                    var result = JObject.Parse(responeString).ToObject<ResponseBonus>();

                    return result;
                }
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой!\nПовторите попытку позже.");
                return null;
            }
        }

        #endregion

        //public static async Task<ResponseModel<T>?>? GetAsyncPost<T>(string url, object shop)
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        var data = new StringContent(JsonConvert.SerializeObject(shop).ToString(), Encoding.UTF8, "application/json");

        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Setts.Default.AuthorizationToken);

        //        var response = await httpClient.PostAsync($"{API_PATH}/functions/{url}", data);

        //        var responeString = await response.Content.ReadAsStringAsync();

        //        if ((responeString.Length == 0) || responeString == null)
        //            return null;
        //        else
        //        {
        //            var result = JObject.Parse(responeString).ToObject<ResponseModel<T>>();

        //            if (result!.data?.rows == null)
        //            {
        //                return null;
        //            }

        //            if (result.message == "success")
        //            {
        //                return result;
        //            }
        //        }
        //        return null;
        //    }
        //}
    }
}

