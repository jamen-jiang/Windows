using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Infrastructure.Configuration
{
    public class Jwt
    {
        /// <summary>
        /// 加密key
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期分钟
        /// </summary>
        public int ExpireMinutes { get; set; }
    }
}
