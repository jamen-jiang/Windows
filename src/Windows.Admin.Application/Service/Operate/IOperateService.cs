using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Admin.Domain;
using Windows.Application.Shared.Dto;

namespace Windows.Admin.Application
{
    public interface IOperateService
    {
        /// <summary>
        /// 根据privilegeIds获取操作列表
        /// </summary>
        /// <param name="privilegeIds"></param>
        /// <returns></returns>
        List<Operate> GetListByPrivilegeIds(List<int> privilegeIds);
        /// <summary>
        /// 获取对应的功能列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<PageResponse<OperateResponse>> Query(PageRequest<OperateRequest> info);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         Task<OperateResponse> Detail(int id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task Add(OperateAddRequest info);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        Task Modify(OperateModifyRequest info);
    }
}
