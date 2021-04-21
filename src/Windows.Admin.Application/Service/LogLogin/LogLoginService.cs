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
    public class LogLoginService : BaseService, ILogLoginService
    {
        private readonly AdminDbContext _db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        public LogLoginService(AdminDbContext db, IHttpContextAccessor context, IMapper mapper)
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
        public async Task<PageResponse<LogLoginResponse>> Query(PageRequest info)
        {
            using (_db)
            {
                PageResponse<LogLoginResponse> model = new PageResponse<LogLoginResponse>();
                var query = _db.LogLogin.AsNoTracking();
                int totalCount = await query.CountAsync();
                List<LogLogin> list = await query.Paging(info.PageIndex, info.PageSize).ToListAsync();
                model.PageIndex = info.PageIndex;
                model.PageSize = info.PageSize;
                model.TotalCount = totalCount;
                model.List = _mapper.Map<List<LogLoginResponse>>(list);
                return model;
            }
        }
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Add(LogLoginRequest info)
        {
            string ua = _context.HttpContext.Request.Headers["User-Agent"];
            var client = UAParser.Parser.GetDefault().Parse(ua);
            info.UserAgent = ua;
            info.Browser = client.UA.ToString();
            info.Os = client.OS.ToString();
            info.IP = IPUtils.GetIP(_context?.HttpContext?.Request);
            var model = _mapper.Map<LogLogin>(info);
            using (_db)
            {
                await _db.AddAsync(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
