using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Module.Application
{
    public class OperateAddRequest
    {
        public int ModuleId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Remark { get; set; }
    }
}
