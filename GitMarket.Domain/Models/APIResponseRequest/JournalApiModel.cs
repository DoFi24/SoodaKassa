namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class JournalApiModel
    {
        public string? parameter { get; set; }
        public JournalApiModelData? data { get; set; }
    }

    public class JournalApiModelData
    {
        public ulong? shop_id { get; set; }
        public uint? staff_id { get; set; }
        public List<JournalValue>? rows { get; set; }
    }
    public class JournalValue
    {
        public string? value { get; set; }
    }
}
