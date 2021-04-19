using Jyz.Application;
using Jyz.Domain;
using Jyz.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Windows.Api.Shared.Filter
{
    /// <summary>
    /// 权限过滤
    /// </summary>
    public class PrivilegeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //允许匿名访问
            //if (context.HttpContext.User.Identity.IsAuthenticated ||
            //     context.Filters.Any(item => item is IAllowAnonymousFilter)||
            //      context.ActionDescriptor.EndpointMetadata.Any(item => item is AllowAnonymousAttribute))
            //    return;
            if (context.HttpContext.User.Identity.IsAuthenticated ||
                context.ActionDescriptor.EndpointMetadata.Any(item => item is NoPrivilegeAttribute || item is AllowAnonymousAttribute))
                return;
            string controllerName = context.RouteData.Values["controller"].ToString(); ;
            string actionName = context.RouteData.Values["action"].ToString();
            //获取权限接口
            var privilegeSvc = CurrentUser.GetService<IPrivilegeService>();
            var list = await privilegeSvc.GetOperateUrlsByUserId(CurrentUser.UserId);
            var model = list.FirstOrDefault(x => x.Controller.Compare(controllerName) && x.Action.Compare(actionName));
            if (model == null)
            {
                throw new ApiException(ApiStatusEnum.Fail_Forbidden);
            }
            else if (!model.IsAuthorize)
            {
                throw new ApiException(ApiStatusEnum.Fail_UnAuthorized);
            }
        }
    }
    /// <summary>
    /// 不需要权限
    /// </summary>
    public class NoPrivilegeAttribute : AuthorizeAttribute
    { 
    
    }
}
