using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Windows.Application.Shared.Dto;
using Windows.Application.Shared.Exception;
using Windows.Application.Shared.Service;
using Windows.Infrastructure.EFCore;
using Windows.Infrastructure.Extensions;
using Windows.Module.Application.Enums;
using Windows.Module.Infrastructure.EFCore;

namespace Windows.Module.Application
{
    public class ModuleService : BaseService, IModuleService
    {
        private readonly IMapper _mapper;
        private readonly ModuleDbContext _db;
        public ModuleService(ModuleDbContext db,IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        /// <summary>
        /// 根据privilegeIds获取模块列表
        /// </summary>
        /// <param name="privilegeIds"></param>
        /// <returns></returns>
        public List<Domain.Module> GetListByPrivilegeIds(List<int> privilegeIds)
        {
            return null;
            //using (_db)
            //{
            //    var moduleIds = _db.Privilege.Where(x => privilegeIds.Contains(x.Id) && x.Access == AccessEnum.Module.ToString()).Select(s => s.AccessValue).ToArray();
            //    List<Domain.Module> modules = _db.Module.WhereByModuleIds(moduleIds).ToList();
            //    return modules;
            //}
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleResponse>> Query(ModuleRequest info)
        {
            using (_db)
            {
                var query = _db.Module.AsNoTracking();
                if (!info.Name.IsNullOrEmpty())
                    query = query.Where(x => x.Name.Contains(info.Name));
                List<Domain.Module> modules = await query.ToListAsync();
                var dtos = _mapper.Map<List<ModuleResponse>>(modules);
                List<ModuleResponse> list = new List<ModuleResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ModuleResponse> Detail(int id)
        {
            using (_db)
            {
                var module = await _db.Module.FindByIdAsync(id);
                return _mapper.Map<ModuleResponse>(module);
            }
        }
        /// <summary>
        /// 获取模块目录列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ComboBoxTreeResponse>> GetModuleCatalogs()
        {
            using (_db)
            {
               var modules = await _db.Module.AsNoTracking().Where(x=>x.Type == (int)ModuleTypeEnum.Catalog).ToListAsync();
                var dtos = _mapper.Map<List<ComboBoxTreeResponse>>(modules);
                var list = new List<ComboBoxTreeResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ComboBoxTreeResponse>> GetModules()
        {
            using (_db)
            {
                var modules = await _db.Module.AsNoTracking().ToListAsync();
                var dtos = _mapper.Map<List<ComboBoxTreeResponse>>(modules);
                var list = new List<ComboBoxTreeResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 添加模块
        /// </summary>
        public async Task Add(ModuleAddRequest info)
        {
            if (info.PId == null)
            {
                if (info.Type == (int)ModuleTypeEnum.Menu)
                {
                    throw new ApiException("顶级目录类型不能为菜单!");
                }
            }
            using (_db)
            {
                Domain.Module model = _mapper.Map<Domain.Module>(info);
                await _db.AddEntityAsync(model);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Modify(ModuleModifyRequest info)
        {
            if (info.PId == null)
            {
                if (info.Type == (int)ModuleTypeEnum.Menu)
                {
                    throw new ApiException("顶级目录类型不能为菜单!");
                }
            }
            using (_db)
            {
                var model = await _db.Module.FindByIdAsync(info.Id);
                _mapper.Map(info, model);
                _db.ModifyEntity(model);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 获取当前模块及下面所有模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<int>> GetCurrentAndChildrenIdList(int id)
        {
            using (_db)
            {
                var modules = await _db.Module.AsNoTracking().ToListAsync();
                List<int> idList = new List<int>();
                idList.Add(id);
                GetChildrenModuleIdList(modules, idList, id);
                return idList;
            }
        }
        /// <summary>
        /// 获取当前模块及下面所有模块
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="idList"></param>
        /// <param name="id"></param>
        private void GetChildrenModuleIdList(List<Domain.Module> modules, List<int> idList, int pId)
        {
            var childrens = modules.Where(x => x.PId == pId).Select(s => s.Id).ToList();
            if (childrens.Count > 0)
            {
                idList.AddRange(childrens);
                foreach (int id in childrens)
                {
                    GetChildrenModuleIdList(modules, idList, id);
                }
            }
        }
    }
}
