using AutoMapper;
using Jyz.Domain;
using Jyz.Infrastructure;
using Jyz.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class LogLoginService : BaseService, ILogLoginService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        public LogLoginService(IHttpContextAccessor context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<PageResponse<LogLoginResponse>> Query(PageRequest info)
        {
            using (var db = NewDB())
            {
                PageResponse<LogLoginResponse> model = new PageResponse<LogLoginResponse>();
                var query = db.LogLogin.AsNoTracking();
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
            using (var db = NewDB())
            {
                await db.AddAsync(model);
                await db.SaveChangesAsync();
            }
        }
    }
}
