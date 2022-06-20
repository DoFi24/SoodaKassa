namespace GitMarket.Domain.Models.APIModels
{
    public class DiscountModel
    {
        public string? parameter { get; set; }
        public List<taxeData>? data { get; set; }
        public uint page { get; set; }
        public uint pageSize { get; set; }
    }
}
