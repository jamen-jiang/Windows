using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Admin.Domain;
using Windows.Admin.Domain.Enums;
using Windows.Admin.Infrastructure.EFCore;
using Windows.Admin.Infrastructure.EFCore.Extensions;
using Windows.Application.Shared.Service;
using Windows.Infrastructure.EFCore;
using Windows.Infrastructure.Extensions;

namespace Windows.Admin.Application
{
    public class ModuleOperateService:BaseService,IModuleOperateService
    {
        private readonly IMapper _mapper;
        private readonly AdminDbContext _db;
        public ModuleOperateService(AdminDbContext db,IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        /// <summary>
        /// 获取模块操作树
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleOperateResponse>> GetModuleOperates()
        {
            using (_db)
            {
                List<Module> modules = await _db.Module.AsNoTracking().ToListAsync();
                var dtos = _mapper.Map<List<ModuleOperateResponse>>(modules);
                var operates = _db.Operate.AsNoTracking().ToList();
                var operateDtos = _mapper.Map<List<OperateResponse>>(operates);
                foreach (var m in dtos)
                {
                    m.Operates = operateDtos.Where(x => x.ModuleId == m.Id.ToInt()).ToList();
                }
                List<ModuleOperateResponse> list = new List<ModuleOperateResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 根据用户Id获取授权的菜单操作列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ModuleOperateResponse>> GetAuthorizeModuleOperates(int userId)
        {
            using (_db)
            {
                List<Module> modules = null;
                List<Operate> operates = null;
                //如果是管理员
                //if (userId == AppSetting.SystemConfig.Admin.ToGuid())
                if (userId == 1)
                {
                    //取出所有模块所有操作
                    modules = await _db.Module.AsNoTracking().OrderBy(o => o.Sort).ToListAsync();
                    operates = await _db.Operate.AsNoTracking().ToListAsync();
                }
                else
                {
                    var user = await _db.User.Include(x=>x.Organization_User).Include(x=>x.Role_User).FindByIdAsync(userId);
                    var organizationIds = user.Organization_User.Select(s => s.OrganizationId).ToArray();
                    var userRoleIds = await _db.Role.GetByUserId(userId).Select(s => s.Id).ToArrayAsync();
                    var organizationRoleIds = await _db.Role.GetByOrganizationIds(organizationIds).Select(s => s.Id).ToArrayAsync();
                    List<Privilege> privilegeList = await _db.Privilege.GetByMasterValues(userId, organizationIds, userRoleIds, organizationRoleIds).ToListAsync();
                    int[] moduleIds = privilegeList.Where(x => x.Access == AccessEnum.Module.ToString()).Select(s => s.AccessValue).ToArray();
                    int[] operateIds = privilegeList.Where(x => x.Access == AccessEnum.Operate.ToString()).Select(s => s.AccessValue).ToArray();
                    modules = await _db.Module.Where(x => moduleIds.Contains(x.Id)).OrderBy(o => o.Sort).ToListAsync();
                    operates = await _db.Operate.Where(x => operateIds.Contains(x.Id)).ToListAsync();
                }
                var moduleDtos = _mapper.Map<List<ModuleOperateResponse>>(modules);
                var operateDtos = _mapper.Map<List<OperateResponse>>(operates);
                foreach (var m in moduleDtos)
                {
                    m.Operates = operateDtos.Where(x => x.ModuleId == m.Id.ToInt()).ToList();
                }
                List<ModuleOperateResponse> list = new List<ModuleOperateResponse>();
                CreateTree(null, moduleDtos, list);
                return list;
            }
        }
        /// <summary>
        /// 获取授权的模块操作Id列表
        /// </summary>
        /// <param name="master"></param>
        /// <param name="masterValue"></param>
        /// <returns></returns>
        public async Task<AuthorizeModuleOperateIdsResponse> GetAuthorizeModuleOperateIds(MasterEnum master, int masterValue)
        {
            using (_db)
            {
                AuthorizeModuleOperateIdsResponse response = new AuthorizeModuleOperateIdsResponse();
                response.ModuleIds = await _db.Privilege.Get(master, AccessEnum.Module, masterValue).Select(s => s.AccessValue).ToListAsync();
                response.OperateIds = await _db.Privilege.Get(master, AccessEnum.Operate, masterValue).Select(s => s.AccessValue).ToListAsync();
                return response;
            }
        }
    }
}
