using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class Role_User : Entity
    {
        public Role_User() { }
        public Role_User(int roleId, int userId)
        {
            RoleId = roleId;
            UserId = userId;
        }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
