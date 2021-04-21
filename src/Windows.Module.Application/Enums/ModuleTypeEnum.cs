using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Module.Application.Enums
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleTypeEnum
    {
        [Description("目录")]
        Catalog = 0,
        [Description("菜单")]
        Menu = 1,
    }
}
