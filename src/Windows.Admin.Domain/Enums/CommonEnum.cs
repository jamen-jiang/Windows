using System.ComponentModel;

namespace Windows.Admin.Domain.Enums
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperateTypeEnum
    {
        [Description("按钮")]
        Operate = 0,
        [Description("请求")]
        Request = 1
    }
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleTypeEnum
    {
        [Description("目录")]
        Catalog = 0,
        [Description("菜单")]
        Menu = 1,
    }
    /// <summary>
    /// 权限对象
    /// </summary>
    public enum MasterEnum
    {
        [Description("角色")]
        Role = 0,
        [Description("用户")]
        User = 1,
        [Description("组织机构")]
        Organization = 2
    }
    /// <summary>
    /// 权限通道
    /// </summary>
    public enum AccessEnum
    {
        [Description("模块")]
        Module,
        [Description("功能")]
        Operate,
    }
    /// <summary>
    /// 组织机构类型
    /// </summary>
    public enum OrganizationTypeEnum
    {
        [Description("机构")]
        Organ,
        [Description("部门")]
        Department,
        [Description("岗位")]
        Post
    }
    /// <summary>
    /// 性别类型
    /// </summary>
    public enum GenderEnum
    {
        [Description("未知")]
        Unknown = 0,
        [Description("男")]
        Man = 1,
        [Description("女")]
        Woman = 2,
    }
}
