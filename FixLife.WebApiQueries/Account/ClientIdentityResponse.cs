using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Account
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
