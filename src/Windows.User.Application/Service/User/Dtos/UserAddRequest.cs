using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.User.Application
{
    public class UserAddRequest
    {
        public UserInfo User { get; set; }
        public List<int> RoleIds { get; set; }
        public List<int> ModuleIds { get; set; }
        public List<int> OperateIds { get; set; }
    }
    public class UserInfo
    {
        public List<int> OrganizationIds { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string Remark { get; set; }
    }
}
