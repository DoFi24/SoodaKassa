using GitMarket.Domain.Models.TitiModels.ProductsModel;

namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class CheckModel
    {
        public ulong id { get; set; }
        public string? docnum { get; set; }
        public PrihodShopModel? shop { get; set; }
        public PrihodSkladModel? sklad { get; set; }
        public Staff? staff { get; set; }
        public ulong? kontragent { get; set; }
        public DateTime? prodaja_started { get; set; }
        public decimal? prepend_sum { get; set; }
        public decimal discount_sum { get; set; }
        public decimal bonus_sum { get; set; }
        public decimal pay_bomus_sum { get; set; }
        public decimal total_sum { get; set; }
        public decimal pay_sum { get; set; }
        public decimal zdach_sum { get; set; }
        public string? status { get; set; }
        public ulong? comment { get; set; }
        public List<CheckDetailProduct>? details { get; set; }
    }
    public class CheckDetailProduct 
    {
        public ulong id { get; set; }
        public ulong? product_id { get; set; }
        public string? product_name { get; set; }
        public string? product_category { get; set; }
        public ulong? product_category_id { get; set; }
        public ulong? prihod_detail_id { get; set; }
        public string? unit { get; set; }
        public ulong? unit_id { get; set; }
        public string? unpack_unit { get; set; }
        public string[]? product_barcodes_string { get; set; }
        public string[]? product_attributes_string { get; set; }
        public object? product_barcodes_json { get; set; }
        public object? product_attributes_json { get; set; }
        public DateTime? srok_start { get; set; }
        public DateTime? srok_end { get; set; }
        public decimal quantity { get; set; }
        public decimal sale_price { get; set; }
        public ulong? discount_id { get; set; }
        public ulong? bonus_id { get; set; }
        public decimal prepend_sum { get; set; }
        public decimal discount_sum { get; set; }
        public decimal bonus_sum { get; set; }
        public decimal pay_bonus_sum { get; set; }
        public decimal taxe_sum { get; set; }
        public decimal total_sum { get; set; }
        public decimal ostatok { get; set; }
        public string? status { get; set; }
        public string? comment { get; set; }
    }
    public class Staff 
    {
        public ulong staff { get; set; }
        public string? full_name { get; set; }
    }
}
