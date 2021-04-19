using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class Role_Organization : Entity
    {
        public Role_Organization(int roleId, int organizationId)
        {
            RoleId = roleId;
            OrganizationId = organizationId;
        }
        public int RoleId { get; set; }
        public int OrganizationId { get; set; }

        public Role Role { get; set; }
        public Organization Organization { get; set; }
    }
}
