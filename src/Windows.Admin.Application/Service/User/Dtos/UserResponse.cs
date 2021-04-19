using System;
using System.Collections.Generic;

namespace Windows.Admin.Application
{
    public class UserResponse: BaseResponse
    {
        public Guid Id { get; set; }
        public List<Guid> OrganizationIds { get; set; } = new List<Guid>();
        public List<string> OrganizationNames { get; set; } = new List<string>();
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
        public List<string> RoleNames { get; set; } = new List<string>();
        public string UserName { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string Avatar { get; set; }
        public string Remark { get; set; }
    }
}
