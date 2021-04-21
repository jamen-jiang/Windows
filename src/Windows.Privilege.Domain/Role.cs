using System.Collections.Generic;
using Windows.SeedWork;

namespace Windows.Privilege.Domain
{
    public partial class Role : FullEntity<int>
    {
        public string Name { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
    }
}
