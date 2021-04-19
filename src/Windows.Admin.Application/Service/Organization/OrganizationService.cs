using AutoMapper;
using Jyz.Domain;
using Jyz.Domain.Enums;
using Jyz.Infrastructure;
using Jyz.Infrastructure.Data;
using Jyz.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class OrganizationService : BaseService,IOrganizationService
    {
        private readonly IMapper _mapper;
        public OrganizationService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<List<OrganizationResponse>> Query(OrganizationRequest info)
        {
            using (var db = NewDB())
            {
                var query = db.Organization.AsNoTracking();
                if (!info.Name.IsNullOrEmpty())
                    query = query.Where(x => x.Name.Contains(info.Name));
                List<Organization> organizations = await query.ToListAsync();
                var dtos = _mapper.Map<List<OrganizationResponse>>(organizations);
                var list = new List<OrganizationResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrganizationResponse> Detail(Guid id)
        {
            using (var db = NewDB())
            {
                var organization = await db.Organization.FindByIdAsync(id);
                return _mapper.Map<OrganizationResponse>(organization);
            }
        }
        /// <summary>
        /// 获取组织机构列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ComboBoxTreeResponse>> GetOrganizations()
        {
            using (var db = NewDB())
            {
                var organizations = await db.Organization.AsNoTracking().ToListAsync();
                var dtos = _mapper.Map<List<ComboBoxTreeResponse>>(organizations);
                var list = new List<ComboBoxTreeResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Add(OrganizationAddRequest info)
        {
            using (var db = NewDB())
            {
                Organization organization = _mapper.Map<Organization>(info.Organization);
                BeforeAdd(organization);
                await db.Organization.AddAsync(organization);
                await db.SaveChangesAsync();
                await SetOtherInfo(db, organization.Id, info.ModuleIds, info.OperateIds, info.RoleIds);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Modify(OrganizationModifyRequest info)
        {
            using (var db = NewDB())
            {
                await db.ExecSqlNoQuery("delete Role_Organization where OrganizationId=@OrganizationId", new SqlParameter("OrganizationId", info.Id));
                await db.ExecSqlNoQuery("delete Privilege where MasterValue=@MasterValue", new SqlParameter("MasterValue", info.Id));
                var organization = await db.Organization.FindByIdAsync(info.Id);
                _mapper.Map(info.Organization, organization);
                db.ModifyEntity(organization);
                await SetOtherInfo(db, info.Id, info.ModuleIds, info.OperateIds, info.RoleIds);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        ///获取当前组织机构及下面所有组织机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetCurrentAndChildrenIdList(Guid id)
        {
            using (var db = NewDB())
            {
                var organizations = await db.Organization.AsNoTracking().ToListAsync();
                List<Guid> idList = new List<Guid>();
                idList.Add(id);
                GetChildrenOrganizationIdList(organizations, idList, id);
                return idList;
            }
        }
        /// <summary>
        /// 设置用户其他关联表信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="moduleIds"></param>
        /// <param name="operateIds"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        private async Task SetOtherInfo(JyzContext db, Guid id, List<Guid> moduleIds, List<Guid> operateIds, List<Guid> roleIds)
        {
            foreach (Guid mId in moduleIds)
            {
                Privilege privilege = new Privilege(MasterEnum.Organization, id, AccessEnum.Module, mId);
                await db.AddAsync(privilege);
            }
            foreach (Guid oId in operateIds)
            {
                Privilege privilege = new Privilege(MasterEnum.Organization, id, AccessEnum.Operate, oId);
                await db.AddAsync(privilege);
            }
            foreach (Guid rId in roleIds)
            {
                Role_Organization model = new Role_Organization(rId, id);
                await db.AddAsync(model);
            }
        }
        /// <summary>
        /// 获取当前组织机构及下面所有组织机构
        /// </summary>
        /// <param name="organizations"></param>
        /// <param name="idList"></param>
        /// <param name="id"></param>
        private void GetChildrenOrganizationIdList(List<Organization> organizations, List<Guid> idList, Guid organizationd)
        {
            var childrens = organizations.Where(x => x.PId == organizationd).Select(s=>s.Id).ToList();
            if (childrens.Count > 0)
            {
                idList.AddRange(childrens);
                foreach (Guid id in childrens)
                {
                    GetChildrenOrganizationIdList(organizations, idList, id);
                }
            }
        }
    }
}
