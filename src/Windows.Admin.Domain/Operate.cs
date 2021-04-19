using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class Operate : FullEntity<int>
    {
        public int ModuleId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
        public Module Module { get; set; }
    }
}
