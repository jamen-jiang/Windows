using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public interface IOrganizationService
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<List<OrganizationResponse>> Query(OrganizationRequest info);
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrganizationResponse> Detail(Guid id);
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        Task<List<ComboBoxTreeResponse>> GetOrganizations();
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task Add(OrganizationAddRequest info);
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task Modify(OrganizationModifyRequest info);
        /// <summary>
        /// 获取当前部门及下面所有部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<Guid>> GetCurrentAndChildrenIdList(Guid id);
    }
}
