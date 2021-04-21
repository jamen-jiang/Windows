using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Application.Shared.Dto
{
    public class PageRequest
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
    }
    public class PageRequest<T> : PageRequest where T : new()
    {
        public T Query { get; set; } = new T();
    }
}
