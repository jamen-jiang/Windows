using System;
using Windows.Application.Shared.Dto;

namespace Windows.Admin.Application
{
    public class RoleResponse: BaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoleCode { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
