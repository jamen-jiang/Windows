using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Admin.Domain;
using Windows.Admin.Domain.Enums;
using Windows.Admin.Infrastructure.EFCore;
using Windows.Application.Shared.Dto;
using Windows.Application.Shared.Service;
using Windows.Infrastructure.EFCore;
using Windows.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;

namespace Windows.Admin.Application
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IMapper _mapper;
        private readonly AdminDbContext _db;
        public RoleService(AdminDbContext db,IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        /// <summary>
        /// 根据id获取role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RoleResponse> Detail(int id)
        {
            using (_db)
            {
                var model = await _db.Role.FindByIdAsync(id);
                return _mapper.Map<RoleResponse>(model);
            }
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<PageResponse<RoleResponse>> Query(PageRequest<RoleRequest> info)
        {
            using (_db)
            {
                PageResponse<RoleResponse> model = new PageResponse<RoleResponse>();
                var query = _db.Role.AsNoTracking();
                if (!info.Query.Name.IsNullOrEmpty())
                    query = query.Where(x => x.Name.Contains(info.Query.Name));
                int totalCount = await query.CountAsync();
                List<Role> list = await query.Paging(info.PageIndex, info.PageSize).ToListAsync();
                model.PageIndex = info.PageIndex;
                model.PageSize = info.PageSize;
                model.TotalCount = totalCount;
                model.List = _mapper.Map<List<RoleResponse>>(list);
                return model;
            }
        }
        /// <summary>
        /// 根据用户Id获取角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<RoleResponse>> GetUserRoles(int userId)
        {
            using (_db)
            {
                var roles = await (from a in _db.Role
                                   join b in _db.Role_User on a.Id equals b.RoleId
                                   where b.UserId == userId
                                   select a).ToListAsync();
                return _mapper.Map<List<RoleResponse>>(roles);
            }
        }
        /// <summary>
        /// 根据部门Id获取角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<RoleResponse>> GetOrganizationRoles(int organizationId)
        {
            using (_db)
            {
                var roles = await (from a in _db.Role
                                   join b in _db.Role_Organization on a.Id equals b.RoleId
                                   where b.OrganizationId == organizationId
                                   select a).ToListAsync();
                return _mapper.Map<List<RoleResponse>>(roles);
            }
        }
        /// <summary>
        /// 角色信息修改
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Save(RoleModifyRequest info)
        {
            using (_db)
            {
                if (info.Id != 0)
                {
                    await _db.ExecSqlNoQuery("delete Role_User where RoleId=@RoleId", new SqlParameter("RoleId", info.Id));
                    await _db.ExecSqlNoQuery("delete Privilege where MasterValue=@MasterValue", new SqlParameter("MasterValue", info.Id));
                    Role role = await _db.Role.FindByIdAsync(info.Id);
                    _mapper.Map(info.Role, role);
                    //BeforeModify(role);
                }
                else
                {
                    Role role = _mapper.Map<Role>(info.Role);
                    await _db.AddAsync(role);
                    await _db.SaveChangesAsync();
                    info.Id = role.Id;
                }
                foreach (int id in info.ModuleIds)
                {
                    Privilege privilege = new Privilege(MasterEnum.Role, info.Id, AccessEnum.Module, id);
                    await _db.AddAsync(privilege);
                }
                foreach (int id in info.OperateIds)
                {
                    Privilege privilege = new Privilege(MasterEnum.Role, info.Id, AccessEnum.Operate, id);
                    await _db.AddAsync(privilege);
                }
                foreach (int id in info.UserIds)
                {
                    Role_User model = new Role_User();
                    model.UserId = id;
                    model.RoleId = info.Id;
                    await _db.AddAsync(model);
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
