using System.Collections.Generic;
using Windows.SeedWork;

namespace Windows.Module.Domain
{
    public partial class Module : FullEntity<int>
    {
        /// <summary>
        /// ∏∏Id
        /// </summary>
        public int? PId { get; set; }
        /// <summary>
        /// ¿‡–Õ
        /// </summary>
        public int Type { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Icon { get; set; }
        public int? Sort { get; set; }
        public string Uri { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<Operate> Operates { get; set; }
    }
}
