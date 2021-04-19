using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class ModuleAndPrivilegeResponse
    {
        public List<ModuleResponse> Modules { get; set; } = new List<ModuleResponse>();
        public List<Guid> SelectedModules { get; set; } = new List<Guid>();
        public List<Guid> SelectedOperates { get; set; } = new List<Guid>();
    }
}
