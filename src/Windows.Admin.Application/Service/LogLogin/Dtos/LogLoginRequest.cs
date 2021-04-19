using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class LogLoginRequest
    {
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginOn { get; set; } = DateTime.Now;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        ///客户端地点
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 客户端 UserAgent
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        public string Os { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; } = true;
    }
}
