namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class GetRequest
    {
        public string? parameter { get; set; }
        public object? data { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

    }
}
