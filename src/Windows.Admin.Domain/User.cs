using System.Collections.Generic;
using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class User : FullEntity<int>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string Avatar { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<Role_User> Role_User { get; set; }
        public virtual ICollection<Organization_User> Organization_User { get; set; }
    }
}
