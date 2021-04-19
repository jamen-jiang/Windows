using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Infrastructure.Configuration
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class Database
    {
        public SqlServer SqlServer { get; set; }
    }
    /// <summary>
    /// SqlServer数据库
    /// </summary>
    public class SqlServer
    { 
        public string ConnectionString { get; set; }
    }
}
