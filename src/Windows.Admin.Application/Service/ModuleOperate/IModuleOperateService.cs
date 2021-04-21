using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Admin.Domain.Enums;

namespace Windows.Admin.Application
{
    public interface IModuleOperateService
    {
        /// <summary>
        /// 根据用户Id获取授权的菜单操作列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ModuleOperateResponse>> GetAuthorizeModuleOperates(int userId);
        /// <summary>
        /// 获取模块操作树
        /// </summary>
        /// <returns></returns>
        Task<List<ModuleOperateResponse>> GetModuleOperates();
        /// <summary>
        /// 获取授权的模块操作Id列表
        /// </summary>
        /// <param name="master"></param>
        /// <param name="masterValue"></param>
        /// <returns></returns>
        Task<AuthorizeModuleOperateIdsResponse> GetAuthorizeModuleOperateIds(MasterEnum master, int masterValue);
    }
}
