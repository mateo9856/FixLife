using Newtonsoft.Json;

namespace FixLife.ClientApp.Models.Account
{
    public class AccountResponseResult
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "details")]
        public string Details { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "hasplans")]
        public bool? HasPlans { get; set; }

    }
}
