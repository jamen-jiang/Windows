using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public interface IRoleService
    {
        /// <summary>
        /// 根据id获取role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoleResponse> Detail(Guid id);
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<PageResponse<RoleResponse>> Query(PageRequest<RoleRequest> info);
        /// <summary>
        /// 根据用户Id获取角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<RoleResponse>> GetUserRoles(Guid userId);
        /// <summary>
        /// 根据部门Id获取角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<RoleResponse>> GetOrganizationRoles(Guid organizationId);
        /// <summary>
        /// 角色信息保存
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task Save(RoleModifyRequest info);
    }
}
