using System;
using Windows.Application.Shared.Dto;

namespace Windows.Module.Application
{
    public class OperateResponse: BaseResponse
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int Id { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
