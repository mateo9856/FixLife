using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Common
{
    public class BaseBusinessEntity : IAudible
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public BaseBusinessEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

    }
}
