namespace GitMarket.Domain.Models.APIModels
{
    public class TaxesModel
    {
        public string? parameter { get; set; }
        public List<taxeData>? data { get; set; }
        public uint page { get; set; }
        public uint pageSize { get; set; }
    }

    public class taxeData
    {
        public long id { get; set; }
        public decimal quantity { get; set; }
    }
}
