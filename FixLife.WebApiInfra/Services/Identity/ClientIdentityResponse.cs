using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Services.Identity
{
    public class ClientIdentityResponse
    {
        public int? Status { get; set; }
        public string? Token { get; set; }
        public string? Details { get; set; }
    }
}
