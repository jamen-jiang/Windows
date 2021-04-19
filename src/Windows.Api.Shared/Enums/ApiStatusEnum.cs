using System.ComponentModel;

namespace Windows.Api.Shared.Enums
{
    /// <summary>
    /// Api状态
    /// </summary>
    public enum ApiStatusEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 200,
        /// <summary>
        /// 令牌无效
        /// </summary>
        [Description("令牌无效")]
        Fail_Token_Unvalid = 1,
        /// <summary>
        /// 令牌过期
        /// </summary>
        [Description("令牌过期")]
        Fail_Token_Expired = 2,
        /// <summary>
        /// 没访问权限
        /// </summary>
        [Description("没访问权限")]
        Fail_UnAuthorized = 401,
        /// <summary>
        /// 403
        /// </summary>
        [Description("403")]
        Fail_Forbidden = 403,
        /// <summary>
        /// 应用程序错误
        /// </summary>
        [Description("应用程序错误")]
        Fail_App = 98,
        /// <summary>
        /// 系统异常
        /// </summary>
        [Description("系统异常")]
        Fail_Exception = 99
    }
}
