using FixLife.WebApiDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.User
{
    public class AdminUser : UserEntity
    {
        public DateTime? LastPasswordChangeDate { get; set; }
        public string CertificateSubject { get; set; }
        public DateTime? ValidCertificateTo { get; set; }
    }
}
