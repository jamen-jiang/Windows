using System.ComponentModel;

namespace Windows.Privilege.Domain.Enums
{
    public enum MasterEnum
    {
        [Description("角色")]
        Role = 0,
        [Description("用户")]
        User = 1,
        [Description("组织机构")]
        Organization = 2
    }
}
