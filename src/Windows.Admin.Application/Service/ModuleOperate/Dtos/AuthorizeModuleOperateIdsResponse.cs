using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class AuthorizeModuleOperateIdsResponse
    {
        public List<int> ModuleIds { get; set; } = new List<int>();
        public List<int> OperateIds { get; set; } = new List<int>();
    }
}
