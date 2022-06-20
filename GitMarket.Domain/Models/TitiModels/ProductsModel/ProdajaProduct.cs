namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class ProdajaProduct
    {
        public int prihod_detail_id { get; set; }
        public int bonus_id { get; set; }
        public int service_id { get; set; } = 0;
        public bool is_service { get; set; } = false;
        public int discount_id { get; set; }
        public decimal sale_price { get; set; }
        public decimal quantity { get; set; }
        public decimal prepend_sum { get => sale_price * quantity;} 
        public decimal discount_sum { get; set; }
        public decimal bonus_sum { get; set; }
        public decimal pay_bonus_sum { get; set; }
        public decimal taxe_sum { get; set; }
        public string? comment { get; set; }
        public decimal total_sum { get => prepend_sum - discount_sum; }
    }
}
