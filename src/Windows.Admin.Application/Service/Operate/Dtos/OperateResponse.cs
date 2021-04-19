using System;

namespace Windows.Admin.Application
{
    public class OperateResponse: BaseResponse
    {
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
