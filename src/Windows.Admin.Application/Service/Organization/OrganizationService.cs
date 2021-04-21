using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Admin.Domain;
using Windows.Admin.Domain.Enums;
using Windows.Admin.Infrastructure.EFCore;
using Windows.Application.Shared.Service;
using Windows.Infrastructure.EFCore;
using Windows.Infrastructure.Extensions;

namespace Windows.Admin.Application
{
    public class OrganizationService : BaseService,IOrganizationService
    {
        private readonly IMapper _mapper;
        private readonly AdminDbContext _db;
        public OrganizationService(AdminDbContext db,IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<List<OrganizationResponse>> Query(OrganizationRequest info)
        {
            using (_db)
            {
                var query = _db.Organization.AsNoTracking();
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
        public async Task<OrganizationResponse> Detail(int id)
        {
            using (_db)
            {
                var organization = await _db.Organization.FindByIdAsync(id);
                return _mapper.Map<OrganizationResponse>(organization);
            }
        }
        /// <summary>
        /// 获取组织机构列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ComboBoxTreeResponse>> GetOrganizations()
        {
            using (_db)
            {
                var organizations = await _db.Organization.AsNoTracking().ToListAsync();
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
            using (_db)
            {
                Organization organization = _mapper.Map<Organization>(info.Organization);
                //BeforeAdd(organization);
                await _db.Organization.AddAsync(organization);
                await _db.SaveChangesAsync();
                await SetOtherInfo(_db, organization.Id, info.ModuleIds, info.OperateIds, info.RoleIds);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Modify(OrganizationModifyRequest info)
        {
            using (_db)
            {
                await _db.ExecSqlNoQuery("delete Role_Organization where OrganizationId=@OrganizationId", new SqlParameter("OrganizationId", info.Id));
                await _db.ExecSqlNoQuery("delete Privilege where MasterValue=@MasterValue", new SqlParameter("MasterValue", info.Id));
                var organization = await _db.Organization.FindByIdAsync(info.Id);
                _mapper.Map(info.Organization, organization);
                _db.ModifyEntity(organization);
                await SetOtherInfo(_db, info.Id, info.ModuleIds, info.OperateIds, info.RoleIds);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        ///获取当前组织机构及下面所有组织机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<int>> GetCurrentAndChildrenIdList(int id)
        {
            using (_db)
            {
                var organizations = await _db.Organization.AsNoTracking().ToListAsync();
                List<int> idList = new List<int>();
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
        private async Task SetOtherInfo(AdminDbContext db, int id, List<int> moduleIds, List<int> operateIds, List<int> roleIds)
        {
            foreach (int mId in moduleIds)
            {
                Privilege privilege = new Privilege(MasterEnum.Organization, id, AccessEnum.Module, mId);
                await db.AddAsync(privilege);
            }
            foreach (int oId in operateIds)
            {
                Privilege privilege = new Privilege(MasterEnum.Organization, id, AccessEnum.Operate, oId);
                await db.AddAsync(privilege);
            }
            foreach (int rId in roleIds)
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
        private void GetChildrenOrganizationIdList(List<Organization> organizations, List<int> idList, int organizationd)
        {
            var childrens = organizations.Where(x => x.PId == organizationd).Select(s=>s.Id).ToList();
            if (childrens.Count > 0)
            {
                idList.AddRange(childrens);
                foreach (int id in childrens)
                {
                    GetChildrenOrganizationIdList(organizations, idList, id);
                }
            }
        }
    }
}
