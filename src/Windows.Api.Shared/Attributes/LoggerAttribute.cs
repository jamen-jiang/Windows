using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Windows.Api.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggerAttribute : Attribute
    {
        public string Remark { get; }
        public LoggerAttribute(string remark)
        {
            Remark = remark ?? throw new ArgumentNullException(nameof(remark));
        }
    }
}
