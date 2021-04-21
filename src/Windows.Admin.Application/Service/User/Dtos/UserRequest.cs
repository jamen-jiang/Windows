using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class UserRequest
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime? CreatedOnStart { get; set; }
        public DateTime? CreatedOnEnd { get; set; }
    }
}
