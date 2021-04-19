using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class RoleSettingResponse
    {
        public List<Guid> UserId { get; set; }
    }
    public class ModuleIdTree
    { 
        public List<Guid> Id { get; set; }
        public List<ModuleIdTree> Children { get; set; }
    }
}
