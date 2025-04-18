using Newtonsoft.Json;

namespace FixLife.ClientApp.Common.WebAuthentication.Clients.AuthorizationResult
{
    public class OAuthUserData
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

    }
}
