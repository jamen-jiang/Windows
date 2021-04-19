using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Windows.Api.Shared.Attributes
{
    /// <summary>
    /// 返回值过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ApiResponseAttribute: ResultFilterAttribute
    {
        /// <summary>
        /// 统一返回值
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            ApiResponse response = new ApiResponse();
            if (context.Result != null)
            {
                var objectResult = context.Result as ObjectResult;
                response.Data = objectResult?.Value;
            }
            context.Result = new ObjectResult(response);
            //base.OnResultExecuting(context);
        }
    }
}
