using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public interface ILogOperateService
    {
        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<PageResponse<LogOperateResponse>> Query(PageRequest info);
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task Add(LogOperateRequest info);
    }
}
