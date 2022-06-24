using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Views.MainWindows
{
    public class udsuserinfodemo
    {
        //public string uuid_v4 = "4263fb8e-c132-41f0-b086-65b6c48996a5";
        //public string companyId = "549756108956";
        //public string apiKey = "YWRlNDIyZDEtZDU5Ny00NmZjLThlMDctZDFjNTIzNWRkMWFj";
        //public string urluserinfo = "http://192.168.8.42/clienter.php";

        //private static async Task<string> GetDataAsync(string adr, string compid, string apikey, string uuid)
        //{
        //    WebRequest request = WebRequest.Create(adr);
        //    request.Method = "GET";
        //    request.Headers.Add("20", "Accept: application/json");
        //    request.Headers.Add("Accept-Charset: utf-8");
        //    request.Headers.Add("Authorization: Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(compid + ":" + apikey)));
        //    request.Headers.Add("X-Origin-Request-Id: " + uuid);

        //    string dt = DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        //    var zone = TimeZoneInfo.Local;
        //    dt = dt + zone.DisplayName.Replace("(UTC", "").Replace(")", "").Substring(0, 6);
        //    request.Headers.Add("X-Timestamp: " + dt);
        //    request.ContentType = "application/json";
        //    WebResponse response = await request.GetResponseAsync();
        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            string s = reader.ReadToEnd();
        //            return s;
        //        }
        //    }
        //    response.Close();
        //    return "";
        //}
        //public static async Task<bool> GetUDSInfo(object requestInfo, string adr, string compid, string apikey, string uuid)
        //{
        //    using (HttpClient httpClient = new())
        //    {
        //        var data = new StringContent(JsonConvert.SerializeObject(requestInfo).ToString(), Encoding.UTF8, "application/json");

        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(compid + ":" + apikey)));

        //        var response = await httpClient.PostAsync($"{adr}", data);

        //        var responeString = await response.Content.ReadAsStringAsync();

        //        //var result = JObject.Parse(responeString).ToObject<ResponseModel<T>>();

        //        //return result.data?.rows != null ? result.data.rows.ToList() : new List<T>();
        //        return false;
        //    }
        //}
        //private static Uuserinfo get_userInfo(string userpromo)
        //{
        //    Task<string> task = Task.Run<string>(async () => await GetDataAsync(urluserinfo + "?exchangeCode=true&code=" + userpromo, this.companyId, this.apiKey, this.uuid_v4));
        //    string jsone = task.Result;
        //    if (jsone.Length > 0)
        //    {
        //        JObject obj = JObject.Parse(jsone);
        //        string Suserinfo = obj["user"].ToString();
        //        JObject Uuserobj = JObject.Parse(Suserinfo);
        //        string Ufullname = Uuserobj["displayName"].ToString();
        //        string Usex = Uuserobj["gender"].ToString();
        //        if (Usex.Equals("Male"))
        //        {
        //            Usex = "Мужской";
        //        }
        //        if (Usex.Equals("FEMALE"))
        //        {
        //            Usex = "Женский";
        //        }
        //        if (Usex.Equals("NOT_SPECIFIED"))
        //        {
        //            Usex = "Не указан";
        //        }
        //        string Uphone = Uuserobj["phone"].ToString();
        //        if (Uphone.Length == 0)
        //        {
        //            Uphone = "Не указан";
        //        }
        //        string Uparticipant = Uuserobj["participant"].ToString();
        //        JObject Uparticipantobj = JObject.Parse(Uparticipant);
        //        string UcashbackRate = Uparticipantobj["cashbackRate"].ToString();
        //        string UdiscountRate = Uparticipantobj["discountRate"].ToString();
        //        string Upoints = Uparticipantobj["points"].ToString();

        //        Uuserinfo info1 = new Uuserinfo();
        //        info1.full_name = Ufullname;

        //        return info1;
        //    }
        //    return null;
        //}
    }

    public class Uuserinfo
    {
        public string full_name { get; set; }
        public string sex { get; set; }
        public string phone { get; set; }
        public string cashbackRate { get; set; }
        public string discountRate { get; set; }
        public string points { get; set; }
    }

}
