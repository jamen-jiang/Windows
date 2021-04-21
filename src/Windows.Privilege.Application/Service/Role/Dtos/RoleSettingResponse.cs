using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Privilege.Application
{
    public class RoleSettingResponse
    {
        public List<int> UserId { get; set; }
    }
    public class ModuleIdTree
    { 
        public List<int> Id { get; set; }
        public List<ModuleIdTree> Children { get; set; }
    }
}
