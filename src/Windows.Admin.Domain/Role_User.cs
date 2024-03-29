using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class Role_User : Entity
    {
        public Role_User() { }
        public Role_User(int roleId, int type,int userId)
        {
            RoleId = roleId;
            Type = type;
            RelationId = userId;
        }
        public int RoleId { get; set; }
        public int Type { get; set; }
        public int RelationId { get; set; }
    }
}
