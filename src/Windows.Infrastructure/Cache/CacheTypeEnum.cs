using System;
using System.Collections.Generic;
using System.Text;

namespace Jyz.Infrastructure
{
    public enum CacheTypeEnum
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        Memory = 0,
        /// <summary>
        /// Redis缓存
        /// </summary>
        Redis
    }
}
