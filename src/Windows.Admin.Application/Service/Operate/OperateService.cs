using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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

namespace Windows.Admin.Application
{
    public class OperateService : BaseService, IOperateService
    {
        private readonly IMapper _mapper;
        private readonly IModuleService _moduleSvc;
        private readonly AdminDbContext _db;
        public OperateService(AdminDbContext db,IMapper mapper, IModuleService moduleSvc)
        {
            _mapper = mapper;
            _moduleSvc = moduleSvc;
            _db = db;
        }
        /// <summary>
        /// 获取所有操作Url
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<List<OperateUrlResponse>> GetAllOperateUrl()
        {
            using (_db)
            {
                var operates= await _db.Operate.Include(x => x.Module).AsNoTracking().ToListAsync();
                return _mapper.Map<List<OperateUrlResponse>>(operates);
            }
        }
        /// <summary>
        /// 根据privilegeIds获取操作列表
        /// </summary>
        /// <param name="privilegeIds"></param>
        /// <returns></returns>
        public List<Operate> GetListByPrivilegeIds(List<int> privilegeIds)
        {
            using (_db)
            {
                var moduleIds = _db.Privilege.Where(x => privilegeIds.Contains(x.Id) && x.Access == AccessEnum.Operate.ToString()).Select(s => s.AccessValue).ToArray();
                List<Operate> operates = _db.Operate.Where(x => moduleIds.Contains(x.Id)).ToList();
                return operates;
            }
        }
        /// <summary>
        /// 获取对应的功能列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async  Task<PageResponse<OperateResponse>> Query(PageRequest<OperateRequest> info)
        {
            using (_db)
            {
                PageResponse<OperateResponse> model = new PageResponse<OperateResponse>();
                var query = _db.Operate.AsNoTracking();
                if (info.Query.ModuleId != 0)
                {
                    var ids = await _moduleSvc.GetCurrentAndChildrenIdList(info.Query.ModuleId);
                    query = query.Where(x => ids.Contains(x.ModuleId));
                }
                if (!info.Query.Name.IsNullOrEmpty())
                    query = query.Where(x => x.Name.Contains(info.Query.Name));
                int totalCount = await query.CountAsync();
                List<Operate> list = await query.Paging(info.PageIndex, info.PageSize).Include(x => x.Module).ToListAsync();
                model.PageIndex = info.PageIndex;
                model.PageSize = info.PageSize;
                model.TotalCount = totalCount;
                model.List = _mapper.Map<List<OperateResponse>>(list);
                return model;
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperateResponse> Detail(int id)
        {
            using (_db)
            {
                Operate operate = await _db.Operate.FindByIdAsync(id);
                return _mapper.Map<OperateResponse>(operate);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Add(OperateAddRequest info)
        {
            using (_db)
            { 
                Operate model = _mapper.Map<Operate>(info);
                //BeforeAdd(model);
                await _db.Operate.AddAsync(model);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="info"></param>
        public async Task Modify(OperateModifyRequest info)
        {
            using (_db)
            {
                Operate model = await _db.Operate.FindByIdAsync(info.Id);
                _mapper.Map(info,model);
                //BeforeModify(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
