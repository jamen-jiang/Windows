using System;
using Windows.Application.Shared.Dto;

namespace Windows.Privilege.Application
{
    public class RoleResponse: BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoleCode { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
