using FixLife.ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Sessions
{
    public static class UserSession
    {
        public static string Token { get;set; }
        public static string Email { get; set; }
        public static AppPlan UserPlans { get; set; }
    }
}
