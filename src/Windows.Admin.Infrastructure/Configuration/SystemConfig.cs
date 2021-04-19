using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Infrastructure.Configuration
{
    public class SystemConfig
    {
        public string Name { get; set; }
        public string Admin { get; set; }
        public string DbConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public int CacheType { get; set; }
    }
}
