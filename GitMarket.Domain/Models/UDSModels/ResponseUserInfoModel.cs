namespace GitMarket.Domain.Models.UDSModels
{
    public class ResponseUserInfoModel
    {
        public User user { get; set; }
        public string code { get; set; }
        public Purchase purchase { get; set; }
    }
}
