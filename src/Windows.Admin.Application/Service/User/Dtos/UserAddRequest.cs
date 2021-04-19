using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class UserAddRequest
    {
        public UserInfo User { get; set; }
        public List<Guid> RoleIds { get; set; }
        public List<Guid> ModuleIds { get; set; }
        public List<Guid> OperateIds { get; set; }
    }
    public class UserInfo
    {
        public List<Guid> OrganizationIds { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string Remark { get; set; }
    }
}
