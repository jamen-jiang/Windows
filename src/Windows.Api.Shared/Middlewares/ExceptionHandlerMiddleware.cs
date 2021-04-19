using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Api.Shared.Enums;
using Windows.Infrastructure.Extensions;
using Windows.Infrastructure.Utils;

namespace Windows.Api.Shared.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex == null) return;

            Logger.Error("请求处理异常" + ex.Message, ex);

            await WriteExceptionAsync(context, ex).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception ex)
        {
            ApiResponse response = new ApiResponse();
            if (ex is ApiException)
            {
                ApiException apiException = ex as ApiException;
                response.Status = apiException.Code;
                response.Message = apiException.Message;
            }
            else
            {
                response.Status = (int)ApiStatusEnum.Fail_Exception;
                response.Message = "系统错误";
            }
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response.ToJson(), Encoding.UTF8).ConfigureAwait(false);
        }
    }
}
