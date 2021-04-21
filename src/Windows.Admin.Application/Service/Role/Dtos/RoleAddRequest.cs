using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class RoleAddRequest
    {
        public RoleInfo Role { get; set; }
        public List<int> UserIds { get; set; }
        public List<int> ModuleIds { get; set; }
        public List<int> OperateIds { get; set; }
    }
    public class RoleInfo
    {
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}
