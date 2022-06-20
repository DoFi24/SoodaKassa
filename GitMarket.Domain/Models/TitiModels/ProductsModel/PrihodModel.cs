using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class PrihodModel
    {
        public ulong id { get; set; }
        public PrihodShopModel? to_shop { get; set; }
        public PrihodSkladModel? to_sklad { get; set; }
        public int? kontragent_id { get; set; }
        public int? staff_id { get; set; }
        public PrihodShopModel? from_shop { get; set; }
        public PrihodSkladModel? from_sklad { get; set; }
        public int? from_sklad_id { get; set; }
        public DateTime? prihod_date { get; set; }
        public decimal sum { get; set; }
        public string? status { get; set; }
        public string? comment { get; set; }
        public bool? is_internal { get; set; }
        public PrihodDetailModel[]? prihods_details { get; set; }
        public bool IsCheked { get; set; } = false;
    }

    public class PrihodDetailModel
    {
        public int id { get; set; }
        public int? product_id { get; set; }
        public string? product_name { get; set; }
        public string? product_category { get; set; }
        public int? product_category_id { get; set; }
        public string? unit_name { get; set; }
        public int? unit_id { get; set; }
        public int? unpack_unit_id { get; set; }
        public string? unpack_unit_name { get; set; }
        public string? product_barcodes_text { get; set; }
        public string? product_attributes_text { get; set; }
        public string[]? product_barcodes_json { get; set; }
        public ProductAttribute[]? product_attributes_json { get; set; }
        public double quantity { get; set; }
        public decimal cost_price { get; set; }
        public double markup { get; set; }
        public bool markup_calculation { get; set; }
        public decimal sale_price { get; set; }
        public decimal unpack_sale_price { get; set; }
        public DateTime? prihod_detail_date { get; set; }
        public DateTime? srok_start { get; set; }
        public DateTime? srok_end { get; set; }
        public double? ostatok { get; set; }
        public double? unpack { get; set; }
        public string? status { get; set; }
        public string? comment { get; set; }

    }
    public class ProductAttribute
    {
        public string? NAME { get; set; }
        public string? VALUE { get; set; }
    }

    public class PrihodShopModel 
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? addres { get; set; }
        public string? phone { get; set; }
        public string? comment { get; set; }

    }
    public class PrihodSkladModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? addres { get; set; }
        public string? comment { get; set; }
        public bool? is_main { get; set; }

    }
}
