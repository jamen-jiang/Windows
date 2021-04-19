using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class LogOperateRequest
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime LogOn { get; set; } = DateTime.Now;
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ApiName { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 接口提交方法
        /// </summary>
        public string ApiMethod { get; set; }
        /// <summary>
        /// 请求数据
        /// </summary>
        public string Request { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string Response { get; set; }
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
        /// 耗时（毫秒）
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; } = true;
    }
}
