using GitMarket.Domain.Models.TitiModels.ProductsModel;

namespace GitMarket.Domain.Models.APIModels
{
    public class ProdajaModels
    {
        public string? parameter { get; set; }
        public data? data { get; set; }
        public uint page { get; set; }
        public uint pageSize { get; set; }
        public decimal? sum { get; set; }
        public decimal? esum { get; set; }
    }
    public class data
    {
        public uint? sklad_id { get; set; }
        public uint? shop_id { get; set; }
        public uint? staff_id { get; set; }
        public decimal? discount_sum { get; set; }
        public uint? kassa_id { get; set; }
        public long? prodaja_id { get; set; } = null;
        public long? kontragent_id { get; set; }
        public decimal? pay_sum { get; set; }
        public string? comment { get; set; }
        public IEnumerable<ProdajaProduct>? rows { get; set; }
    }
}
