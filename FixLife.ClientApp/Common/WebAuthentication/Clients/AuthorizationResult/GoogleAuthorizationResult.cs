using Newtonsoft.Json;

namespace FixLife.ClientApp.Common.WebAuthentication.Clients.AuthorizationResult
{
    public class GoogleAuthorizationResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
