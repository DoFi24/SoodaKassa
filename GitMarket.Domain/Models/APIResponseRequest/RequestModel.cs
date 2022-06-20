using System.Collections;

namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class RequestModelGet
    {
        public string? parameter { get; set; }
        public dynamic? data { get; set; }
        public uint page { get; set; }
        public uint pageSize { get; set; }
        public uint shopId { get; set; }
        public uint staffId { get; set; }
    }
}
