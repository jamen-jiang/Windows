using System.Collections.Generic;
using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class Role : FullEntity<int>
    {
        public string Name { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<Role_User> Role_User { get; set; }
        public virtual ICollection<Role_Organization> Role_Organization { get; set; }
    }
}
