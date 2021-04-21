using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Module.Application
{
    public class OperateUrlResponse
    {
        public int ModuleId { get; set; }
        public string Controller { get; set; }
        public int OperateId { get; set; }
        public string Action { get; set; }
        public bool IsAuthorize { get; set; } = false;
    }
}
