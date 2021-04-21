using Jyz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Application.Shared.Dto;
using Windows.SeedWork;

namespace Windows.Admin.Application
{
    public class OrganizationResponse: BaseResponse,ITreeNode<OrganizationResponse>
    {
        public object Id { get; set; }
        public object PId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
        public List<OrganizationResponse> Children { get; set; } = new List<OrganizationResponse>();
    }
}
