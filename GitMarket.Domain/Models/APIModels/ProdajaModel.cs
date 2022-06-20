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
        public ulong sklad_id { get; set; }
        public ulong shop_id { get; set; }
        public ulong staff_id { get; set; }
        public ulong? kontragent_id { get; set; }
        public decimal? pay_sum { get; set; }
        public string? comment { get; set; }
        public IEnumerable<ProdajaProduct>? rows { get; set; }
    }
}
