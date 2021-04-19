using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class OperateModifyRequest:OperateAddRequest
    {
        public Guid Id { get; set; }
    }
}
