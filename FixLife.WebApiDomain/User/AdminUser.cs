using FixLife.WebApiDomain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.User
{
    public class AdminUser : UserEntity
    {
        [BsonElement("lastPasswordChangeDate")]
        public DateTime? LastPasswordChangeDate { get; set; }
        [BsonElement("certificateSubject")]
        public string CertificateSubject { get; set; }
        [BsonElement("validCertificateTo")]
        public DateTime? ValidCertificateTo { get; set; }
    }
}
