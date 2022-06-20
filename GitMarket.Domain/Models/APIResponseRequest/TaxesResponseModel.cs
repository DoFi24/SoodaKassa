namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class TaxesResponseModel<T>
    {
        public string? message { get; set; }
        public TaxesResponseInModel<T>? data { get; set; }
    }
    public class TaxesResponseInModel<T>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public decimal nds { get; set; }
        public T[]? rows { get; set; }
    }
}
