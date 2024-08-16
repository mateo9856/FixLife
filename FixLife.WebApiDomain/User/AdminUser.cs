using FixLife.WebApiDomain.Common;

namespace FixLife.WebApiDomain.User
{
    public class AdminUser : UserEntity
    {
        public DateTime? LastPasswordChangeDate { get; set; }
        public string CertificateSubject { get; set; }
        public DateTime? ValidCertificateTo { get; set; }
    }
}
