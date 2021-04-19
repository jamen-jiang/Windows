using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Windows.Api.Shared.Enums;

namespace Windows.Api.Shared.Attributes
{
    /// <summary>
    /// 模型过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 模型验证
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ApiResponse response = new ApiResponse();
                response.Status = (int)ApiStatusEnum.Fail_App;
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        response.Message += error.ErrorMessage + "|";
                    }
                }
                context.Result = new ObjectResult(response);
            }
            base.OnActionExecuting(context);
        }
    }
}
