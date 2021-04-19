using Microsoft.Extensions.Configuration;

namespace Windows.Admin.Infrastructure.Configuration
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class AppSetting
    {
        public static IConfiguration Configuration { get; private set; }
        public static Jwt Jwt { get; private set; }
        public static Cors Cors { get; private set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public static Database Database { get; private set; }
    }
}
