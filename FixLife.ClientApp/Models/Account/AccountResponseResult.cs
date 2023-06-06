using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
