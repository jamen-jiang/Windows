using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class DictionaryAddRequest
    {
        public Guid? PId { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string Value { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
