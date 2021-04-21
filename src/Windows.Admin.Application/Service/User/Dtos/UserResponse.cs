using System;
using System.Collections.Generic;
using Windows.Application.Shared.Dto;

namespace Windows.Admin.Application
{
    public class UserResponse: BaseResponse
    {
        public int Id { get; set; }
        public List<int> OrganizationIds { get; set; } = new List<int>();
        public List<string> OrganizationNames { get; set; } = new List<string>();
        public List<int> RoleIds { get; set; } = new List<int>();
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
