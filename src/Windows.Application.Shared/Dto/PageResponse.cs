using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Application.Shared.Dto
{
    public class PageResponse<T> where T : class
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        public List<T> List { get; set; }
    }
}
