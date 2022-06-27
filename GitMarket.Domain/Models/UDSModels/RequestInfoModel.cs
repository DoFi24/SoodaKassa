namespace GitMarket.Domain.Models.UDSModels
{
    public class RequestInfoModel
    {
        public string code { get; set; }
        public Participant? participant { get; set; }
        public Receipt? receipt { get; set; }
    }
    public class Participant
    {
        public string uid { get; set; }
        public string phone { get; set; }
    }
    public class Receipt
    {
        public decimal total { get; set; }
        public decimal? skipLoyaltyTotal { get; set; }
        public decimal points { get; set; }
        public string? number { get; set; }
        public decimal cash { get; set; }
    }
}
