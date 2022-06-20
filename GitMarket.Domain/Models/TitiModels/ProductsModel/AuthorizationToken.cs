namespace GitMarket.Domain.Models.TitiModels.ProductsModel
{
    public class AuthorizationToken
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public RembemberUser? data { get; set; }

    }
    public class RembemberUser 
    {
        public string? remember_token { get; set; }
        public User? USER { get; set; }
    }
    public class User
    {
        public uint Id { get; set; }
        public string? Login { get; set; }
        public string? Paswword { get; set; }
        public RURoles[]? Roles { get; set; }
    }
    public class RURoles 
    {
        public uint Id { get; set; }
        public string? Name { get; set; }
        public string? Key { get; set; }
        public uint Priority { get; set; }
    }
}
