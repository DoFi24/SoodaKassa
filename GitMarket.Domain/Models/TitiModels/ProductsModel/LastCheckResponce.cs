namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class LastCheckResponce
    {
        public string? message { get; set; }
        public LastCheckResponceData? data { get; set; }
    }
    public class LastCheckResponceData
    {
        public long prodaja_id { get; set; }
        public long check_id { get; set; }
        public string? check_no { get; set; }
        public long smena_id { get; set; }
        public long staff_id { get; set; }
    }
}
