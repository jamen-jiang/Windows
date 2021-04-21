using System;
using System.Collections.Generic;

namespace Windows.Admin.Application
{
    public interface ICommonService
    {
        /// <summary>
        /// 获取枚举类型下拉框列表
        /// </summary>
        /// <returns></returns>
        List<ComboBoxResponse> GetComboBoxList<T>() where T : Enum;
    }
}
