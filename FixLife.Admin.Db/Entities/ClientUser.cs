using FixLife.Admin.Db.Entities.Base;

namespace FixLife.Admin.Db.Entities
{
    public class ClientUser : EntityBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
