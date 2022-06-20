namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class SpisanieModel
    {
        public int ID { get; set; }
        public int? SKLAD_ID { get; set; }
        public int? PRIHOD_ID { get; set; }
        public int? STAFF_ID { get; set; }
        public decimal? SUM { get; set; }
        public DateTime? SPISANIE_DATE { get; set; }
        public string? COMMENT { get; set; }
        public SpisanieDetail[]? SPISANIE_DETAILS { get; set; }
    }
    public class SpisanieDetail
    {
        public int ID { get; set; }
        public int? SKLAD_DETAIL_ID { get; set; }
        public int? PRIHOD_DETAIL_ID { get; set; }
        public int? PRODUCT_ID { get; set; }
        public decimal? QUANTITY { get; set; }
        public decimal? COST_PRICE { get; set; }
        public int? UNIT_ID { get; set; }
        public DateTime? SPISANIE_DETAIL_DATE { get; set; }
        public string? COMMENT { get; set; }
        public decimal? OSTATOK { get; set; }
    }
}
