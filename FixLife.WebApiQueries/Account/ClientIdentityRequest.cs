using FixLife.WebApiQueries.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Account
{
    public class ClientIdentityRequest
    {
        public string Credentials { get; set; }
        public string Password { get; set; }
        public LoginTypeEnum LoginType { get; set; }
    }
}
