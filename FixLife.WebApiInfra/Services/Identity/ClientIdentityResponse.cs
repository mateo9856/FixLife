namespace FixLife.WebApiInfra.Services.Identity
{
    public class ClientIdentityResponse
    {
        public int? Status { get; set; }
        public string? Token { get; set; }
        public string? Details { get; set; }
        public string? Email { get; set; }
        public bool? HasPlans { get; set; }
    }
}
