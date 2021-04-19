using AutoMapper;
using Jyz.Domain;
using Jyz.Domain.Enums;
using Jyz.Infrastructure;
using Jyz.Infrastructure.Data;
using Jyz.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class OperateService : BaseService, IOperateService
    {
        private readonly IMapper _mapper;
        private readonly IModuleService _moduleSvc;
        public OperateService(IMapper mapper, IModuleService moduleSvc)
        {
            _mapper = mapper;
            _moduleSvc = moduleSvc;
        }
        /// <summary>
        /// 获取所有操作Url
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<List<OperateUrlResponse>> GetAllOperateUrl()
        {
            using (var db = NewDB())
            {
                var operates= await db.Operate.Include(x => x.Module).AsNoTracking().ToListAsync();
                return _mapper.Map<List<OperateUrlResponse>>(operates);
            }
        }
        /// <summary>
        /// 根据privilegeIds获取操作列表
        /// </summary>
        /// <param name="privilegeIds"></param>
        /// <returns></returns>
        public List<Operate> GetListByPrivilegeIds(List<Guid> privilegeIds)
        {
            using (var db = NewDB())
            {
                var moduleIds = db.Privilege.Where(x => privilegeIds.Contains(x.Id) && x.Access == AccessEnum.Operate.ToString()).Select(s => s.AccessValue).ToArray();
                List<Operate> operates = db.Operate.Where(x => moduleIds.Contains(x.Id)).ToList();
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
            using (var db = NewDB())
            {
                PageResponse<OperateResponse> model = new PageResponse<OperateResponse>();
                var query = db.Operate.AsNoTracking();
                if (!info.Query.ModuleId.IsEmpty())
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
        public async Task<OperateResponse> Detail(Guid id)
        {
            using (var db = NewDB())
            {
                Operate operate = await db.Operate.FindByIdAsync(id);
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
            using (var db = NewDB())
            { 
                Operate model = _mapper.Map<Operate>(info);
                BeforeAdd(model);
                await db.Operate.AddAsync(model);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="info"></param>
        public async Task Modify(OperateModifyRequest info)
        {
            using (var db = NewDB())
            {
                Operate model = await db.Operate.FindByIdAsync(info.Id);
                _mapper.Map(info,model);
                BeforeModify(model);
                await db.SaveChangesAsync();
            }
        }
    }
}
