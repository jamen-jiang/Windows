using Microsoft.AspNetCore.Builder;
using Windows.Api.Shared.Middlewares;

namespace Windows.Api.Shared.Middlewares
{
    public static class MiddlewareUtil
    {
        /// <summary>
        /// 异常处理中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
        public static IApplicationBuilder UseResultMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResultMiddleware>();
        }
    }
}
