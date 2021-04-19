using System.Collections.Generic;
using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public class Organization: FullEntity<int>
    {
        public int? PId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<Role_Organization> Role_Organization { get; set; }
        public virtual ICollection<Organization_User> Organization_User { get; set; }
    }
}
