namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class SingleResponceModel<T>
    {
        public string? message { get; set; }
        public T? data { get; set; }
    }
}
