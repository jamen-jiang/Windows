using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Module.Application.Enums
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperateTypeEnum
    {
        [Description("按钮")]
        Operate = 0,
        [Description("请求")]
        Request = 1
    }
}
