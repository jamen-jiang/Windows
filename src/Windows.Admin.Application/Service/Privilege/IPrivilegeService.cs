using System.Collections.Generic;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public interface IPrivilegeService
    {
        /// <summary>
        /// 获取全部url并且此用户Id是否已授权
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<OperateUrlResponse>> GetOperateUrlsByUserId(int userId);
    }
}
