using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class RoleModifyRequest: RoleAddRequest
    {
        public int Id { get; set; }
    }
}
