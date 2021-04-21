using Windows.SeedWork;

namespace Windows.Privilege.Domain
{
    public partial class Role_Relation : Entity
    {
        public Role_Relation() { }
        public Role_Relation(int roleId, int type,int userId)
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
