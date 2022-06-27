namespace GitMarket.Domain.Models.UDSModels
{
    public class RequestBonusModel
    {
        public decimal points { get; set; }
        public string? comment { get; set; }
        public List<string> participants { get; set; }
        public bool silent { get; set; } = false;
    }
}
