using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class OrganizationAddRequest
    {
        public OrganizationInfo Organization { get; set; }
        public List<Guid> RoleIds { get; set; }
        public List<Guid> ModuleIds { get; set; }
        public List<Guid> OperateIds { get; set; }
    }
    public class OrganizationInfo
    {
        public Guid? PId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
