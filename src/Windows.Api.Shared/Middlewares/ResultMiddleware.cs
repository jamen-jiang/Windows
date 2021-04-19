using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Windows.Api.Shared.Middlewares
{
    public class ResultMiddleware
    {
        private readonly RequestDelegate _next;
        public ResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            MemoryStream ms1 = null;
            StreamWriter writer = null;
            HttpRequest request = context.Request;
            // 获取请求body内容
            if (request.Method.ToLower().Equals("post"))
            {
                // 启用倒带功能，就可以让 Request.Body 可以再次读取
                request.EnableBuffering();
                Stream stream = request.Body;
                //获取到body值
                string bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();
                //修改body值
                bodyAsText = Regex.Replace(bodyAsText, "(\":\")([0-9]{16,19})(\",)", "\":$2,");
                //放到流中回填回去
                ms1 = new MemoryStream();
                writer = new StreamWriter(ms1);
                writer.Write(bodyAsText);
                writer.Flush();
                request.Body = ms1;
                request.Body.Position = 0;
            }
            //获取到Response.body内容
            using (var ms = new MemoryStream())
            {
                var orgBodyStream = context.Response.Body;
                context.Response.Body = ms;
                //context.Response.ContentType = "multipart/form-data";
                //执行controller中正常逻辑代码
                await _next(context);
                using (var sr = new StreamReader(ms))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    //得到Action的返回值
                    var responseJsonResult = sr.ReadToEnd();
                    ms.Seek(0, SeekOrigin.Begin);
                    //如下代码若不注释则会显示Action的返回值 这里做了注释 则清空Action传过来的值  
                    //  await ms.CopyToAsync(orgBodyStream);
                    responseJsonResult = Regex.Replace(responseJsonResult, "(\":)([0-9]{16,})(,)", "$1\"$2\"$3");
                    var alterResult = responseJsonResult;
                    context.Response.Body = orgBodyStream;
                    //显示修改后的数据 
                    await context.Response.WriteAsync(alterResult, Encoding.UTF8);
                }
            }
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
            if (ms1 != null)
            {
                ms1.Close();
                ms1.Dispose();
            }
        }
    }
}
