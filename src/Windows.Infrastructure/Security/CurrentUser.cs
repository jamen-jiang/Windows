using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Windows.Infrastructure.Extensions;

namespace Windows.Infrastructure.Security
{
    public static class CurrentUser
    {
        private static IHttpContextAccessor _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
        public static HttpContext HttpContext
        {
            get
            {
                return _context.HttpContext;
            }
        }
        /// <summary>
        /// 是否已授权
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return HttpContext.User.Identity.IsAuthenticated;
            }
        }
        public static int UserId
        {
            get
            {
                return HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti).ToInt();
            }
        }
        public static string UserName
        {
            get
            {
                return HttpContext.User.FindFirstValue(ClaimTypes.Name)?.ToString();
            }
        }
        public static T GetService<T>() where T : class
        {
            return HttpContext.RequestServices.GetService<T>();
        }
    }
}
