namespace FixLife.ClientApp.Common.WebAuthentication.Clients
{
    public class OAuthClient
    {
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string ClientSecret { get; set; } = string.Empty;

    }
}
