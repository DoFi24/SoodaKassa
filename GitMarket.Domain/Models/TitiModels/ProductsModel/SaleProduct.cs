namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class SaleProduct
    {
        public long? Shop_Id { get; set; }
        public long? Sklad_Id { get; set; }
        public long? Prihod_Detail_Id { get; set; }
        public long? Product_Id { get; set; }
        public string? Product_Barcode_Tex { get; set; }
        public string? Product_Name { get; set; }
        public string? Product_Category { get; set; }
        public string? Product_Attributes_Text { get; set; }
        public string? Unit_Name { get; set; }
        public string? Unpack_Unit_Name { get; set; }
        public decimal Sale_Price { get; set; }
        public decimal? Unpack_Sale_Price { get; set; }
        public decimal? Unpack { get; set; }
        public bool? IsUnpack { get; set; } = false;
        public decimal Quantity { get; set; }

        private decimal quantityCount = 0m;
        public decimal QuantityCount
        {
            get => Math.Round(quantityCount, 2);
            set
            {
                quantityCount = value;
            }
        }
        public DateTime? Srok_Start { get; set; }
        public DateTime? Srok_End { get; set; }
        public int? Unit_Id { get; set; }
        public int? Unpack_Unit_Id { get; set; }
        public int? Product_Category_Id { get; set; }
        public string[]? Product_Barcodes_Json { get; set; }
        public object[]? Product_Attributes_Json { get; set; }
        public string? Comment { get; set; }
        public decimal Itog { get => (QuantityCount * Sale_Price)+(QuantityCount * Sale_Price * Discount);}
        public decimal Summa { get => Itog - Discount; }
        public decimal Discount { get; set; } = 0;
        public int? Service_id { get; set; } = 0;
        public bool? Is_service { get; set; } =false;

    }
}
