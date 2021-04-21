using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Admin.Domain;
using Windows.Admin.Infrastructure.EFCore;
using Windows.Application.Shared.Dto;
using Windows.Application.Shared.Service;
using Windows.Infrastructure.EFCore;
using Windows.Infrastructure.Utils;

namespace Windows.Admin.Application
{
    public class LogOperateService : BaseService, ILogOperateService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        private readonly AdminDbContext _db;
        public LogOperateService(AdminDbContext db,IHttpContextAccessor context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _db = db;
        }
        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<PageResponse<LogOperateResponse>> Query(PageRequest info)
        {
            using (_db)
            {
                PageResponse<LogOperateResponse> model = new PageResponse<LogOperateResponse>();
                var query = _db.LogOperate.AsNoTracking();
                int totalCount = await query.CountAsync();
                List<LogOperate> list = await query.Paging(info.PageIndex, info.PageSize).ToListAsync();
                model.PageIndex = info.PageIndex;
                model.PageSize = info.PageSize;
                model.TotalCount = totalCount;
                model.List = _mapper.Map<List<LogOperateResponse>>(list);
                return model;
            }
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Add(LogOperateRequest info)
        {
            string ua = _context.HttpContext.Request.Headers["User-Agent"];
            var client = UAParser.Parser.GetDefault().Parse(ua);
            info.UserAgent = ua;
            info.Browser = client.UA.ToString();
            info.Os = client.OS.ToString();
            info.IP = IPUtils.GetIP(_context?.HttpContext?.Request);
            var model = _mapper.Map<LogOperate>(info);
            using (_db)
            {
                await _db.AddAsync(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
