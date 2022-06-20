namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class VerifyModel
    {
        public string? message { get; set; }
        public string? error { get; set; }
        public string? barcodeType { get; set; }
        public VerifyData[]? data { get; set; }
    }
    public class VerifyData 
    {
        public string? full_name { get; set; }
        public string? Bonuses { get; set; }
    }
}
