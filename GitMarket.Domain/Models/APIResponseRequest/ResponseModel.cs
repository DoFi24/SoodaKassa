namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class ResponseModel<T>
    {
        public string? message { get; set; }
        public ResponesInModel<T>? data { get; set; }
    }
    public class ResponesInModel<T> 
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public T[]? rows { get; set; }
    }
}
