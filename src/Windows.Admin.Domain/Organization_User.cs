using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public class Organization_User:Entity
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }

        public Organization Organization { get; set; }
        public User User { get; set; }
    }
}
