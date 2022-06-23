namespace GitMarket.Domain.Models.UDSModels
{
    public class RequestInfoModel
    {
        public string code { get; set; }
        public string participant { get; set; }
        public string receipt { get; set; }
    }
    public class Participant
    {
        public string uid { get; set; }
        public string phone { get; set; }
    }
    public class Receipt
    {
        public double total { get; set; }
        public double skipLoyaltyTotal { get; set; }
        public double points { get; set; }
    }
}
